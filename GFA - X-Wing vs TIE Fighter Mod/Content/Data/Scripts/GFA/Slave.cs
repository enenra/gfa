using System;
using System.Collections.Generic;
using System.IO;
using Sandbox.Game.Entities;
using Sandbox.ModAPI;
using VRage.Game;
using VRage.Game.Components;
using VRage.Game.Entity;
using VRage.Game.ModAPI;
using VRage.ModAPI;
using VRage.Utils;
using VRageMath;

namespace Scripts
{
    [MySessionComponentDescriptor(MyUpdateOrder.BeforeSimulation, 999999)]
    public class Session : MySessionComponentBase
    {
        private const double CombatRadius = 2500;
        private const double CombatNearEdge = CombatRadius - 1;
        private readonly BoundingSphereD _combatNearSphere = new BoundingSphereD(Vector3D.Zero, CombatNearEdge);
        private BoundingSphereD _combatMinSphere = new BoundingSphereD(Vector3D.Zero, CombatRadius);
        private BoundingSphereD _combatMaxSphere = new BoundingSphereD(Vector3D.Zero, CombatRadius + 10000);

        private int _count;
        private int _fastStart;
        private readonly List <MyEntity> _managedEntities = new List<MyEntity>(1000);
        private MyEntity _sphereEntity;
        
        public override void LoadData()
        {
        }
        protected override void UnloadData()
        {
        }
        public override void BeforeStart()
        {
          // if (!MyAPIGateway.Utilities.IsDedicated)
          // {
          //     _sphereEntity = GetSphereEntity();
          // }
        }
        public override void UpdateBeforeSimulation()
        {
            _count++;
            if (_count - _fastStart < 300 || _count % 100 == 0)
            {
                //RefreshVisualState();
                _managedEntities.Clear();
                MyGamePruningStructure.GetAllTopMostEntitiesInSphere(ref _combatMaxSphere, _managedEntities, MyEntityQueryType.Dynamic);
                MyAPIGateway.Parallel.For(0, _managedEntities.Count, i =>
                {
                    try
                    {
                        var ent = _managedEntities[i];
                        if (!ShouldProcessEntity(ent))
                        {
                            return;
                        }
                        ApplyForcesToEntity(ent);
                    }
                    catch (Exception e)
                    {
                        MyLog.Default.WriteLine($"An exception occurred while processing entity {i}: {e.Message}");
                    }
                });
            }
        }
        private readonly HashSet<Type> _skipEntityTypes = new HashSet<Type> { /* Add pre-ignored Types Here */ };

        private bool ShouldProcessEntity(MyEntity ent)
        {
            Type entType = ent.GetType();

            // Fast check against cache
            if (_skipEntityTypes.Contains(entType) || ent.MarkedForClose || ent.IsPreview || ent.Physics == null || ent.Physics.IsPhantom || !ent.InScene)
            {
                return false;
            }

            var grid = ent as MyCubeGrid;
            var player = ent as IMyCharacter;
            if (grid == null && player == null)
            {
                // Cache this type for future fast checks
                _skipEntityTypes.Add(entType);
                return false;
            }

            var entVolume = ent.PositionComp.WorldVolume;
            return entVolume.Contains(_combatNearSphere) != ContainmentType.Contains;
        }
        private void ApplyForcesToEntity(MyEntity ent)
        {
            var grid = ent as MyCubeGrid;
            var pos = grid?.Physics?.CenterOfMassWorld ?? ent.PositionComp.WorldVolume.Center;
            var dir = Vector3D.Zero - pos;
            Vector3D dirNorm;
            Vector3D.Normalize(ref dir, out dirNorm);
            var force = dirNorm * ((grid?.Physics?.Mass ?? ent.Physics.Mass) *
            MyUtils.GetSmallestDistanceToSphereAlwaysPositive(ref pos, ref _combatMinSphere));
            ent.Physics.AddForce(MyPhysicsForceType.APPLY_WORLD_FORCE, force, pos, Vector3.Zero);
            _fastStart = _count;
        }
    }
}
