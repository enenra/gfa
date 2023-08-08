using static Scripts.Structure;
using static Scripts.Structure.WeaponDefinition;
using static Scripts.Structure.WeaponDefinition.ModelAssignmentsDef;
using static Scripts.Structure.WeaponDefinition.HardPointDef;
using static Scripts.Structure.WeaponDefinition.HardPointDef.Prediction;
using static Scripts.Structure.WeaponDefinition.TargetingDef.BlockTypes;
using static Scripts.Structure.WeaponDefinition.TargetingDef.Threat;
using static Scripts.Structure.WeaponDefinition.TargetingDef;
using static Scripts.Structure.WeaponDefinition.TargetingDef.CommunicationDef.Comms;
using static Scripts.Structure.WeaponDefinition.TargetingDef.CommunicationDef.SecurityMode;
using static Scripts.Structure.WeaponDefinition.HardPointDef.HardwareDef;
using static Scripts.Structure.WeaponDefinition.HardPointDef.HardwareDef.HardwareType;
using System.Collections.Generic;

namespace Scripts {
    partial class Parts {
        WeaponDefinition GFA_Weapon_MG7ProtonTorpedoLauncher => new WeaponDefinition
        {
            Assignments = new ModelAssignmentsDef
            {
                MountPoints = new[] {
                    new MountPointDef {
                        SubtypeId = "GFA_SG_XWing_TorpLauncher",
                        MuzzlePartId = "None",
                        AzimuthPartId = "None",
                        ElevationPartId = "None",
                        DurabilityMod = 0.25f,
                    },
                    new MountPointDef {
                        SubtypeId = "GFA_SG_XWing_TorpLauncherMirror",
                        MuzzlePartId = "None",
                        AzimuthPartId = "None",
                        ElevationPartId = "None",
                        DurabilityMod = 0.25f,
                    },
                    
                 },
                Muzzles = new[] {
                    "muzzle_missile_001",
                },
                Scope = "scope_001",
            },         
            Targeting = new TargetingDef
            {
                Threats = new[] {
                    Grids,
                },
                SubSystems = new[] {
                    Power, Utility, Offense, Thrust, Production, Any, 
                },
            },            
            HardPoint = new HardPointDef
            {
                PartName = "MG7 Proton Torpedo Launcher", 
                DeviateShotAngle = 0.15f,
                AimingTolerance = 40f,
                Ui = new UiDef
                {
                    ToggleGuidance = true,
                },
                Ai = new AiDef
                {
                    TrackTargets = false, // Whether this weapon tracks its own targets, or (for multiweapons) relies on the weapon with PrimaryTracking enabled for target designation. Turrets Need this set to True.
                    TurretAttached = false, // Whether this weapon is a turret and should have the UI and API options for such. Turrets Need this set to True.
                    TurretController = false, // Whether this weapon can physically control the turret's movement. Turrets Need this set to True.
                    PrimaryTracking = false, // For multiweapons: whether this weapon should designate targets for other weapons on the platform without their own tracking.
                    LockOnFocus = false, // If enabled, weapon will only fire at targets that have been HUD selected AND locked onto by pressing Numpad 0.
                    SuppressFire = true, // If enabled, weapon can only be fired manually.
                    DefaultLeadGroup = 2, // Default LeadGroup setting, range 0-5, 0 is disables lead group.  Only useful for fixed weapons or weapons set to OverrideLeads.
                    TargetGridCenter = false, // Does not target blocks, instead it targets grid center.
                },
                HardWare = new HardwareDef
                {
                    InventorySize = 1.5f,
                    Type = BlockWeapon,
                },
                Loading = new LoadingDef
                {
                    RateOfFire = 60,
                    BarrelsPerShot = 1,
                    TrajectilesPerBarrel = 1,
                    ReloadTime = 480,
                    MagsToLoad = 1,
                },
                Audio = new HardPointAudioDef
                {
                    FiringSound = "_GFA_ProtonTorpedo_Shot",
                    FiringSoundPerShot = true,
                },
                Graphics = new HardPointParticleDef
                {
                    Effect1 = new ParticleDef
                    {
                        Name = "GFA_Particle_ProtonTorpedo_Shot",
                        Extras = new ParticleOptionDef
                        {
                            Loop = true,
                            Restart = true,
                            MaxDistance = 800,
                            MaxDuration = 0,
                            Scale = 0.5f,
                        },
                    },
                },
            },
            Ammos = new[] {
                GFA_Ammo_MG7ProtonTorpedo,
            },
        };
    }
}
