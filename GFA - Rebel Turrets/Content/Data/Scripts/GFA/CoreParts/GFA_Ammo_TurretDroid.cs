using static Scripts.Structure.WeaponDefinition;
using static Scripts.Structure.WeaponDefinition.AmmoDef;
using static Scripts.Structure.WeaponDefinition.AmmoDef.EjectionDef;
using static Scripts.Structure.WeaponDefinition.AmmoDef.EjectionDef.SpawnType;
using static Scripts.Structure.WeaponDefinition.AmmoDef.ShapeDef.Shapes;
using static Scripts.Structure.WeaponDefinition.AmmoDef.DamageScaleDef.CustomScalesDef.SkipMode;
using static Scripts.Structure.WeaponDefinition.AmmoDef.GraphicDef;
using static Scripts.Structure.WeaponDefinition.AmmoDef.FragmentDef;
using static Scripts.Structure.WeaponDefinition.AmmoDef.PatternDef.PatternModes;
using static Scripts.Structure.WeaponDefinition.AmmoDef.FragmentDef.TimedSpawnDef.PointTypes;
using static Scripts.Structure.WeaponDefinition.AmmoDef.TrajectoryDef;
using static Scripts.Structure.WeaponDefinition.AmmoDef.TrajectoryDef.ApproachDef.Conditions;
using static Scripts.Structure.WeaponDefinition.AmmoDef.TrajectoryDef.ApproachDef.UpRelativeTo;
using static Scripts.Structure.WeaponDefinition.AmmoDef.TrajectoryDef.ApproachDef.FwdRelativeTo;
using static Scripts.Structure.WeaponDefinition.AmmoDef.TrajectoryDef.ApproachDef.ReInitCondition;
using static Scripts.Structure.WeaponDefinition.AmmoDef.TrajectoryDef.ApproachDef.RelativeTo;
using static Scripts.Structure.WeaponDefinition.AmmoDef.TrajectoryDef.ApproachDef.ConditionOperators;
using static Scripts.Structure.WeaponDefinition.AmmoDef.TrajectoryDef.ApproachDef.StageEvents;
using static Scripts.Structure.WeaponDefinition.AmmoDef.TrajectoryDef.ApproachDef;
using static Scripts.Structure.WeaponDefinition.AmmoDef.TrajectoryDef.GuidanceType;
using static Scripts.Structure.WeaponDefinition.AmmoDef.DamageScaleDef;
using static Scripts.Structure.WeaponDefinition.AmmoDef.DamageScaleDef.ShieldDef.ShieldType;
using static Scripts.Structure.WeaponDefinition.AmmoDef.DamageScaleDef.DeformDef.DeformTypes;
using static Scripts.Structure.WeaponDefinition.AmmoDef.AreaOfDamageDef;
using static Scripts.Structure.WeaponDefinition.AmmoDef.AreaOfDamageDef.Falloff;
using static Scripts.Structure.WeaponDefinition.AmmoDef.AreaOfDamageDef.AoeShape;
using static Scripts.Structure.WeaponDefinition.AmmoDef.EwarDef;
using static Scripts.Structure.WeaponDefinition.AmmoDef.EwarDef.EwarMode;
using static Scripts.Structure.WeaponDefinition.AmmoDef.EwarDef.EwarType;
using static Scripts.Structure.WeaponDefinition.AmmoDef.EwarDef.PushPullDef.Force;
using static Scripts.Structure.WeaponDefinition.AmmoDef.GraphicDef.LineDef;
using static Scripts.Structure.WeaponDefinition.AmmoDef.GraphicDef.LineDef.FactionColor;
using static Scripts.Structure.WeaponDefinition.AmmoDef.GraphicDef.LineDef.TracerBaseDef;
using static Scripts.Structure.WeaponDefinition.AmmoDef.GraphicDef.LineDef.Texture;
using static Scripts.Structure.WeaponDefinition.AmmoDef.GraphicDef.DecalDef;
using static Scripts.Structure.WeaponDefinition.AmmoDef.DamageScaleDef.DamageTypes.Damage;

namespace Scripts
{
    partial class Parts
    {
        private AmmoDef GFA_Ammo_TurretDroid_Red => new AmmoDef
        {
            AmmoMagazine = "GFA_AmmoMag_Sidearm_Red", // SubtypeId of physical ammo magazine. Use "Energy" for weapons without physical ammo.
            AmmoRound = "Turret Droid Bolt Red", // Name of ammo in terminal, should be different for each ammo type used by the same weapon. Is used by Shrapnel.
            TerminalName = "Red Blaster Cartridge", // Optional terminal name for this ammo type, used when picking ammo/cycling consumables.  Safe to have duplicates across different ammo defs.
            HybridRound = true, // Use both a physical ammo magazine and energy per shot.
            EnergyCost = 0.1f, // Scaler for energy per shot (EnergyCost * BaseDamage * (RateOfFire / 3600) * BarrelsPerShot * TrajectilesPerBarrel). Uses EffectStrength instead of BaseDamage if EWAR.
            BaseDamage = 50f, // Direct damage; one steel plate is worth 100.
            Mass = 0.1f, // In kilograms; how much force the impact will apply to the target.
            Health = 0, // How much damage the projectile can take from other projectiles (base of 1 per hit) before dying; 0 disables this and makes the projectile untargetable.
            EnergyMagazineSize = 1000, // For energy weapons, how many shots to fire before reloading.
            HeatModifier = 4f, // Allows this ammo to modify the amount of heat the weapon produces per shot.
            BackKickForce = 1f, // Recoil. This is applied to the Parent Grid.
            NoGridOrArmorScaling = true, // If you enable this you can remove the damagescale section entirely.
            HardPointUsable = true, // Whether this is a primary ammo type fired directly by the turret. Set to false if this is a shrapnel ammoType and you don't want the turret to be able to select it directly.
            Shape = new ShapeDef // Defines the collision shape of the projectile, defaults to LineShape and uses the visual Line Length if set to 0.
            {
                Shape = LineShape, // LineShape or SphereShape. Do not use SphereShape for fast moving projectiles if you care about precision.
                Diameter = 2, // Diameter is minimum length of LineShape or minimum diameter of SphereShape.
            },
            DamageScales = new DamageScaleDef
            {
                MaxIntegrity = 0f, // Blocks with integrity higher than this value will be immune to damage from this projectile; 0 = disabled.
                HealthHitModifier = 0.5, // How much Health to subtract from another projectile on hit; defaults to 1 if zero or less.
                // For the following modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01f = 1% damage, 2 = 200% damage.
                FallOff = new FallOffDef
                {
                    Distance = 500f, // Distance at which damage begins falling off.
                    MinMultipler = 0.01f, // Value from 0.0001f to 1f where 0.1f would be a min damage of 10% of base damage.
                },
                Shields = new ShieldDef
                {
                    Modifier = 20f, // Multiplier for damage against shields.
                    Type = Default, // Damage vs healing against shields; Default, Heal
                },
                DamageType = new DamageTypes // Damage type of each element of the projectile's damage; Kinetic, Energy
                {
                    Base = Energy, // Base Damage uses this
                    AreaEffect = Energy,
                    Detonation = Energy,
                    Shield = Energy, // Damage against shields is currently all of one type per projectile. Shield Bypass Weapons, always Deal Energy regardless of this line
                },
                Deform = new DeformDef
                {
                    DeformType = HitBlock,
                    DeformDelay = 30,
                },
            },
            Trajectory = new TrajectoryDef
            {
                Guidance = None, // None, Remote, TravelTo, Smart, DetectTravelTo, DetectSmart, DetectFixed
                MaxLifeTime = 900, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..). time begins at 0 and time must EXCEED this value to trigger "time > maxValue". Please have a value for this, It stops Bad things.
                DesiredSpeed = 400, // voxel phasing if you go above 5100
                MaxTrajectory = 600f, // Max Distance the projectile or beam can Travel.
                TotalAcceleration = 1234.5, // 0 means no limit, something to do due with a thing called delta and something called v.
            },
            AmmoGraphics = new GraphicDef
            {
                ModelName = "",
                VisualProbability = 1f,
                ShieldHitDraw = false,
                Decals = new DecalDef
                {
                    MaxAge = 3600,
                    Map = new[]
                    {
                        new TextureMapDef
                        {
                            HitMaterial = "Metal",
                            DecalMaterial = "GunBullet",
                        },
                        new TextureMapDef
                        {
                            HitMaterial = "Glass",
                            DecalMaterial = "GunBullet",
                        },
                    },
                },
                Particles = new AmmoParticleDef
                {
                    Hit = new ParticleDef
                    {
                        Name = "GFA_Particle_LaserCannon_Impact",
                        ApplyToShield = false,
                        Offset = Vector(x: 0, y: 0, z: 0),
                        DisableCameraCulling = false, // If not true will not cull when not in view of camera, be careful with this and only use if you know you need it
                        Extras = new ParticleOptionDef
                        {
                            Scale = 0.15f,
                            HitPlayChance = 1f,
                        },
                    },
                    ShieldHit = new ParticleDef
                    {
                        Name = "GFA_Particle_Shield_Impact",
                        DisableCameraCulling = false, // If not true will not cull when not in view of camera, be careful with this and only use if you know you need it
                        Extras = new ParticleOptionDef
                        {
                            Scale = 5,
                            HitPlayChance = 1f, // 0-1% chance the particle is shown
                        },
                    },
                },
                Lines = new LineDef
                {
                    ColorVariance = Random(start: 0.75f, end: 2f),
                    WidthVariance = Random(start: 0f, end: 0f),
                    DropParentVelocity = false,

                    Tracer = new TracerBaseDef
                    {
                        Enable = true,
                        Length = 3f,
                        Width = 0.05f,
                        Color = Color(red: 13, green: 3, blue: 2, alpha: 1),
                        FactionColor = DontUse,
                        VisualFadeStart = 0,
                        VisualFadeEnd = 0,
                        AlwaysDraw = false,
                        Textures = new[] {
                            "GFA_TurretDroid_Bullet",
                        },
                    },
                },
            },
            AmmoAudio = new AmmoAudioDef
            {
                TravelSound = "_GFA_LaserCannon_PassBy",
                HitSound = "_GFA_LaserCannon_Impact",
                ShotSound = "",
                ShieldHitSound = "_GFA_Shield_Impact",
                PlayerHitSound = "_GFA_LaserCannon_Impact",
                VoxelHitSound = "_GFA_LaserCannon_Impact",
                FloatingHitSound = "_GFA_LaserCannon_Impact",
                HitPlayChance = 0.5f,
                HitPlayShield = true,
            },
        };
        private AmmoDef GFA_Ammo_TurretDroid_Green => new AmmoDef
        {
            AmmoMagazine = GFA_Ammo_TurretDroid_Red.AmmoMagazine.Replace("Red", "Green"),
            AmmoRound = GFA_Ammo_TurretDroid_Red.AmmoRound.Replace("Red", "Green"),
            TerminalName = GFA_Ammo_TurretDroid_Red.TerminalName.Replace("Red", "Green"),
            HybridRound = GFA_Ammo_TurretDroid_Red.HybridRound,
            EnergyCost = GFA_Ammo_TurretDroid_Red.EnergyCost,
            BaseDamage = GFA_Ammo_TurretDroid_Red.BaseDamage,
            Mass = GFA_Ammo_TurretDroid_Red.Mass,
            Health = GFA_Ammo_TurretDroid_Red.Health,
            EnergyMagazineSize = GFA_Ammo_TurretDroid_Red.EnergyMagazineSize,
            HeatModifier = GFA_Ammo_TurretDroid_Red.HeatModifier,
            BackKickForce = GFA_Ammo_TurretDroid_Red.BackKickForce,
            NoGridOrArmorScaling = GFA_Ammo_TurretDroid_Red.NoGridOrArmorScaling,
            HardPointUsable = GFA_Ammo_TurretDroid_Red.HardPointUsable,
            Shape = new ShapeDef
            {
                Shape = GFA_Ammo_TurretDroid_Red.Shape.Shape,
                Diameter = GFA_Ammo_TurretDroid_Red.Shape.Diameter,
            },
            DamageScales = GFA_Ammo_TurretDroid_Red.DamageScales,
            AreaOfDamage = new AreaOfDamageDef
            {
                EndOfLife = new EndOfLifeDef
                {
                    Enable = GFA_Ammo_TurretDroid_Red.AreaOfDamage.EndOfLife.Enable,
                    Radius = GFA_Ammo_TurretDroid_Red.AreaOfDamage.EndOfLife.Radius,
                    Damage = GFA_Ammo_TurretDroid_Red.AreaOfDamage.EndOfLife.Damage,
                    Depth = GFA_Ammo_TurretDroid_Red.AreaOfDamage.EndOfLife.Depth,
                    MaxAbsorb = GFA_Ammo_TurretDroid_Red.AreaOfDamage.EndOfLife.MaxAbsorb,
                    Falloff = GFA_Ammo_TurretDroid_Red.AreaOfDamage.EndOfLife.Falloff,
                    ParticleScale = GFA_Ammo_TurretDroid_Red.AreaOfDamage.EndOfLife.ParticleScale,
                    CustomParticle = GFA_Ammo_TurretDroid_Red.AreaOfDamage.EndOfLife.CustomParticle,
                    CustomSound = GFA_Ammo_TurretDroid_Red.AreaOfDamage.EndOfLife.CustomSound,
                    NoVisuals = GFA_Ammo_TurretDroid_Red.AreaOfDamage.EndOfLife.NoVisuals,
                    NoSound = GFA_Ammo_TurretDroid_Red.AreaOfDamage.EndOfLife.NoSound,
                    Shape = GFA_Ammo_TurretDroid_Red.AreaOfDamage.EndOfLife.Shape,
                },
            },
            Trajectory = new TrajectoryDef
            {
                Guidance = GFA_Ammo_TurretDroid_Red.Trajectory.Guidance,
                MaxLifeTime = GFA_Ammo_TurretDroid_Red.Trajectory.MaxLifeTime,
                DesiredSpeed = GFA_Ammo_TurretDroid_Red.Trajectory.DesiredSpeed,
                MaxTrajectory = GFA_Ammo_TurretDroid_Red.Trajectory.MaxTrajectory,
                TotalAcceleration = GFA_Ammo_TurretDroid_Red.Trajectory.TotalAcceleration,
            },
            AmmoGraphics = new GraphicDef
            {
                ModelName = "",
                VisualProbability = GFA_Ammo_TurretDroid_Red.AmmoGraphics.VisualProbability,
                ShieldHitDraw = GFA_Ammo_TurretDroid_Red.AmmoGraphics.ShieldHitDraw,
                Decals = GFA_Ammo_TurretDroid_Red.AmmoGraphics.Decals,
                Particles = new AmmoParticleDef
                {
                    Hit = GFA_Ammo_TurretDroid_Red.AmmoGraphics.Particles.Hit,
                    ShieldHit = GFA_Ammo_TurretDroid_Red.AmmoGraphics.Particles.ShieldHit,
                    WeaponEffect1Override = new ParticleDef
                    {
                        Name = "GFA_Particle_LaserCannon_Muzzle_Green",
                        Offset = Vector(x: 0, y: 0, z: 0),
                        DisableCameraCulling = false,
                        Extras = new ParticleOptionDef
                        {
                            Loop = true,
                            Restart = true,
                            MaxDistance = 800,
                            MaxDuration = 0.1f,
                            Scale = 0.25f,
                        },
                    },
                },
                Lines = new LineDef
                {
                    ColorVariance = GFA_Ammo_TurretDroid_Red.AmmoGraphics.Lines.ColorVariance,
                    WidthVariance = GFA_Ammo_TurretDroid_Red.AmmoGraphics.Lines.WidthVariance,
                    DropParentVelocity = GFA_Ammo_TurretDroid_Red.AmmoGraphics.Lines.DropParentVelocity,

                    Tracer = new TracerBaseDef
                    {
                        Enable = GFA_Ammo_TurretDroid_Red.AmmoGraphics.Lines.Tracer.Enable,
                        Length = GFA_Ammo_TurretDroid_Red.AmmoGraphics.Lines.Tracer.Length,
                        Width = GFA_Ammo_TurretDroid_Red.AmmoGraphics.Lines.Tracer.Width,
                        Color = Color(red: 3, green: 13, blue: 2, alpha: 1),
                        FactionColor = GFA_Ammo_TurretDroid_Red.AmmoGraphics.Lines.Tracer.FactionColor,
                        VisualFadeStart = GFA_Ammo_TurretDroid_Red.AmmoGraphics.Lines.Tracer.VisualFadeStart,
                        VisualFadeEnd = GFA_Ammo_TurretDroid_Red.AmmoGraphics.Lines.Tracer.VisualFadeEnd,
                        AlwaysDraw = GFA_Ammo_TurretDroid_Red.AmmoGraphics.Lines.Tracer.AlwaysDraw,
                        Textures = GFA_Ammo_TurretDroid_Red.AmmoGraphics.Lines.Tracer.Textures,
                    },
                },
            },
            AmmoAudio = GFA_Ammo_TurretDroid_Red.AmmoAudio,
        };
    }
}
