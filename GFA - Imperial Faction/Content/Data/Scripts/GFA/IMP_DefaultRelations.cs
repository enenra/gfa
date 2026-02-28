using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sandbox.Common;
using Sandbox.Common.ObjectBuilders;
using Sandbox.Common.ObjectBuilders.Definitions;
using Sandbox.Definitions;
using Sandbox.Game;
using Sandbox.Game.Entities;
using Sandbox.Game.EntityComponents;
using Sandbox.Game.GameSystems;
using Sandbox.ModAPI;
using Sandbox.ModAPI.Interfaces;
using Sandbox.ModAPI.Weapons;
using SpaceEngineers.Game.ModAPI;
using VRage.Game;
using VRage.Game.Components;
using VRage.Game.Entity;
using VRage.Game.ModAPI;
using VRage.ModAPI;
using VRage.ObjectBuilders;
using VRage.Utils;
using VRageMath;

namespace GFA.IMP_DefaultRelations
{

    [MySessionComponentDescriptor(MyUpdateOrder.BeforeSimulation)]

    public class SessionCore : MySessionComponentBase
    {
        public IMyFaction IMPFaction = null;
        public IMyFaction MGFaction = null;
        public IMyFaction REBFaction = null;
        public IMyFaction CIVFaction = null;

        public override void BeforeStart()
        {

            if (!MyAPIGateway.Multiplayer.IsServer)
                return;

            if (IMPFaction == null)
            {
                IMPFaction = MyAPIGateway.Session.Factions.TryGetFactionByTag("IMP");
            }

            if (MGFaction == null)
            {
                MGFaction = MyAPIGateway.Session.Factions.TryGetFactionByTag("MG");
                if (MGFaction != null)
                    MyAPIGateway.Session.Factions.SetReputation(IMPFaction.FactionId, MGFaction.FactionId, 1500);
                //MyLog.Default.WriteLine("Adjusted MG.");
            }
            if (REBFaction == null)
            {
                REBFaction = MyAPIGateway.Session.Factions.TryGetFactionByTag("REB");
                if (REBFaction != null)
                    MyAPIGateway.Session.Factions.SetReputation(IMPFaction.FactionId, REBFaction.FactionId, -1500);
                //MyLog.Default.WriteLine("Adjusted REB.");
            }
            if (CIVFaction == null)
            {
                CIVFaction = MyAPIGateway.Session.Factions.TryGetFactionByTag("CIV");
                if (CIVFaction != null)
                    MyAPIGateway.Session.Factions.SetReputation(IMPFaction.FactionId, CIVFaction.FactionId, 0);
                //MyLog.Default.WriteLine("Adjusted CIV.");
            }

        }


    }

}