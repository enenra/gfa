@BlockID "GFA_SG_XWing_RearGear"
@Version 2
@Author enenra

using DoorUp as Subpart("gear_door_upper")
using DoorDown as Subpart("gear_door_lower")
using PistonUpperBase as Subpart("gear_piston_upper")
using PistonUpperHead as Subpart("gear_piston_upper_head") parent PistonUpperBase
using PistonLowerBase as Subpart("gear_piston_lower_base") parent PistonUpperHead
using PistonLowerHead as Subpart("gear_piston_lower_head") parent PistonLowerBase
using Foot as Subpart("gear_foot_rear") parent PistonLowerHead
using Emitter as Emitter("gear_piston_upper")

var isOpen = false

func reset() {
	DoorUp.Reset()
	DoorDown.Reset()
	PistonUpperBase.Reset()
	PistonUpperHead.Reset()
	PistonLowerBase.Reset()
	PistonLowerHead.Reset()
	Foot.Reset()
}

func unfold() {
    if (isOpen == false) {
		# Step 1
		DoorUp.Rotate([0,0,1], 110.0, 30, Linear)
		DoorDown.Rotate([0,0,1], -130.0, 30, Linear)

		# Step 2
		var startDelay = 30
        PistonUpperBase.Delay(startDelay).Rotate([0, 0, 1], -40.0, 20, Linear)
        PistonUpperHead.Delay(startDelay).Translate([-0.385, 0, 0], 60, Linear)
        PistonLowerBase.Delay(startDelay).Rotate([0, 0, 1], -140.0, 40, Linear)

		# Step 3
		startDelay = startDelay + 20
        Foot.Delay(startDelay).Rotate([0, 0, 1], 110.0, 40, Linear)

		# Step 4
		startDelay = startDelay + 10
        PistonLowerHead.Delay(startDelay).Translate([0.285, 0, 0], 30, Linear)

		Emitter.PlaySound("_GFA_XWing_CargoScoop")
        isOpen = true
    }
}

func fold() {
    if (isOpen == true) {

		# Step 1
        PistonLowerHead.Translate([-0.285, 0, 0], 30, Linear)

		# Step 2
		var startDelay = 10
        Foot.Delay(startDelay).Rotate([0, 0, 1], -110.0, 40, Linear)

		# Step 3
		startDelay = startDelay + 20
        PistonUpperBase.Delay(startDelay).Rotate([0, 0, 1], 40.0, 40, Linear)
        PistonUpperHead.Delay(startDelay).Translate([0.385, 0, 0], 20, Linear)
        PistonLowerBase.Delay(startDelay).Rotate([0, 0, 1], 140.0, 40, Linear)

		# Step 4
		startDelay = startDelay + 30
		DoorUp.Delay(startDelay).Rotate([0,0,1], -110.0, 30, Linear)
		DoorDown.Delay(startDelay).Rotate([0,0,1], 130.0, 30, Linear)

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