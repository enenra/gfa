using VRage.Game.Components;
using System.Linq;
using Sandbox.Definitions;
using Sandbox.Game.Entities;
using VRage.Game;
using Digi;

namespace GFA.SwitchJumpEffects
{

    [MySessionComponentDescriptor(MyUpdateOrder.BeforeSimulation)]
    public class SwitchEffects : MySessionComponentBase
    {
        UndoableEditToolset DefEdits = new UndoableEditToolset();

        public override void LoadData()
        {

            foreach (var jumpDef in MyDefinitionManager.Static.GetAllDefinitions().OfType<MyJumpDriveDefinition>())
            {
                DefEdits.MakeEdit(jumpDef, (d, v) => d.JumpParticleEffect = v, jumpDef.JumpParticleEffect, "GFA_HyperspaceJump");
                DefEdits.MakeEdit(jumpDef, (d, v) => d.ChargingSound = v, jumpDef.ChargingSound, new MySoundPair("_GFA_Hyperspace_Charging", true));
                DefEdits.MakeEdit(jumpDef, (d, v) => d.JumpOutSound = v, jumpDef.JumpOutSound, new MySoundPair("_GFA_Hyperspace_Enter", true));
                if (jumpDef.CubeSize == MyCubeSize.Large)
                {
                    DefEdits.MakeEdit(jumpDef, (d, v) => d.JumpInSound = v, jumpDef.JumpInSound, new MySoundPair("_GFA_LG_Hyperspace_Exit", true));
                }
                else
                {
                    DefEdits.MakeEdit(jumpDef, (d, v) => d.JumpInSound = v, jumpDef.JumpInSound, new MySoundPair("_GFA_SG_Hyperspace_Exit", true));
                }
            }

        }

        protected override void UnloadData()
        {
            base.UnloadData();
            DefEdits.UndoAll();
        }

    }

}
