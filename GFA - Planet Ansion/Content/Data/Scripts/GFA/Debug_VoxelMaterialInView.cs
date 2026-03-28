using System;
using System.Collections.Generic;
using Sandbox.Game.Entities;
using Sandbox.ModAPI;
using VRage.Game;
using VRage.Game.Components;
using VRage.Game.ModAPI;
using VRage.Utils;
using VRageMath;
using CollisionLayers = Sandbox.Engine.Physics.MyPhysics.CollisionLayers;

namespace Digi.Experiments
{
    [MySessionComponentDescriptor(MyUpdateOrder.AfterSimulation)]
    public class Debug_VoxelMaterialInView : MySessionComponentBase
    {
        MyStringId MaterialDot = MyStringId.GetOrCompute("WhiteDot");
        List<IHitInfo> Hits = new List<IHitInfo>();

        public override void UpdateAfterSimulation()
        {
            try
            {
                var cam = MyAPIGateway.Session.Camera.WorldMatrix;
                Vector3D from = cam.Translation + cam.Forward * 1;
                Vector3D to = cam.Translation + cam.Forward * 30;

                Hits.Clear();
                MyAPIGateway.Physics.CastRay(from, to, Hits, CollisionLayers.CollisionLayerWithoutCharacter);

                foreach(var hit in Hits)
                {
                    var voxel = hit.HitEntity as MyVoxelBase;
                    if(voxel != null)
                    {
                        Vector3D pos = hit.Position;
                        MyTransparentGeometry.AddPointBillboard(MaterialDot, Color.Cyan, pos, 0.2f, 0);
                        MyVoxelMaterialDefinition matDef = voxel.GetMaterialAt(ref pos);
                        MyAPIGateway.Utilities.ShowNotification($"VoxelMaterial: {matDef?.Id.SubtypeName}", 16);
                        break;
                    }
                }

                Hits.Clear();
            }
            catch(Exception e)
            {
                LogError(e);
            }
        }

        void LogError(Exception e)
        {
            MyLog.Default.WriteLineAndConsole($"{ModContext.ModName}: {e.Message}\n{e.StackTrace}");
            if(MyAPIGateway.Session?.Player != null)
                MyAPIGateway.Utilities.ShowNotification($"[ ERROR: {ModContext.ModName}: error at {GetType().Name} ]", 10000, MyFontEnum.Red);
        }
    }
}
