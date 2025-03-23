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
        WeaponDefinition GFA_Weapon_TurretDroid => new WeaponDefinition
        {
            Assignments = new ModelAssignmentsDef
            {
                MountPoints = new[] {
                    new MountPointDef {
                        SubtypeId = "GFA_LG_TurretDroid",
                        MuzzlePartId = "elevation",
                        AzimuthPartId = "azimuth",
                        ElevationPartId = "elevation",
                        DurabilityMod = 0.25f,
                        IconName = "GFA_Filter_AmmoMag_Sidearm.dds"
                    },

                 },
                Muzzles = new[] {
                    "muzzle_barrel_1",
                    "muzzle_barrel_2",
                },
                Scope = "muzzle_barrel_1",
            },
            Targeting = new TargetingDef
            {
                Threats = new[] {
                    Grids, Projectiles, Characters,// Types of threat to engage: Grids, Projectiles, Characters, Meteors, Neutrals, ScanRoid, ScanPlanet, ScanFriendlyCharacter, ScanFriendlyGrid, ScanEnemyCharacter, ScanEnemyGrid, ScanNeutralCharacter, ScanNeutralGrid, ScanUnOwnedGrid, ScanOwnersGrid
                },
                SubSystems = new[] {
                    Thrust, Utility, Offense, Power, Production, Any, // Subsystem targeting priority: Offense, Utility, Power, Production, Thrust, Jumping, Steering, Any
                },
                MaxTargetDistance = 800,
            },
            HardPoint = new HardPointDef
            {
                PartName = "Turret Droid",
                DeviateShotAngle = 0.1f, // Projectile inaccuracy in degrees.
                AimingTolerance = 1f, // How many degrees off target a turret can fire at. 0 - 180 firing angle.
                AimLeadingPrediction = Accurate, // Level of turret aim prediction; Off, Basic, Accurate, Advanced
                AddToleranceToTracking = false, // Allows turret to track to the edge of the AimingTolerance cone instead of dead centre.
                Ui = new UiDef
                {
                    EnableOverload = false, // Enables terminal option to turn on Overload; this allows energy weapons to double damage per shot, at the cost of quadrupled power draw and heat gain, and 2% self damage on overheat.
                },
                Ai = new AiDef
                {
                    DefaultLeadGroup = 1, // Default LeadGroup setting, range 0-5, 0 is disables lead group.  Only useful for fixed weapons or weapons set to OverrideLeads.
                    TrackTargets = true,
                    TurretAttached = true,
                    TurretController = true,
                },
                HardWare = new HardwareDef
                {
                    RotateRate = 0.01f,
                    ElevateRate = 0.01f,
                    MinAzimuth = -180,
                    MaxAzimuth = 180,
                    MinElevation = -10,
                    MaxElevation = 50,
                    InventorySize = 0.6f,
                    Type = BlockWeapon,
                },
                Other = new OtherDef
                {
                    Debug = false, // Force enables debug mode.
                },
                Loading = new LoadingDef
                {
                    RateOfFire = 360, // Set this to 3600 for beam weapons. This is how fast your Gun fires.
                    BarrelsPerShot = 1, // How many muzzles will fire a projectile per fire event.
                    TrajectilesPerBarrel = 1, // Number of projectiles per muzzle per fire event.
                    SkipBarrels = 0, // Number of muzzles to skip after each fire event.
                    ReloadTime = 60*3, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    MagsToLoad = 1, // Number of physical magazines to consume on reload.
                    DelayUntilFire = 0, // How long the weapon waits before shooting after being told to fire. Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    HeatPerShot = 5, // Heat generated per shot.
                    MaxHeat = 800, // Max heat before weapon enters cooldown (70% of max heat).
                    Cooldown = 0.8f, // Percentage of max heat to be under to start firing again after overheat; accepts 0 - 0.95
                    HeatSinkRate= 40, // Amount of heat lost per second.
                    ShotsInBurst = 0, // Use this if you don't want the weapon to fire an entire physical magazine in one go. Should not be more than your magazine capacity.
                    StayCharged = true, // Will start recharging whenever power cap is not full.
                },
                Audio = new HardPointAudioDef
                {
                    PreFiringSound = "", // Audio for warmup effect.
                    FiringSound = "_GFA_TurretDroid_Shot", // Audio for firing.
                    FiringSoundPerShot = true, // Whether to replay the sound for each shot, or just loop over the entire track while firing.
                    FireSoundEndDelay = 0, // How long the firing audio should keep playing after firing stops. Measured in game ticks(6 = 100ms, 60 = 1 seconds, etc..).
                    FireSoundNoBurst = false, // Don't stop firing sound from looping when delaying after burst.
                },
                Graphics = new HardPointParticleDef
                {
                    Effect1 = new ParticleDef
                    {
                        Name = "GFA_Particle_LaserCannon_Muzzle_Red", // SubtypeId of muzzle particle effect.
                        Offset = Vector(x: 0, y: 0, z: 0), // Offsets the effect from the muzzle empty.
                        DisableCameraCulling = false, // If not true will not cull when not in view of camera, be careful with this and only use if you know you need it
                        Extras = new ParticleOptionDef
                        {
                            Loop = true, // Set this to the same as in the particle sbc!
                            Restart = true, // Whether to end a looping effect instantly when firing stops.
                            MaxDistance = 800,
                            MaxDuration = 0.1f,
                            Scale = 0.25f, // Scale of effect.
                        },
                    },
                },
            },
            Ammos = new[] {
                GFA_Ammo_TurretDroid_Red,
                GFA_Ammo_TurretDroid_Green
            },
            Animations = GFA_Animation_TurretDroid,
        };
    }
}
