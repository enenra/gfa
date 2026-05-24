using Sandbox.Common.ObjectBuilders;
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
using VRage.ObjectBuilders;
using VRageMath;

namespace GFA
{
    [MyEntityComponentDescriptor(typeof(MyObjectBuilder_InteriorLight), false, "GFA_LG_IMP_FakeLight")]
    internal class SwitchBlockLogic : MyGameLogicComponent
    {
        public static readonly string[] EmissiveSubtypes = new[]
        {
            "GFA_LG_IMP_HangarPylon",
            "GFA_LG_IMP_HangarPylonBottom",
            "GFA_LG_IMP_InteriorWall_Half_Inv",
            "GFA_LG_IMP_InteriorWall",
            "GFA_LG_IMP_InteriorWall_Mirror",
            "GFA_LG_IMP_InteriorWall2_Half_Inv",
            "GFA_LG_IMP_InteriorWall2_Half",
            "GFA_LG_IMP_InteriorWall2",
            "GFA_LG_IMP_InteriorWall2_Mirror",
        };

        private IMyInteriorLight _block;
        private Color _cachedColor = Color.White;

        public override void Init(MyObjectBuilder_EntityBase objectBuilder)
        {
            base.Init(objectBuilder);
            _block = (IMyInteriorLight) Entity;
            NeedsUpdate |= MyEntityUpdateEnum.BEFORE_NEXT_FRAME;
        }

        public override void UpdateOnceBeforeFrame()
        {
            base.UpdateOnceBeforeFrame();

            if(_block?.CubeGrid?.Physics == null)
                return;

            NeedsUpdate |= MyEntityUpdateEnum.EACH_FRAME;
        }

        HashSet<MyCubeBlock> _emissiveCache = new HashSet<MyCubeBlock>();
        public override void UpdateAfterSimulation()
        {
            if (_cachedColor == _block.Color && (_block.Enabled || _cachedColor == Color.Black))
                return;
            _cachedColor = _block.Enabled ? _block.Color : Color.Black;

            UpdateBlocks();
        }

        public override void MarkForClose()
        {
            base.MarkForClose();

            _cachedColor = Color.White;
            UpdateBlocks();
        }

        public void UpdateBlocks()
        {
            RetrieveBlocks(_block.SlimBlock, ref _emissiveCache, "GFA_LG_IMP_FakeLight");

            List<Vector3I> otherLights = new List<Vector3I>();
            foreach (var light in _emissiveCache.Where(block => block is IMyInteriorLight && block != _block))
                otherLights.Add(light.Position);

            foreach (var block in _emissiveCache)
            {
                // Ignore blocks that are closer to other lights.
                double manhattanDist = block.Position.RectangularDistance(_block.Position);
                if (!otherLights.Any(lightPos => block.Position.RectangularDistance(lightPos) < manhattanDist))
                    block.UpdateEmissiveParts(block.Render.RenderObjectIDs[0], 1, _cachedColor, Color.White);
            }

            _emissiveCache.Clear();
        }

        /// <summary>
        /// Non-recursive attached block retriever.
        /// </summary>
        /// <param name="block"></param>
        /// <param name="attached"></param>
        public static void RetrieveBlocks(IMySlimBlock block, ref HashSet<MyCubeBlock> attached, params string[] additionalSubtypes)
        {
            var neighborCache = new HashSet<IMySlimBlock>();
            var validNeighborCache = new Queue<IMySlimBlock>();

            validNeighborCache.Enqueue(block);
            while (validNeighborCache.Count > 0)
            {
                GetNearBlocks(validNeighborCache.Dequeue(), neighborCache);

                foreach (var neighbor in neighborCache)
                    if (neighbor.FatBlock != null && (EmissiveSubtypes.Contains(neighbor.BlockDefinition.Id.SubtypeName) || additionalSubtypes.Contains(neighbor.BlockDefinition.Id.SubtypeName)) && attached.Add((MyCubeBlock) neighbor.FatBlock))
                        validNeighborCache.Enqueue(neighbor);

                neighborCache.Clear();
            }
        }

        public static void GetNearBlocks(IMySlimBlock block, ICollection<IMySlimBlock> collection)
        {
            // Switches should only connect to one block. We're using this instead of .GetNeighbors() because that method doesn't work on block deletion.
            if (block.BlockDefinition.Id.SubtypeName == "GFA_LG_IMP_FakeLight")
            {
                Matrix localOrientation;
                block.Orientation.GetMatrix(out localOrientation);

                var adajent = block.CubeGrid.GetCubeBlock((Vector3I)Vector3D.Rotate(Vector3D.Down, localOrientation) + block.Position);
                if (adajent != null)
                    collection.Add(adajent);
                return;
            }

            for (int x = block.Min.X - 1; x <= block.Max.X + 1; x++)
            {
                bool xAtLimit = x == block.Min.X - 1 || x == block.Max.X + 1;

                for (int y = block.Min.Y - 1; y <= block.Max.Y + 1; y++)
                {
                    bool yAtLimit = y == block.Min.Y - 1 || y == block.Max.Y + 1;

                    for (int z = block.Min.Z - 1; z <= block.Max.Z + 1; z++)
                    {
                        bool zAtLimit = z == block.Min.Z - 1 || z == block.Max.Z + 1;
                        int numAtLimit = 0;
                        if (xAtLimit)
                            numAtLimit++;
                        if (yAtLimit)
                            numAtLimit++;
                        if (zAtLimit)
                            numAtLimit++;
                        if (numAtLimit > 1)
                            continue;

                        var adajent = block.CubeGrid.GetCubeBlock(new Vector3I(x, y, z));
                        if (adajent != null)
                        {
                            if (adajent.BlockDefinition.Id.SubtypeName == "GFA_LG_IMP_FakeLight")
                            {
                                Matrix localOrientation;
                                adajent.Orientation.GetMatrix(out localOrientation);

                                if (!((Vector3I)Vector3D.Rotate(Vector3D.Down, localOrientation) + adajent.Position).IsInsideInclusiveEnd(block.Min, block.Max))
                                    continue;
                            }
                            collection.Add(adajent);
                        }
                    }
                }
            }
        }
    }
}
