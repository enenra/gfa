using System.Collections.Generic;
using static Scripts.Structure.WeaponDefinition;
using static Scripts.Structure.WeaponDefinition.AnimationDef;
using static Scripts.Structure.WeaponDefinition.AnimationDef.PartAnimationSetDef.EventTriggers;
using static Scripts.Structure.WeaponDefinition.AnimationDef.RelMove.MoveType;
using static Scripts.Structure.WeaponDefinition.AnimationDef.RelMove;
namespace Scripts
{ // Don't edit above this line
    partial class Parts
    {
        private AnimationDef GFA_Animation_H9DualTurbolaser => new AnimationDef
        {
            AnimationSets = new[]
            {
                new PartAnimationSetDef()
                {
                    SubpartId = Names("recoil_1"),
                    BarrelId = "muzzle_barrel_1", //only used for firing, use "Any" for all muzzles
                    StartupFireDelay = 0,
                    AnimationDelays = Delays(FiringDelay: 0, ReloadingDelay: 0, OverheatedDelay: 0, TrackingDelay: 0, LockedDelay: 0, OnDelay: 0, OffDelay: 0, BurstReloadDelay: 0, OutOfAmmoDelay: 0, PreFireDelay: 0, StopFiringDelay: 0, StopTrackingDelay:0, InitDelay:0),//Delay before animation starts
                    Reverse = Events(),
                    Loop = Events(),
                    EventMoveSets = new Dictionary<PartAnimationSetDef.EventTriggers, RelMove[]>
                    {
                        // Reloading, Firing, Tracking, Overheated, TurnOn, TurnOff, BurstReload, NoMagsToLoad, PreFire, EmptyOnGameLoad, StopFiring, StopTracking, LockDelay, Init
                        [Firing] =
                            new[]
                            {
                                new RelMove
                                {
                                    CenterEmpty = "",
                                    TicksToMove = 4, //number of ticks to complete motion, 60 = 1 second
                                    MovementType = ExpoDecay, //Linear,ExpoDecay,ExpoGrowth,Delay,Show, //instant or fade Hide, //instant or fade
                                    LinearPoints = new[]
                                    {
                                        Transformation(0, 0, 0.85), //linear movement
                                    },
                                    Rotation = Transformation(0, 0, 0), //degrees
                                    RotAroundCenter = Transformation(0, 0, 0), //degrees
                                },
                                new RelMove
                                {
                                    CenterEmpty = "",
                                    TicksToMove = 2, //number of ticks to complete motion, 60 = 1 second
                                    MovementType = Delay,
                                    LinearPoints = new XYZ[0],
                                    Rotation = Transformation(0, 0, 0), //degrees
                                    RotAroundCenter = Transformation(0, 0, 0), //degrees
                                },
                                new RelMove
                                {
                                    CenterEmpty = "",
                                    TicksToMove = 14, //number of ticks to complete motion, 60 = 1 second
                                    MovementType = Linear, //Linear,ExpoDecay,ExpoGrowth,Delay,Show, //instant or fade Hide, //instant or fade
                                    LinearPoints = new[]
                                    {
                                        Transformation(0, 0, -0.85), //linear movement
                                    },
                                    Rotation = Transformation(0, 0, 0), //degrees
                                    RotAroundCenter = Transformation(0, 0, 0), //degrees
                                },
                            },

                    }
                },
                new PartAnimationSetDef()
                {
                    SubpartId = Names("recoil_2"),
                    BarrelId = "muzzle_barrel_2", //only used for firing, use "Any" for all muzzles
                    StartupFireDelay = 0,
                    AnimationDelays = Delays(FiringDelay: 0, ReloadingDelay: 0, OverheatedDelay: 0, TrackingDelay: 0, LockedDelay: 0, OnDelay: 0, OffDelay: 0, BurstReloadDelay: 0, OutOfAmmoDelay: 0, PreFireDelay: 0, StopFiringDelay: 0, StopTrackingDelay:0, InitDelay:0),//Delay before animation starts
                    Reverse = Events(),
                    Loop = Events(),
                    EventMoveSets = new Dictionary<PartAnimationSetDef.EventTriggers, RelMove[]>
                    {
                        // Reloading, Firing, Tracking, Overheated, TurnOn, TurnOff, BurstReload, NoMagsToLoad, PreFire, EmptyOnGameLoad, StopFiring, StopTracking, LockDelay, Init
                        [Firing] =
                            new[]
                            {
                                new RelMove
                                {
                                    CenterEmpty = "",
                                    TicksToMove = 4, //number of ticks to complete motion, 60 = 1 second
                                    MovementType = ExpoDecay, //Linear,ExpoDecay,ExpoGrowth,Delay,Show, //instant or fade Hide, //instant or fade
                                    LinearPoints = new[]
                                    {
                                        Transformation(0, 0, 0.85), //linear movement
                                    },
                                    Rotation = Transformation(0, 0, 0), //degrees
                                    RotAroundCenter = Transformation(0, 0, 0), //degrees
                                },
                                new RelMove
                                {
                                    CenterEmpty = "",
                                    TicksToMove = 2, //number of ticks to complete motion, 60 = 1 second
                                    MovementType = Delay,
                                    LinearPoints = new XYZ[0],
                                    Rotation = Transformation(0, 0, 0), //degrees
                                    RotAroundCenter = Transformation(0, 0, 0), //degrees
                                },
                                new RelMove
                                {
                                    CenterEmpty = "",
                                    TicksToMove = 14, //number of ticks to complete motion, 60 = 1 second
                                    MovementType = Linear, //Linear,ExpoDecay,ExpoGrowth,Delay,Show, //instant or fade Hide, //instant or fade
                                    LinearPoints = new[]
                                    {
                                        Transformation(0, 0, -0.85), //linear movement
                                    },
                                    Rotation = Transformation(0, 0, 0), //degrees
                                    RotAroundCenter = Transformation(0, 0, 0), //degrees
                                },
                            },

                    }
                },
            }
        };
    }
}