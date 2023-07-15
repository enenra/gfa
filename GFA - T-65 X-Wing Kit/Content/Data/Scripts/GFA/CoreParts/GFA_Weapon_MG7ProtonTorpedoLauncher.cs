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
                AimingTolerance = 2f,
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
                    FiringSound = "_GFA_XWing_ProtonTorpedo_Shot",
                    FiringSoundPerShot = true,
                },
                Graphics = new HardPointParticleDef
                {
                    Effect1 = new ParticleDef
                    {
                        Name = "Muzzle_Flash_LargeCalibre",
                        Extras = new ParticleOptionDef
                        {
                            Scale = 1f,
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
