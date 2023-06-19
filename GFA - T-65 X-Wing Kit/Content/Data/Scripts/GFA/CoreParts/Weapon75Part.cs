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
        // Don't edit above this line
        WeaponDefinition GFA_Weapon_XWing_LaserCannon_BottomLeft => new WeaponDefinition
        {
            Assignments = new ModelAssignmentsDef
            {
                MountPoints = new[] {
                    new MountPointDef {
                        SubtypeId = "GFA_SG_XWing_FireControl",
                        MuzzlePartId = "bottomleft_el",
                        AzimuthPartId = "sfoil_laser_bottomleft",
                        ElevationPartId = "bottomleft_el",
                        DurabilityMod = 0.25f,
                    },
                    
                 },
                Muzzles = new[] {
                    "muzzle_barrel_004",
                },
                Scope = "muzzle_barrel_004",
            },
            Targeting = new TargetingDef
            {
                Threats = new[] {
                    Grids, // Types of threat to engage: Grids, Projectiles, Characters, Meteors, Neutrals, ScanRoid, ScanPlanet, ScanFriendlyCharacter, ScanFriendlyGrid, ScanEnemyCharacter, ScanEnemyGrid, ScanNeutralCharacter, ScanNeutralGrid, ScanUnOwnedGrid, ScanOwnersGrid
                },
                SubSystems = new[] {
                    Thrust, Utility, Offense, Power, Production, Any, // Subsystem targeting priority: Offense, Utility, Power, Production, Thrust, Jumping, Steering, Any
                },
            },
            HardPoint = new HardPointDef
            {
                PartName = "KX9 Laser Cannon",
                DeviateShotAngle = 0.2f, // Projectile inaccuracy in degrees.
                AimingTolerance = 1f, // How many degrees off target a turret can fire at. 0 - 180 firing angle.
                AimLeadingPrediction = Accurate, // Level of turret aim prediction; Off, Basic, Accurate, Advanced
                AddToleranceToTracking = false, // Allows turret to track to the edge of the AimingTolerance cone instead of dead centre.
                Ui = new UiDef
                {
                    RateOfFire = true, // Enables terminal slider for changing rate of fire.
                    RateOfFireMin = 0.5f, // Sets the minimum limit for the rate of fire slider, default is 0.  Range is 0-1f.
                    EnableOverload = false, // Enables terminal option to turn on Overload; this allows energy weapons to double damage per shot, at the cost of quadrupled power draw and heat gain, and 2% self damage on overheat.
                },
                Ai = new AiDef
                {
                    TrackTargets = true, // Whether this weapon tracks its own targets, or (for multiweapons) relies on the weapon with PrimaryTracking enabled for target designation. Turrets Need this set to True.
                    TurretAttached = true, // Whether this weapon is a turret and should have the UI and API options for such. Turrets Need this set to True.
                    TurretController = true, // Whether this weapon can physically control the turret's movement. Turrets Need this set to True.
                    PrimaryTracking = false, // For multiweapons: whether this weapon should designate targets for other weapons on the platform without their own tracking.
                    SuppressFire = true, // If enabled, weapon can only be fired manually.
                    OverrideLeads = false, // Disable target leading on fixed weapons, or allow it for turrets.
                    DefaultLeadGroup = 0, // Default LeadGroup setting, range 0-5, 0 is disables lead group.  Only useful for fixed weapons or weapons set to OverrideLeads.
                },
                HardWare = new HardwareDef
                {
                    RotateRate = 0.1f, // Max traversal speed of azimuth subpart in radians per tick (0.1 is approximately 360 degrees per second).
                    ElevateRate = 0.1f, // Max traversal speed of elevation subpart in radians per tick.
                    MinAzimuth = -180,
                    MaxAzimuth = 180,
                    MinElevation = -40,
                    MaxElevation = 40,
                    HomeAzimuth = 0, // Default resting rotation angle
                    HomeElevation = 0, // Default resting elevation
                    InventorySize = 1f, // Inventory capacity in kL.
                    IdlePower = 0.25f, // Constant base power draw in MW.
                },
                Other = new OtherDef
                {
                    ConstructPartCap = 1, // Maximum number of blocks with this weapon on a grid; 0 = unlimited.
                    Debug = true, // Force enables debug mode.
                },
                Loading = new LoadingDef
                {
                    RateOfFire = 60, // Set this to 3600 for beam weapons. This is how fast your Gun fires.
                    BarrelsPerShot = 1, // How many muzzles will fire a projectile per fire event.
                    SkipBarrels = 0, // Number of muzzles to skip after each fire event.
                    ReloadTime = 1, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    MagsToLoad = 1, // Number of physical magazines to consume on reload.
                    DelayUntilFire = 0, // How long the weapon waits before shooting after being told to fire. Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    HeatPerShot = 1, // Heat generated per shot.
                    MaxHeat = 70000, // Max heat before weapon enters cooldown (70% of max heat).
                    Cooldown = .95f, // Percentage of max heat to be under to start firing again after overheat; accepts 0 - 0.95
                    HeatSinkRate = 9000000, // Amount of heat lost per second.
                    DegradeRof = false, // Progressively lower rate of fire when over 80% heat threshold (80% of max heat).
                    ShotsInBurst = 0, // Use this if you don't want the weapon to fire an entire physical magazine in one go. Should not be more than your magazine capacity.
                    DelayAfterBurst = 0, // How long to spend "reloading" after each burst. Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    StayCharged = true, // Will start recharging whenever power cap is not full.
                    MaxActiveProjectiles = 0, // Maximum number of drones in flight (only works for drone launchers)
                },
                Audio = new HardPointAudioDef
                {
                    PreFiringSound = "", // Audio for warmup effect.
                    FiringSound = "WepShipGatlingShot", // Audio for firing.
                    FiringSoundPerShot = true, // Whether to replay the sound for each shot, or just loop over the entire track while firing.
                    ReloadSound = "", // Sound SubtypeID, for when your Weapon is in a reloading state
                    NoAmmoSound = "",
                    HardPointRotationSound = "", // Audio played when turret is moving.
                    FireSoundEndDelay = 120, // How long the firing audio should keep playing after firing stops. Measured in game ticks(6 = 100ms, 60 = 1 seconds, etc..).
                    FireSoundNoBurst = true, // Don't stop firing sound from looping when delaying after burst.
                },
                Graphics = new HardPointParticleDef
                {
                    Effect1 = new ParticleDef
                    {
                        Name = "Muzzle_Flash_Large", // SubtypeId of muzzle particle effect.
                        Color = Color(red: 0, green: 0, blue: 0, alpha: 1), // Deprecated, set color in particle sbc.
                        Offset = Vector(x: 0, y: 0, z: 0), // Offsets the effect from the muzzle empty.
                        DisableCameraCulling = false, // If not true will not cull when not in view of camera, be careful with this and only use if you know you need it
                        Extras = new ParticleOptionDef
                        {
                            Loop = false, // Set this to the same as in the particle sbc!
                            Restart = false, // Whether to end a looping effect instantly when firing stops.
                            MaxDistance = 800,
                            MaxDuration = 0,
                            Scale = 1f, // Scale of effect.
                        },
                    },
                },
            },
            Ammos = new[] {
                GFA_Ammo_XWing_LaserCannon,
            },
        };
        WeaponDefinition GFA_Weapon_XWing_LaserCannon_BottomRight => new WeaponDefinition
        {
            Assignments = new ModelAssignmentsDef
            {
                MountPoints = new[] {
                    new MountPointDef {
                        SubtypeId = "GFA_SG_XWing_FireControl",
                        MuzzlePartId = "bottomright_el",
                        AzimuthPartId = "sfoil_laser_bottomright",
                        ElevationPartId = "bottomright_el",
                        DurabilityMod = 0.25f,
                    },
                    
                 },
                Muzzles = new[] {
                    "muzzle_barrel_003",
                },
                Scope = "muzzle_barrel_003",
            },
            Targeting = new TargetingDef
            {
                Threats = new[] {
                    Grids, // Types of threat to engage: Grids, Projectiles, Characters, Meteors, Neutrals, ScanRoid, ScanPlanet, ScanFriendlyCharacter, ScanFriendlyGrid, ScanEnemyCharacter, ScanEnemyGrid, ScanNeutralCharacter, ScanNeutralGrid, ScanUnOwnedGrid, ScanOwnersGrid
                },
                SubSystems = new[] {
                    Thrust, Utility, Offense, Power, Production, Any, // Subsystem targeting priority: Offense, Utility, Power, Production, Thrust, Jumping, Steering, Any
                },
            },
            HardPoint = new HardPointDef
            {
                PartName = "KX9 Laser Cannon",
                DeviateShotAngle = 0.2f, // Projectile inaccuracy in degrees.
                AimingTolerance = 1f, // How many degrees off target a turret can fire at. 0 - 180 firing angle.
                AimLeadingPrediction = Accurate, // Level of turret aim prediction; Off, Basic, Accurate, Advanced
                AddToleranceToTracking = false, // Allows turret to track to the edge of the AimingTolerance cone instead of dead centre.
                Ui = new UiDef
                {
                    RateOfFire = true, // Enables terminal slider for changing rate of fire.
                    RateOfFireMin = 0.5f, // Sets the minimum limit for the rate of fire slider, default is 0.  Range is 0-1f.
                    EnableOverload = false, // Enables terminal option to turn on Overload; this allows energy weapons to double damage per shot, at the cost of quadrupled power draw and heat gain, and 2% self damage on overheat.
                },
                Ai = new AiDef
                {
                    TrackTargets = true, // Whether this weapon tracks its own targets, or (for multiweapons) relies on the weapon with PrimaryTracking enabled for target designation. Turrets Need this set to True.
                    TurretAttached = true, // Whether this weapon is a turret and should have the UI and API options for such. Turrets Need this set to True.
                    TurretController = true, // Whether this weapon can physically control the turret's movement. Turrets Need this set to True.
                    PrimaryTracking = true, // For multiweapons: whether this weapon should designate targets for other weapons on the platform without their own tracking.
                    SuppressFire = true, // If enabled, weapon can only be fired manually.
                    OverrideLeads = false, // Disable target leading on fixed weapons, or allow it for turrets.
                    DefaultLeadGroup = 0, // Default LeadGroup setting, range 0-5, 0 is disables lead group.  Only useful for fixed weapons or weapons set to OverrideLeads.
                },
                HardWare = new HardwareDef
                {
                    RotateRate = 0.1f, // Max traversal speed of azimuth subpart in radians per tick (0.1 is approximately 360 degrees per second).
                    ElevateRate = 0.1f, // Max traversal speed of elevation subpart in radians per tick.
                    MinAzimuth = -180,
                    MaxAzimuth = 180,
                    MinElevation = -40,
                    MaxElevation = 40,
                    HomeAzimuth = 0, // Default resting rotation angle
                    HomeElevation = 0, // Default resting elevation
                    InventorySize = 1f, // Inventory capacity in kL.
                    IdlePower = 0.25f, // Constant base power draw in MW.
                },
                Other = new OtherDef
                {
                    ConstructPartCap = 1, // Maximum number of blocks with this weapon on a grid; 0 = unlimited.
                    Debug = true, // Force enables debug mode.
                },
                Loading = new LoadingDef
                {
                    RateOfFire = 60, // Set this to 3600 for beam weapons. This is how fast your Gun fires.
                    BarrelsPerShot = 1, // How many muzzles will fire a projectile per fire event.
                    SkipBarrels = 0, // Number of muzzles to skip after each fire event.
                    ReloadTime = 1, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    MagsToLoad = 1, // Number of physical magazines to consume on reload.
                    DelayUntilFire = 0, // How long the weapon waits before shooting after being told to fire. Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    HeatPerShot = 1, // Heat generated per shot.
                    MaxHeat = 70000, // Max heat before weapon enters cooldown (70% of max heat).
                    Cooldown = .95f, // Percentage of max heat to be under to start firing again after overheat; accepts 0 - 0.95
                    HeatSinkRate = 9000000, // Amount of heat lost per second.
                    DegradeRof = false, // Progressively lower rate of fire when over 80% heat threshold (80% of max heat).
                    ShotsInBurst = 0, // Use this if you don't want the weapon to fire an entire physical magazine in one go. Should not be more than your magazine capacity.
                    DelayAfterBurst = 0, // How long to spend "reloading" after each burst. Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    StayCharged = true, // Will start recharging whenever power cap is not full.
                    MaxActiveProjectiles = 0, // Maximum number of drones in flight (only works for drone launchers)
                },
                Audio = new HardPointAudioDef
                {
                    PreFiringSound = "", // Audio for warmup effect.
                    FiringSound = "WepShipGatlingShot", // Audio for firing.
                    FiringSoundPerShot = true, // Whether to replay the sound for each shot, or just loop over the entire track while firing.
                    ReloadSound = "", // Sound SubtypeID, for when your Weapon is in a reloading state
                    NoAmmoSound = "",
                    HardPointRotationSound = "", // Audio played when turret is moving.
                    FireSoundEndDelay = 120, // How long the firing audio should keep playing after firing stops. Measured in game ticks(6 = 100ms, 60 = 1 seconds, etc..).
                    FireSoundNoBurst = true, // Don't stop firing sound from looping when delaying after burst.
                },
                Graphics = new HardPointParticleDef
                {
                    Effect1 = new ParticleDef
                    {
                        Name = "Muzzle_Flash_Large", // SubtypeId of muzzle particle effect.
                        Color = Color(red: 0, green: 0, blue: 0, alpha: 1), // Deprecated, set color in particle sbc.
                        Offset = Vector(x: 0, y: 0, z: 0), // Offsets the effect from the muzzle empty.
                        DisableCameraCulling = false, // If not true will not cull when not in view of camera, be careful with this and only use if you know you need it
                        Extras = new ParticleOptionDef
                        {
                            Loop = false, // Set this to the same as in the particle sbc!
                            Restart = false, // Whether to end a looping effect instantly when firing stops.
                            MaxDistance = 800,
                            MaxDuration = 0,
                            Scale = 1f, // Scale of effect.
                        },
                    },
                },
            },
            Ammos = new[] {
                GFA_Ammo_XWing_LaserCannon,
            },
        };
        WeaponDefinition GFA_Weapon_XWing_LaserCannon_TopRight => new WeaponDefinition
        {
            Assignments = new ModelAssignmentsDef
            {
                MountPoints = new[] {
                    new MountPointDef {
                        SubtypeId = "GFA_SG_XWing_FireControl",
                        MuzzlePartId = "topright_el",
                        AzimuthPartId = "sfoil_laser_topright",
                        ElevationPartId = "topright_el",
                        DurabilityMod = 0.25f,
                    },
                    
                 },
                Muzzles = new[] {
                    "muzzle_barrel_001",
                },
                Scope = "muzzle_barrel_001",
            },
            Targeting = new TargetingDef
            {
                Threats = new[] {
                    Grids, // Types of threat to engage: Grids, Projectiles, Characters, Meteors, Neutrals, ScanRoid, ScanPlanet, ScanFriendlyCharacter, ScanFriendlyGrid, ScanEnemyCharacter, ScanEnemyGrid, ScanNeutralCharacter, ScanNeutralGrid, ScanUnOwnedGrid, ScanOwnersGrid
                },
                SubSystems = new[] {
                    Thrust, Utility, Offense, Power, Production, Any, // Subsystem targeting priority: Offense, Utility, Power, Production, Thrust, Jumping, Steering, Any
                },
            },
            HardPoint = new HardPointDef
            {
                PartName = "KX9 Laser Cannon",
                DeviateShotAngle = 0.2f, // Projectile inaccuracy in degrees.
                AimingTolerance = 1f, // How many degrees off target a turret can fire at. 0 - 180 firing angle.
                AimLeadingPrediction = Accurate, // Level of turret aim prediction; Off, Basic, Accurate, Advanced
                AddToleranceToTracking = false, // Allows turret to track to the edge of the AimingTolerance cone instead of dead centre.
                Ui = new UiDef
                {
                    RateOfFire = true, // Enables terminal slider for changing rate of fire.
                    RateOfFireMin = 0.5f, // Sets the minimum limit for the rate of fire slider, default is 0.  Range is 0-1f.
                    EnableOverload = false, // Enables terminal option to turn on Overload; this allows energy weapons to double damage per shot, at the cost of quadrupled power draw and heat gain, and 2% self damage on overheat.
                },
                Ai = new AiDef
                {
                    TrackTargets = true, // Whether this weapon tracks its own targets, or (for multiweapons) relies on the weapon with PrimaryTracking enabled for target designation. Turrets Need this set to True.
                    TurretAttached = true, // Whether this weapon is a turret and should have the UI and API options for such. Turrets Need this set to True.
                    TurretController = true, // Whether this weapon can physically control the turret's movement. Turrets Need this set to True.
                    PrimaryTracking = false, // For multiweapons: whether this weapon should designate targets for other weapons on the platform without their own tracking.
                    SuppressFire = true, // If enabled, weapon can only be fired manually.
                    OverrideLeads = false, // Disable target leading on fixed weapons, or allow it for turrets.
                    DefaultLeadGroup = 0, // Default LeadGroup setting, range 0-5, 0 is disables lead group.  Only useful for fixed weapons or weapons set to OverrideLeads.
                },
                HardWare = new HardwareDef
                {
                    RotateRate = 0.1f, // Max traversal speed of azimuth subpart in radians per tick (0.1 is approximately 360 degrees per second).
                    ElevateRate = 0.1f, // Max traversal speed of elevation subpart in radians per tick.
                    MinAzimuth = -180,
                    MaxAzimuth = 180,
                    MinElevation = -40,
                    MaxElevation = 40,
                    HomeAzimuth = 0, // Default resting rotation angle
                    HomeElevation = 0, // Default resting elevation
                    InventorySize = 1f, // Inventory capacity in kL.
                    IdlePower = 0.25f, // Constant base power draw in MW.
                },
                Other = new OtherDef
                {
                    ConstructPartCap = 1, // Maximum number of blocks with this weapon on a grid; 0 = unlimited.
                    Debug = true, // Force enables debug mode.
                },
                Loading = new LoadingDef
                {
                    RateOfFire = 60, // Set this to 3600 for beam weapons. This is how fast your Gun fires.
                    BarrelsPerShot = 1, // How many muzzles will fire a projectile per fire event.
                    SkipBarrels = 0, // Number of muzzles to skip after each fire event.
                    ReloadTime = 1, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    MagsToLoad = 1, // Number of physical magazines to consume on reload.
                    DelayUntilFire = 0, // How long the weapon waits before shooting after being told to fire. Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    HeatPerShot = 1, // Heat generated per shot.
                    MaxHeat = 70000, // Max heat before weapon enters cooldown (70% of max heat).
                    Cooldown = .95f, // Percentage of max heat to be under to start firing again after overheat; accepts 0 - 0.95
                    HeatSinkRate = 9000000, // Amount of heat lost per second.
                    DegradeRof = false, // Progressively lower rate of fire when over 80% heat threshold (80% of max heat).
                    ShotsInBurst = 0, // Use this if you don't want the weapon to fire an entire physical magazine in one go. Should not be more than your magazine capacity.
                    DelayAfterBurst = 0, // How long to spend "reloading" after each burst. Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    StayCharged = true, // Will start recharging whenever power cap is not full.
                    MaxActiveProjectiles = 0, // Maximum number of drones in flight (only works for drone launchers)
                },
                Audio = new HardPointAudioDef
                {
                    PreFiringSound = "", // Audio for warmup effect.
                    FiringSound = "WepShipGatlingShot", // Audio for firing.
                    FiringSoundPerShot = true, // Whether to replay the sound for each shot, or just loop over the entire track while firing.
                    ReloadSound = "", // Sound SubtypeID, for when your Weapon is in a reloading state
                    NoAmmoSound = "",
                    HardPointRotationSound = "", // Audio played when turret is moving.
                    FireSoundEndDelay = 120, // How long the firing audio should keep playing after firing stops. Measured in game ticks(6 = 100ms, 60 = 1 seconds, etc..).
                    FireSoundNoBurst = true, // Don't stop firing sound from looping when delaying after burst.
                },
                Graphics = new HardPointParticleDef
                {
                    Effect1 = new ParticleDef
                    {
                        Name = "Muzzle_Flash_Large", // SubtypeId of muzzle particle effect.
                        Color = Color(red: 0, green: 0, blue: 0, alpha: 1), // Deprecated, set color in particle sbc.
                        Offset = Vector(x: 0, y: 0, z: 0), // Offsets the effect from the muzzle empty.
                        DisableCameraCulling = false, // If not true will not cull when not in view of camera, be careful with this and only use if you know you need it
                        Extras = new ParticleOptionDef
                        {
                            Loop = false, // Set this to the same as in the particle sbc!
                            Restart = false, // Whether to end a looping effect instantly when firing stops.
                            MaxDistance = 800,
                            MaxDuration = 0,
                            Scale = 1f, // Scale of effect.
                        },
                    },
                },
            },
            Ammos = new[] {
                GFA_Ammo_XWing_LaserCannon,
            },
        };
        WeaponDefinition GFA_Weapon_XWing_LaserCannon_TopLeft => new WeaponDefinition
        {
            Assignments = new ModelAssignmentsDef
            {
                MountPoints = new[] {
                    new MountPointDef {
                        SubtypeId = "GFA_SG_XWing_FireControl",
                        MuzzlePartId = "topleft_el",
                        AzimuthPartId = "sfoil_laser_topleft",
                        ElevationPartId = "topleft_el",
                        DurabilityMod = 0.25f,
                    },
                    
                 },
                Muzzles = new[] {
                    "muzzle_barrel_002",
                },
                Scope = "muzzle_barrel_002",
            },
            Targeting = new TargetingDef
            {
                Threats = new[] {
                    Grids, // Types of threat to engage: Grids, Projectiles, Characters, Meteors, Neutrals, ScanRoid, ScanPlanet, ScanFriendlyCharacter, ScanFriendlyGrid, ScanEnemyCharacter, ScanEnemyGrid, ScanNeutralCharacter, ScanNeutralGrid, ScanUnOwnedGrid, ScanOwnersGrid
                },
                SubSystems = new[] {
                    Thrust, Utility, Offense, Power, Production, Any, // Subsystem targeting priority: Offense, Utility, Power, Production, Thrust, Jumping, Steering, Any
                },
            },
            HardPoint = new HardPointDef
            {
                PartName = "KX9 Laser Cannon",
                DeviateShotAngle = 0.2f, // Projectile inaccuracy in degrees.
                AimingTolerance = 1f, // How many degrees off target a turret can fire at. 0 - 180 firing angle.
                AimLeadingPrediction = Accurate, // Level of turret aim prediction; Off, Basic, Accurate, Advanced
                AddToleranceToTracking = false, // Allows turret to track to the edge of the AimingTolerance cone instead of dead centre.
                Ui = new UiDef
                {
                    RateOfFire = true, // Enables terminal slider for changing rate of fire.
                    RateOfFireMin = 0.5f, // Sets the minimum limit for the rate of fire slider, default is 0.  Range is 0-1f.
                    EnableOverload = false, // Enables terminal option to turn on Overload; this allows energy weapons to double damage per shot, at the cost of quadrupled power draw and heat gain, and 2% self damage on overheat.
                },
                Ai = new AiDef
                {
                    TrackTargets = true, // Whether this weapon tracks its own targets, or (for multiweapons) relies on the weapon with PrimaryTracking enabled for target designation. Turrets Need this set to True.
                    TurretAttached = true, // Whether this weapon is a turret and should have the UI and API options for such. Turrets Need this set to True.
                    TurretController = true, // Whether this weapon can physically control the turret's movement. Turrets Need this set to True.
                    PrimaryTracking = false, // For multiweapons: whether this weapon should designate targets for other weapons on the platform without their own tracking.
                    SuppressFire = true, // If enabled, weapon can only be fired manually.
                    OverrideLeads = false, // Disable target leading on fixed weapons, or allow it for turrets.
                    DefaultLeadGroup = 0, // Default LeadGroup setting, range 0-5, 0 is disables lead group.  Only useful for fixed weapons or weapons set to OverrideLeads.
                },
                HardWare = new HardwareDef
                {
                    RotateRate = 0.1f, // Max traversal speed of azimuth subpart in radians per tick (0.1 is approximately 360 degrees per second).
                    ElevateRate = 0.1f, // Max traversal speed of elevation subpart in radians per tick.
                    MinAzimuth = -180,
                    MaxAzimuth = 180,
                    MinElevation = -40,
                    MaxElevation = 40,
                    HomeAzimuth = 0, // Default resting rotation angle
                    HomeElevation = 0, // Default resting elevation
                    InventorySize = 1f, // Inventory capacity in kL.
                    IdlePower = 0.25f, // Constant base power draw in MW.
                },
                Other = new OtherDef
                {
                    ConstructPartCap = 1, // Maximum number of blocks with this weapon on a grid; 0 = unlimited.
                    Debug = true, // Force enables debug mode.
                },
                Loading = new LoadingDef
                {
                    RateOfFire = 60, // Set this to 3600 for beam weapons. This is how fast your Gun fires.
                    BarrelsPerShot = 1, // How many muzzles will fire a projectile per fire event.
                    SkipBarrels = 0, // Number of muzzles to skip after each fire event.
                    ReloadTime = 1, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    MagsToLoad = 1, // Number of physical magazines to consume on reload.
                    DelayUntilFire = 0, // How long the weapon waits before shooting after being told to fire. Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    HeatPerShot = 1, // Heat generated per shot.
                    MaxHeat = 70000, // Max heat before weapon enters cooldown (70% of max heat).
                    Cooldown = .95f, // Percentage of max heat to be under to start firing again after overheat; accepts 0 - 0.95
                    HeatSinkRate = 9000000, // Amount of heat lost per second.
                    DegradeRof = false, // Progressively lower rate of fire when over 80% heat threshold (80% of max heat).
                    ShotsInBurst = 0, // Use this if you don't want the weapon to fire an entire physical magazine in one go. Should not be more than your magazine capacity.
                    DelayAfterBurst = 0, // How long to spend "reloading" after each burst. Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    StayCharged = true, // Will start recharging whenever power cap is not full.
                    MaxActiveProjectiles = 0, // Maximum number of drones in flight (only works for drone launchers)
                },
                Audio = new HardPointAudioDef
                {
                    PreFiringSound = "", // Audio for warmup effect.
                    FiringSound = "WepShipGatlingShot", // Audio for firing.
                    FiringSoundPerShot = true, // Whether to replay the sound for each shot, or just loop over the entire track while firing.
                    ReloadSound = "", // Sound SubtypeID, for when your Weapon is in a reloading state
                    NoAmmoSound = "",
                    HardPointRotationSound = "", // Audio played when turret is moving.
                    FireSoundEndDelay = 120, // How long the firing audio should keep playing after firing stops. Measured in game ticks(6 = 100ms, 60 = 1 seconds, etc..).
                    FireSoundNoBurst = true, // Don't stop firing sound from looping when delaying after burst.
                },
                Graphics = new HardPointParticleDef
                {
                    Effect1 = new ParticleDef
                    {
                        Name = "Muzzle_Flash_Large", // SubtypeId of muzzle particle effect.
                        Color = Color(red: 0, green: 0, blue: 0, alpha: 1), // Deprecated, set color in particle sbc.
                        Offset = Vector(x: 0, y: 0, z: 0), // Offsets the effect from the muzzle empty.
                        DisableCameraCulling = false, // If not true will not cull when not in view of camera, be careful with this and only use if you know you need it
                        Extras = new ParticleOptionDef
                        {
                            Loop = false, // Set this to the same as in the particle sbc!
                            Restart = false, // Whether to end a looping effect instantly when firing stops.
                            MaxDistance = 800,
                            MaxDuration = 0,
                            Scale = 1f, // Scale of effect.
                        },
                    },
                },
            },
            Ammos = new[] {
                GFA_Ammo_XWing_LaserCannon,
            },
        };
        // Don't edit below this line.
    }
}
