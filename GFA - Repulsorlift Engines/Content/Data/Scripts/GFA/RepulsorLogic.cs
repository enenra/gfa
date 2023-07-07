using Sandbox.Common.ObjectBuilders;
using Sandbox.Game.Entities;
using Sandbox.ModAPI;
using System.Collections.Generic;
using VRage.Game.Components;
using VRage.ModAPI;
using VRage.ObjectBuilders;
using VRageMath;

namespace Repulsors
{
    [MyEntityComponentDescriptor(typeof(MyObjectBuilder_Thrust), false)]
    class RepulsorLogic : MyGameLogicComponent
    {
        private readonly HashSet<string> _subtypes = new HashSet<string>()
        {
            "GFA_LG_RepulsorSmall",
            "GFA_SG_RepulsorSmall"
        };

        private const int THRESHOLD = 25;

        private IMyThrust _thrust;
        private Vector3 _direction;
        private bool _limited;

        public override void Init(MyObjectBuilder_EntityBase objectBuilder)
        {
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

            NeedsUpdate = MyEntityUpdateEnum.EACH_FRAME;
        }

        public override void UpdateBeforeSimulation()
        {
            if (_thrust?.CubeGrid?.Physics == null || !_thrust.IsFunctional || !_thrust.Enabled || _thrust.MarkedForClose || _thrust.CubeGrid.IsStatic)
                return;

            var grid = _thrust.CubeGrid;
            var velocity = grid.Physics.LinearVelocity;
            var localV = Vector3.RotateAndScale(velocity, grid.PositionComp.WorldMatrixNormalizedInv);
            var velComponent = Vector3.Multiply(localV, _direction);
            var speed = - velComponent.Sum;
            var limit = speed > THRESHOLD;

            if (limit != _limited)
            {
                _thrust.ThrustMultiplier = limit ? 0.001f : 1f;
                _limited = limit;
            }

        }

    }
}