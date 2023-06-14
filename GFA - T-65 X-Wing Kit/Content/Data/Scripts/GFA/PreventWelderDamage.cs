using System;
using System.Collections.Generic;
using Sandbox.Common.ObjectBuilders;
using Sandbox.Game.Entities;
using Sandbox.ModAPI;
using VRage.Game;
using VRage.Game.Components;
using VRage.Game.ModAPI;
using VRage.ObjectBuilders;
using VRage.Utils;

namespace Digi.PreventWelderDamage
{
    [MySessionComponentDescriptor(MyUpdateOrder.NoUpdate)]
    public class Example_PreventBlockDamageOnCharacters : MySessionComponentBase
    {
        HashSet<MyObjectBuilderType> BlockTypes = new HashSet<MyObjectBuilderType>()
        {
            // prevent an entire block type from doing damage to characters.
            //typeof(MyObjectBuilder_ShipWelder),
            //typeof(MyObjectBuilder_ShipGrinder),
            //typeof(MyObjectBuilder_Drill),
        };

        HashSet<MyDefinitionId> BlockIDs = new HashSet<MyDefinitionId>()
        {
            MyDefinitionId.Parse("ShipWelder/GFA_SG_XWing_AstroBay"),
        };

        public override void BeforeStart()
        {
            // damage is done only serverside, this also means this script can work for DS that allow console players to join.
            if(MyAPIGateway.Session.IsServer)
            {
                MyAPIGateway.Session.DamageSystem.RegisterBeforeDamageHandler(100, DamageHandler);
            }
        }

        protected override void UnloadData()
        {
            // damage system does not have unregister
        }

        void DamageHandler(object victim, ref MyDamageInformation info)
        {
            try
            {
                if(info.IsDeformation || info.Amount <= 0 || info.AttackerId == 0)
                    return;

                IMyCharacter chr = victim as IMyCharacter;
                if(chr == null)
                    return;

                IMyCubeBlock block = MyEntities.GetEntityById(info.AttackerId) as IMyCubeBlock;
                if(block == null)
                    return;

                if(BlockTypes.Contains(block.BlockDefinition.TypeId) || BlockIDs.Contains(block.BlockDefinition))
                {
                    info.Amount = 0f;
                }
            }
            catch(Exception e)
            {
                AddToLog(e);
            }
        }

        void AddToLog(Exception e)
        {
            string modName = ModContext?.ModName ?? GetType().FullName;
            MyLog.Default.WriteLineAndConsole($"{modName} ERROR: {e.ToString()}");
            if(MyAPIGateway.Session?.Player != null)
                MyAPIGateway.Utilities.ShowNotification($"[ {modName} ERROR: {e.Message} | Send SpaceEngineers.Log to mod author ]", 10000, MyFontEnum.Red);
        }
    }
}