using Sandbox.Game.Entities;
using Sandbox.ModAPI;
using System.Collections.Generic;
using VRage.Game;
using VRage.Game.Components;
using VRage.Game.Entity;
using VRage.Game.ModAPI;
using VRage.Utils;
using VRageMath;

namespace NeighbouringBlockDamage
{
    [MySessionComponentDescriptor(MyUpdateOrder.NoUpdate)]
    internal class BlockDamageSession : MySessionComponentBase
    {
        internal readonly HashSet<IMySlimBlock> Grinded = new HashSet<IMySlimBlock>();

        internal readonly HashSet<MyStringHash> Subtypes = new HashSet<MyStringHash>()
        { 
            MyStringHash.GetOrCompute("GFA_SG_TIEFighter_Wing"),
        };

        public override void LoadData()
        {
            if (!MyAPIGateway.Multiplayer.IsServer)
                return;

            MyEntities.OnEntityCreate += OnEntityCreate;
        }

        private void OnEntityCreate(MyEntity entity)
        {
            var block = entity as MyCubeBlock;
            if (block == null || !Subtypes.Contains(block.BlockDefinition.Id.SubtypeId))
                return;

            block.OnClose += OnClose;
        }

        private void OnClose(MyEntity entity)
        {
            var block = entity as IMyCubeBlock;
            if (!block.SlimBlock.IsDestroyed)
                return;

            if (Grinded.Remove(block.SlimBlock))
                return;

            var grid = block.CubeGrid as MyCubeGrid;
            var pos = block.SlimBlock.Position;
            pos += 2 * (Vector3I)block.PositionComp.LocalMatrixRef.Backward;

            MyCube cube;
            if (!grid.TryGetCube(pos, out cube))
                return;

            var slim = (IMySlimBlock)cube.CubeBlock;
            slim.DoDamage(10000f, MyDamageType.Explosion, true);
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

            if (!Subtypes.Contains(block.BlockDefinition.Id.SubtypeId))
                return;

            Grinded.Add(block);
        }

        protected override void UnloadData()
        {
            if (!MyAPIGateway.Multiplayer.IsServer)
                return;

            MyEntities.OnEntityCreate -= OnEntityCreate;
        }
    }
}