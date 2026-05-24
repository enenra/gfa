using Sandbox.Game.Entities;
using Sandbox.ModAPI;
using SpaceEngineers.Game.ModAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VRage.Game.Components;
using VRage.Game.ModAPI;
using VRage.ModAPI;
using VRageMath;

namespace GFA
{
    [MySessionComponentDescriptor(MyUpdateOrder.AfterSimulation)]
    internal class SwitchSessionLogic : MySessionComponentBase
    {
        public override void LoadData()
        {
            MyAPIGateway.Entities.OnEntityAdd += OnEntityAdd;
        }

        protected override void UnloadData()
        {
            MyAPIGateway.Entities.OnEntityAdd -= OnEntityAdd;
        }

        public override void UpdateAfterSimulation()
        {
            if (_emissiveCache.Count == 0)
                return;

            var lightsToUpdate = new List<MyCubeBlock>();

            // Reset colors in case there was an assembly split
            // Do this first because it would collide with (switch).UpdateBlocks() emissive setting
            foreach (var block in _emissiveCache)
            {
                if (block is IMyInteriorLight)
                    lightsToUpdate.Add(block);
                else
                    block.UpdateEmissiveParts(block.Render.RenderObjectIDs[0], 1, Color.White, Color.White);
            }

            // Trigger switch update
            foreach (var light in lightsToUpdate)
                light.GameLogic?.GetAs<SwitchBlockLogic>()?.UpdateBlocks();
            _emissiveCache.Clear();
        }

        HashSet<MyCubeBlock> _emissiveCache = new HashSet<MyCubeBlock>();

        /// <summary>
        /// Trigger an update wave whenever blocks are added to ensure that color is properly propogated.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="slim"></param>
        private void OnBlockAddedGlobally<T>(T slim) where T : IMySlimBlock
        {
            if (slim.FatBlock == null || !SwitchBlockLogic.EmissiveSubtypes.Contains(slim.BlockDefinition.Id.SubtypeName))
                return;

            SwitchBlockLogic.RetrieveBlocks(slim, ref _emissiveCache, "GFA_LG_IMP_FakeLight");
        }

        private void OnEntityAdd(IMyEntity e)
        {
            if (!(e is IMyCubeGrid))
                return;
            ((IMyCubeGrid)e).OnBlockAdded += OnBlockAddedGlobally; // We don't need to unregister this because the game does it for us on unload.
            ((IMyCubeGrid)e).OnBlockRemoved += OnBlockAddedGlobally;
        }
    }
}
