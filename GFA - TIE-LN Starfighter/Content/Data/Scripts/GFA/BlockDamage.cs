using Sandbox.Game.Entities;
using Sandbox.ModAPI;
using System.Collections.Generic;
using VRage.Collections;
using VRage.Game;
using VRage.Game.Components;
using VRage.Game.Entity;
using VRage.Game.ModAPI;
using VRage.Utils;
using VRageMath;
using static NeighbouringBlockDamage.SubtypeData.Direction;

namespace NeighbouringBlockDamage
{
    [MySessionComponentDescriptor(MyUpdateOrder.AfterSimulation)]
    internal class BlockDamageSession : MySessionComponentBase
    {
        internal const bool DEBUG = false;
        internal const float DAMAGE = 10000f;

        internal readonly Dictionary<MyStringHash, SubtypeData> Subtypes = new Dictionary<MyStringHash, SubtypeData>()
        {
            { MyStringHash.GetOrCompute("GFA_SG_TIEFighter_Wing"), new SubtypeData(Forward, -2) },
            { MyStringHash.GetOrCompute("GFA_SG_TIEFighter_Wing_MG"), new SubtypeData(Forward, -2) },
            { MyStringHash.GetOrCompute("GFA_SG_TIEFighter_Wing_MG_Mirror"), new SubtypeData(Forward, 2) },
            { MyStringHash.GetOrCompute("GFA_SG_TIEFighter_Wing_IN"), new SubtypeData(Forward, -2) },
        };

        private readonly HashSet<IMySlimBlock> _grinded = new HashSet<IMySlimBlock>();
        private readonly ConcurrentCachingList<MyCubeBlock> _drawBlocks = new ConcurrentCachingList<MyCubeBlock>();

        public override void LoadData()
        {
            if (!MyAPIGateway.Multiplayer.IsServer)
                return;

            MyEntities.OnEntityCreate += OnEntityCreate;
        }

        private void OnEntityCreate(MyEntity entity)
        {
            var block = entity as MyCubeBlock;
            if (block == null || !Subtypes.ContainsKey(block.BlockDefinition.Id.SubtypeId))
                return;

            block.OnClose += OnClose;

            if (DEBUG)
            {
                _drawBlocks.Add(block);
            }
        }

        private void OnClose(MyEntity entity)
        {
            var block = entity as MyCubeBlock;

            if (DEBUG)
            {
                _drawBlocks.Remove(block);
            }

            var slim = (IMySlimBlock)block.SlimBlock;
            if (!slim.IsDestroyed || _grinded.Remove(slim))
                return;

            SubtypeData data;
            if (!Subtypes.TryGetValue(block.BlockDefinition.Id.SubtypeId, out data))
                return;

            var pos = GetDamageBlockPosition(block, data);

            MyCube cube;
            if (!block.CubeGrid.TryGetCube(pos, out cube))
                return;

            var damageSlim = (IMySlimBlock)cube.CubeBlock;
            damageSlim.DoDamage(DAMAGE, MyDamageType.Explosion, true);
        }

        public override void BeforeStart()
        {
            if (!MyAPIGateway.Multiplayer.IsServer)
                return;

            MyAPIGateway.Session.DamageSystem.RegisterDestroyHandler(0, OnDestroy);
        }

        internal void OnDestroy(object target, MyDamageInformation info)
        {
            var block = target as IMySlimBlock;
            if (block == null)
                return;

            if (info.Type != MyDamageType.Grind)
                return;

            if (!Subtypes.ContainsKey(block.BlockDefinition.Id.SubtypeId))
                return;

            _grinded.Add(block);
        }

        public override void UpdateAfterSimulation()
        {
            if (!DEBUG || _drawBlocks.Count == 0)
                return;

            _drawBlocks.ApplyChanges();
            foreach (var block in _drawBlocks)
            {
                SubtypeData data;
                if (!Subtypes.TryGetValue(block.BlockDefinition.Id.SubtypeId, out data))
                    continue;

                var localPos = GetDamageBlockPosition(block, data);
                var worldPos = block.CubeGrid.GridIntegerToWorld(localPos);
                var matrix = MatrixD.CreateTranslation(worldPos);
                var color = Color.BlueViolet;
                MySimpleObjectDraw.DrawTransparentSphere(ref matrix, 0.5f, ref color, MySimpleObjectRasterizer.Solid, 20, null, MyStringId.GetOrCompute("Square"), 0.02f);
            }
        }

        protected override void UnloadData()
        {
            if (!MyAPIGateway.Multiplayer.IsServer)
                return;

            MyEntities.OnEntityCreate -= OnEntityCreate;
        }

        internal Vector3I GetDamageBlockPosition(IMyCubeBlock block, SubtypeData data)
        {
            var blockMatrix = block.PositionComp.LocalMatrixRef;
            Vector3I axis;
            switch (data.Axis)
            {
                case Forward:
                    axis = (Vector3I)blockMatrix.Forward;
                    break;
                case Right:
                    axis = (Vector3I)blockMatrix.Right;
                    break;
                case Up:
                    axis = (Vector3I)blockMatrix.Up;
                    break;
                default:
                    axis = Vector3I.Zero;
                    break;
            }

            return block.SlimBlock.Position + (data.Offset * axis);
        }
    }

    internal class SubtypeData
    {
        internal enum Direction
        {
            Forward,
            Right,
            Up,
        }

        internal readonly Direction Axis;
        internal readonly int Offset;

        internal SubtypeData(Direction axis, int offset)
        {
            Axis = axis;
            Offset = offset;
        }
    }
}
