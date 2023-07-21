@BlockID "GFA_SG_XWing_FrontGear"
@Version 2
@Author enenra

using DoorLeft as Subpart("gear_door_left")
using DoorRight as Subpart("gear_door_right")
using PistonInner as Subpart("gear_piston_inner")
using PistonOuter as Subpart("gear_piston_outer") parent PistonInner
using Foot as Subpart("gear_foot") parent PistonOuter
using SupportPistonOuter as Subpart("gear_sup_piston_outer") parent PistonInner
using SupportPistonInner as Subpart("gear_sup_piston_inner") parent SupportPistonOuter
using SupportPistonRail as Subpart("gear_sup_piston_rail") parent SupportPistonInner
using Emitter as Emitter("gear_piston_inner")

var isOpen = false

func reset() {
	DoorLeft.Reset()
	DoorRight.Reset()
	PistonInner.Reset()
	PistonOuter.Reset()
	Foot.Reset()
	SupportPistonOuter.Reset()
	SupportPistonInner.Reset()
	SupportPistonRail.Reset()
}

func unfold() {
    if (isOpen == false) {
		# Step 1
        DoorLeft.Rotate([0,-0.1644278718,0.9863891093], 95.0, 40, Linear)
        DoorRight.Rotate([0,-0.1644278718,0.9863891093], -95.0, 40, Linear)

		# Step 2
		var startDelay = 40
        PistonInner.Delay(startDelay).Rotate([1, 0, 0], 85.0, 60, Linear)
        PistonOuter.Delay(startDelay).Translate([0, 0, -0.625], 60, Linear)
        Foot.Delay(startDelay).Rotate([1, 0, 0], 85.0, 60, Linear)

        SupportPistonOuter.Delay(startDelay).Rotate([1, 0, 0], 55.0, 20, Linear)
        SupportPistonInner.Delay(startDelay).Translate([0, 0, -0.375], 60, Linear)
        SupportPistonRail.Delay(startDelay).Rotate([1, 0, 0], -27.5, 20, Linear)

		# Step 3
		startDelay = startDelay + 20
        SupportPistonOuter.Delay(startDelay).Rotate([1, 0, 0], 42.5, 20, Linear)
        SupportPistonRail.Delay(startDelay).Rotate([1, 0, 0], -17.5, 40, Linear)

		# Step 4
		startDelay = startDelay + 20
        SupportPistonOuter.Delay(startDelay).Rotate([1, 0, 0], 32.5, 20, Linear)

		Emitter.PlaySound("_GFA_XWing_CargoScoop")
        isOpen = true
    }
}

func fold() {
    if (isOpen == true) {

		# Step 1
        PistonInner.Rotate([1, 0, 0], -85.0, 60, Linear)
        PistonOuter.Translate([0, 0, 0.625], 60, Linear)
        Foot.Rotate([1, 0, 0], -85.0, 60, Linear)

        SupportPistonOuter.Rotate([1, 0, 0], -32.5, 20, Linear)
        SupportPistonInner.Translate([0, 0, 0.375], 60, Linear)

		# Step 2
		var startDelay = 20
        SupportPistonOuter.Delay(startDelay).Rotate([1, 0, 0], -42.5, 20, Linear)
        SupportPistonRail.Delay(startDelay).Rotate([1, 0, 0], 17.5, 40, Linear)

		# Step 3
		startDelay = startDelay + 20
        SupportPistonOuter.Delay(startDelay).Rotate([1, 0, 0], -55.0, 20, Linear)
        SupportPistonRail.Delay(startDelay).Rotate([1, 0, 0], 27.5, 20, Linear)

		# Step 4
		startDelay = startDelay + 20
		DoorLeft.Delay(startDelay).Rotate([0,-0.1644278718,0.9863891093], -95.0, 40, Linear)
        DoorRight.Delay(startDelay).Rotate([0,-0.1644278718,0.9863891093], 95.0, 40, Linear)

		Emitter.PlaySound("_GFA_XWing_CargoScoop")
        isOpen = false
		API.DelayFunction("reset", 100)
    }
}

action Block() {
	Working() {
		unfold()
	}
	NotWorking() {
		fold()
	}
}