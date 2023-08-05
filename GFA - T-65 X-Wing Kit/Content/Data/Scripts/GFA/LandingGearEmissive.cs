using VRage.Game.Components;
using Sandbox.ModAPI;
using VRage.ObjectBuilders;
using VRage.ModAPI;
using VRageMath;
using Sandbox.Common.ObjectBuilders;
using SpaceEngineers.Game.ModAPI;
using LandingGearMode = SpaceEngineers.Game.ModAPI.Ingame.LandingGearMode;
using System.Collections.Generic;
using VRage.Game.Entity;
using System;
using VRage.Utils;

namespace enenra.LandingGearEmissive
{
    [MyEntityComponentDescriptor(typeof(MyObjectBuilder_LandingGear), false)]

    public class Container : MyGameLogicComponent
    {
        IMyLandingGear m_block;
        List<MyEntitySubpart> subparts = new List<MyEntitySubpart>();
        bool autoLockEnabled;

        private const string EMISSIVE_MATERIAL_NAME = "Emissive";
        private Color DAMAGED = new Color(0, 0, 0);
        private Color DISABLED = new Color(255, 0, 0);
        private Color WORKING = new Color(128, 128, 128);
        private Color CONSTRAINT = new Color(255, 255, 0);
        private Color LOCKED = new Color(10, 255, 0);
        private Color AUTOLOCK = new Color(0, 255, 255);

        bool IsWorking => m_block.IsWorking;

        public override void Init(MyObjectBuilder_EntityBase objectBuilder)
        {
            if (MyAPIGateway.Session == null)
                return;

            if (MyAPIGateway.Utilities.IsDedicated && MyAPIGateway.Multiplayer.IsServer)
                return;

            m_block = Entity as IMyLandingGear;
            autoLockEnabled = m_block.AutoLock;

            var subtype = m_block.BlockDefinition.SubtypeName;
            if (!subtype.StartsWith("GFA_LG_") && !subtype.StartsWith("GFA_SG_"))
            {
                m_block = null;
                return;
            }

            NeedsUpdate |= MyEntityUpdateEnum.BEFORE_NEXT_FRAME;

            m_block.IsWorkingChanged += OnIsWorkingChanged;
            m_block.LockModeChanged += OnLockModeChanged;
            m_block.PropertiesChanged += OnPropertiesChanged;
            base.Init(objectBuilder);
        }

        public override void UpdateOnceBeforeFrame()
        {
            bool aqdVisualsPresent = false;
            bool emissiveColorsPresent = false;

            GetSubpartsRecursive((MyEntity)m_block, subparts);
            if (subparts.Count < 1)
            {
                m_block = null;
                return;
            }

            foreach (var mod in MyAPIGateway.Session.Mods)
            {
                if (aqdVisualsPresent && emissiveColorsPresent)
                    break;

                if (mod.PublishedFileId == 2711430394) // AQD - Emissive Colors
                    aqdVisualsPresent = true;
                else if (mod.PublishedFileId == 2212516940) // Emissive Colors - Red / Green Color Vision Deficiency
                    emissiveColorsPresent = true;
            }

            if (aqdVisualsPresent)
            {
                DAMAGED = new Color(0, 0, 0);
                DISABLED = new Color(171, 42, 29);
                WORKING = new Color(128, 128, 128);
                CONSTRAINT = new Color(169, 76, 21);
                LOCKED = new Color(60, 163, 33);
                AUTOLOCK = new Color(54, 90, 161);
            }
            else if (emissiveColorsPresent)
            {
                DAMAGED = new Color(0, 0, 0);
                DISABLED = new Color(255, 0, 0);
                WORKING = new Color(128, 128, 128);
                CONSTRAINT = new Color(175, 45, 0);
                LOCKED = new Color(10, 255, 25);
                AUTOLOCK = new Color(0, 255, 255);
            }

            base.UpdateOnceBeforeFrame();
        }

        internal void GetSubpartsRecursive(MyEntity entity, List<MyEntitySubpart> subparts)
        {
            if (entity.Subparts.Count == 0)
                return;

            foreach (var part in entity.Subparts.Values)
            {
                subparts.Add(part);
                GetSubpartsRecursive(part, subparts);
            }
        }

        private void SetEmissiveColor(Color color)
        {
            foreach (var sp in subparts)
            {
                sp.SetEmissiveParts(EMISSIVE_MATERIAL_NAME, color, 1f);
            }
        }

        private void UpdateEmissives()
        {
            var isWorking = m_block.IsWorking;
            var isFunctional = m_block.IsFunctional;
            var landingGearMode = m_block.LockMode;

            if (isWorking)
            {
                switch (landingGearMode)
                {
                    case LandingGearMode.Unlocked:
                        if (autoLockEnabled)
                        {
                            SetEmissiveColor(AUTOLOCK);
                        }
                        else
                        {
                            SetEmissiveColor(WORKING);
                        }
                        break;

                    case LandingGearMode.ReadyToLock:
                        SetEmissiveColor(CONSTRAINT);
                        break;

                    case LandingGearMode.Locked:
                        SetEmissiveColor(LOCKED);
                        break;
                }
            }
            else
            {
                if (isFunctional)
                {
                    SetEmissiveColor(DISABLED);
                }
                else
                {
                    SetEmissiveColor(DAMAGED);
                }
            }

        }

        public override void Close()
        {
            if (m_block != null)
            {
                m_block.IsWorkingChanged -= OnIsWorkingChanged;
                m_block.LockModeChanged -= OnLockModeChanged;
                m_block.PropertiesChanged -= OnPropertiesChanged;
                m_block = null;
            }

            base.Close();
        }

        private void OnIsWorkingChanged(VRage.Game.ModAPI.IMyCubeBlock obj)
        {
            UpdateEmissives();
        }

        private void OnLockModeChanged(IMyLandingGear sender, LandingGearMode newMode)
        {
            UpdateEmissives();
        }

        private void OnPropertiesChanged(IMyTerminalBlock block)
        {
            if (m_block.AutoLock != autoLockEnabled)
            {
                autoLockEnabled = m_block.AutoLock;
                UpdateEmissives();
            }
        }
    }
}