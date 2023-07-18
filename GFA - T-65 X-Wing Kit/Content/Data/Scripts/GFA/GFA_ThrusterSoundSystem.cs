using System;
using System.Text;
using System.Collections.Generic;
using Sandbox.Game;
using Sandbox.ModAPI;
using Sandbox.Game.Entities;
using Sandbox.Definitions;
using Sandbox.Common.ObjectBuilders;
using VRage.Game;
using VRage.Game.Components;
using VRage.Game.ModAPI;
using VRage.Game.ModAPI.Ingame.Utilities;
using VRage.Game.Entity;
using VRage.ObjectBuilders;
using VRage.ModAPI;
using VRage.Utils;
using VRageMath;
using VRage.Sync;
using Sandbox.Common.ObjectBuilders;
using Sandbox.ModAPI;
using Sandbox.ModAPI.Interfaces.Terminal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VRage.Game.Components;
using VRage.Game.ModAPI;
using VRage.Game.ModAPI.Network;
using VRage.Network;
using VRage.ObjectBuilders;
using VRage.Sync;
using VRage.Utils;


namespace MWI.ThrusterSoundSystem
{
    [MyEntityComponentDescriptor(typeof(MyObjectBuilder_Thrust), false, "GFA_SG_XWing_Thruster")]
    public class ThrusterSoundSystem : MyGameLogicComponent, IMyEventProxy
    {
        string thruster_sfx_version = "Sound System - v1.0";
        private MyEntity3DSoundEmitter soundEmitter;
        private MySoundPair soundPair;
        bool soundRunning = false;
        int retriggerDelay = 100; // minimum time in tick before the thruster sound wille run from <start>
        int retriggerDelayTime = 100;

        int timeSinceStop = 100;

        bool isAiControlled = false;
        bool AiIsThrusting = false;
        int aiThrustTimer = 60;
        int aiTime = 60;

        private MyThrust thruster;

        //Dedi Sync
        public MySync<float, SyncDirection.FromServer> m_serverSync_thrust = null;
        public float syncedThrustValue = 0.0f;

        public override void Init(MyObjectBuilder_EntityBase objectBuilder) //On block placement:
        {
            m_serverSync_thrust.ValueChanged += serverSync_UpdateThrustData;
            thruster = Entity as MyThrust;
            NeedsUpdate |= VRage.ModAPI.MyEntityUpdateEnum.BEFORE_NEXT_FRAME;
        }

        private void serverSync_UpdateThrustData(MySync<float, SyncDirection.FromServer> newThrust)
        {
            syncedThrustValue = newThrust.Value;
        }
        public override void UpdateOnceBeforeFrame()
        {
            if (!MyAPIGateway.Utilities.IsDedicated && thruster.CubeGrid.Physics != null)
            {
                soundEmitter = new MyEntity3DSoundEmitter((MyEntity)Entity);
                thruster.m_soundEmitter.CustomVolume = 0;
                soundPair = (thruster as MyThrust).BlockDefinition.PrimarySound;
                NeedsUpdate = MyEntityUpdateEnum.EACH_FRAME | MyEntityUpdateEnum.EACH_100TH_FRAME | MyEntityUpdateEnum.EACH_10TH_FRAME;                
            }
            if (MyAPIGateway.Utilities.IsDedicated)
            {
                NeedsUpdate = MyEntityUpdateEnum.EACH_FRAME | MyEntityUpdateEnum.EACH_100TH_FRAME | MyEntityUpdateEnum.EACH_10TH_FRAME;
            }
        }
        public override void UpdateBeforeSimulation100()
        {
            if (thruster != null)
            //On controll changed instead?
            {//Check if grid is controlled by ai
                isAiControlled = false;
                var grid = thruster.CubeGrid as IMyCubeGrid;
                if (grid != null){
                    if (grid.ControlSystem != null){
                        if (grid.ControlSystem.CurrentShipController != null){
                            isAiControlled = grid.ControlSystem.CurrentShipController.IsAutopilotControlled;
                        }
                    }
                }
            }
            base.UpdateBeforeSimulation();
        }
        public override void UpdateBeforeSimulation10()
        {

            if (MyAPIGateway.Multiplayer.MultiplayerActive)//Server or host
            {
                if ((MyAPIGateway.Utilities.IsDedicated || MyAPIGateway.Multiplayer.IsServer))
                {
                    if (isAiControlled)
                    {
                        m_serverSync_thrust.Value = thruster.CurrentStrength;
                        //MyLog.Default.WriteLineAndConsole("Sending value....?");
                    }

                }
            }

            base.UpdateBeforeSimulation10();
        }
        public override void UpdateAfterSimulation()
        {
            if (MyAPIGateway.Utilities.IsDedicated) return;
            if (thruster != null)
            {
                float RealThrust = thruster.CurrentStrength;  //synced to solve the issue of dedi  not syncing AI
                if (isAiControlled)
                {
                    if (MyAPIGateway.Multiplayer.MultiplayerActive)
                    {
                        RealThrust = syncedThrustValue;
                    }
                    //MyLog.Default.WriteLineAndConsole("[AutoPilot] CurrentThrustPercentage: " + theThrust.ToString());
                }

                if (RealThrust > 0.10f )
                {
                    aiThrustTimer = aiTime;
                    if (retriggerDelay <= 0)
                    {
                        if ((!soundEmitter.IsPlaying || (soundEmitter.IsPlaying && !soundRunning)) && timeSinceStop > 0)
                        {
                            //MyAPIGateway.Utilities.ShowNotification("CustomSound: Retrigger", 100, MyFontEnum.Blue);
                            soundRunning = true;
                            soundEmitter.PlaySound(soundPair, true, true, false, false, false, null, false);
                            if (soundEmitter.IsPlaying) retriggerDelay = retriggerDelayTime;
                            if (soundEmitter.IsPlaying) timeSinceStop = retriggerDelayTime;
                        }
                        else if ((!soundEmitter.IsPlaying || (soundEmitter.IsPlaying && !soundRunning)))
                        {
                            soundRunning = true;
                            //MyAPIGateway.Utilities.ShowNotification("CustomSound: Play", 100, MyFontEnum.Blue);
                            soundEmitter.PlaySound(soundPair, true, false, false, false, false, null, false);
                            if (soundEmitter.IsPlaying) retriggerDelay = retriggerDelayTime;
                            if (soundEmitter.IsPlaying) timeSinceStop = retriggerDelayTime;
                        }
                    }
                }
                else if ((soundRunning && (isAiControlled && aiThrustTimer<=0)) || (soundRunning && !isAiControlled))
                {
                    soundRunning = false;
                    soundEmitter.PlaySound(soundPair, true, true, false, false, true, null, false);

                    //MyAPIGateway.Utilities.ShowNotification("CustomSound: Stoppign", 10, MyFontEnum.Blue);

                }

                if (retriggerDelay > 0) retriggerDelay -= 1;
                if (aiThrustTimer > 0) aiThrustTimer -= 1;
                if (!soundRunning && timeSinceStop > 0) timeSinceStop -= 1;
                //MyAPIGateway.Utilities.ShowNotification("CustomSound: " + soundEmitter.IsPlaying+" || "+ retriggerDelay+"||", 1, MyFontEnum.Blue);                
            }
        }
        public override void Close() 
        {
            m_serverSync_thrust.ValueChanged -= serverSync_UpdateThrustData;
            soundEmitter?.StopSound(true, true);
            soundEmitter = null;
        }        
    }
}