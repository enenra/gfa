using Sandbox.Common.ObjectBuilders;
using Sandbox.Game.Entities;
using Sandbox.ModAPI;
using System.Collections.Generic;
using VRage.Game.Components;
using VRage.Game.ModAPI;
using VRage.ModAPI;
using VRage.ObjectBuilders;
using VRage.Utils;
using VRageMath;

namespace Repulsors
{
    [MyEntityComponentDescriptor(typeof(MyObjectBuilder_Thrust), false)]
    class RepulsorLogic : MyGameLogicComponent
    {
        private readonly HashSet<string> _subtypes = new HashSet<string>()
        {
            "GFA_SG_TIEFighter_RepulsorBack",
            "GFA_SG_TIEFighter_RepulsorSide",
            "GFA_SG_TIEFighter_RepulsorVertical"
        };

        private const int THRESHOLD = 25;

        private IMyThrust _thrust;
        private IMyCubeGrid _grid;
        private Vector3 _direction;

        public override void Init(MyObjectBuilder_EntityBase objectBuilder)
        {
            //MyAPIGateway.Utilities.ShowMessage("", "Init");
            var thrust = Entity as IMyThrust;
            if (!_subtypes.Contains(thrust.BlockDefinition.SubtypeName)) return;

            _thrust = thrust;
            NeedsUpdate = MyEntityUpdateEnum.BEFORE_NEXT_FRAME;
        }

        public override void UpdateOnceBeforeFrame()
        {
            if (((MyThrust)_thrust).IsPreview) return;

            var forward = _thrust.Orientation.Forward;
            _direction = Base6Directions.GetVector(forward);
            _grid = _thrust.CubeGrid;

            NeedsUpdate = MyEntityUpdateEnum.EACH_FRAME;
        }

        public override void UpdateBeforeSimulation()
        {
            if (_thrust?.CubeGrid?.Physics == null || !_thrust.IsFunctional || !_thrust.Enabled || _thrust.MarkedForClose || _thrust.CubeGrid.IsStatic)
                return;

            var grid = _thrust.CubeGrid;
            if (grid != _grid)
            {
                UpdateOnceBeforeFrame();
            }
            
            var velocity = grid.Physics.LinearVelocity;
            var localV = Vector3.RotateAndScale(velocity, grid.PositionComp.WorldMatrixNormalizedInv);
            var velComponent = Vector3.Multiply(localV, _direction);
            var speed = - velComponent.Sum;
            var limit = speed > THRESHOLD;
            var limited = MyUtils.IsEqual(_thrust.ThrustMultiplier, 0.01f);
            //MyAPIGateway.Utilities.ShowNotification(_thrust.ThrustMultiplier.ToString(), 16);

            if (limit != limited)
            {
                _thrust.ThrustMultiplier = limit ? 0.01f : 1f;
            }

        }

    }
}