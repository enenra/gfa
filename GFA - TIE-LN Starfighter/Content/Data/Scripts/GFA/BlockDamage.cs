using Sandbox.ModAPI;
using System.Collections.Generic;
using VRage.Game;
using VRage.Game.Components;
using VRage.Game.ModAPI;
using VRage.Utils;

namespace Neighbouring_Block_Damage
{
    [MySessionComponentDescriptor(MyUpdateOrder.NoUpdate)]
    internal class BlockDamageSession : MySessionComponentBase
    {
        private readonly List<IMySlimBlock> _blocks = new List<IMySlimBlock>();

        private readonly HashSet<MyStringHash> _subtypes = new HashSet<MyStringHash>()
        { 
            MyStringHash.GetOrCompute("GFA_SG_TIEFighter_Wing"),
        };

        public override void BeforeStart()
        {
            if (!MyAPIGateway.Multiplayer.IsServer)
                return;

            MyAPIGateway.Session.DamageSystem.RegisterBeforeDamageHandler(0, OnDestroy);
        }

        internal void OnDestroy(object target, ref MyDamageInformation info)
        {
            var block = target as IMySlimBlock;
            if (block == null) 
                return;

            if (!_subtypes.Contains(block.BlockDefinition.Id.SubtypeId))
                return;

            if (info.Type == MyDamageType.Grind)
                return;

            if (info.Amount < block.Integrity)
                return;

            block.GetNeighbours(_blocks);
            foreach (var slim in _blocks)
            {
                slim.DoDamage(10000, MyDamageType.Explosion, true);
            }
            _blocks.Clear();
        }
    }
}