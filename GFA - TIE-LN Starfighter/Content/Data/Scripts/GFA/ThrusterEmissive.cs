using VRage.Game.Components;
using Sandbox.ModAPI;
using VRage.ObjectBuilders;
using VRage.ModAPI;
using VRageMath;
using Sandbox.Common.ObjectBuilders;

namespace enenra.ThrusterEmissive
{
    [MyEntityComponentDescriptor(typeof(MyObjectBuilder_Thrust), false)]

    public class Container : MyGameLogicComponent
    {
        IMyThrust m_block;

        private const string EMISSIVE_MATERIAL_NAME = "Emissive";
        private Color WHITE = new Color(255, 255, 255);
        private Color BLACK = new Color(0, 0, 0);

        bool IsWorking => m_block.IsWorking;

        public override void Init(MyObjectBuilder_EntityBase objectBuilder)
        {
            m_block = Entity as IMyThrust;

            var subtype = m_block.BlockDefinition.SubtypeName;
            if (subtype != "GFA_SG_TIEFighter_Thruster")
            {
                m_block = null;
                return;
            }

            NeedsUpdate |= MyEntityUpdateEnum.BEFORE_NEXT_FRAME;

            m_block.IsWorkingChanged += OnIsWorkingChanged;
            base.Init(objectBuilder);
        }

        public override void Close()
        {
            if (m_block != null)
            {
                m_block.IsWorkingChanged -= OnIsWorkingChanged;
                m_block = null;
            }

            base.Close();
        }

        private void OnIsWorkingChanged(VRage.Game.ModAPI.IMyCubeBlock obj)
        {
            if (MyAPIGateway.Session == null)
                return;

            if (MyAPIGateway.Utilities.IsDedicated && MyAPIGateway.Multiplayer.IsServer)
                return;

            if (IsWorking)
            {
                m_block.SetEmissiveParts(EMISSIVE_MATERIAL_NAME, WHITE, 5f);
            }
            else 
            {
                m_block.SetEmissiveParts(EMISSIVE_MATERIAL_NAME, BLACK, 1f);
            }
        }
    }
}