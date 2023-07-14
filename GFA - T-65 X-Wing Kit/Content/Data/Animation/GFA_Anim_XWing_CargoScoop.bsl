@BlockID "GFA_SG_XWing_CargoScoop"
@Version 2
@Author enenra

using Scoop as Button("cargoscoop", "detector_conveyor_1")
using PistonInner01 as Subpart("piston_inner_001") parent Scoop
using PistonInner02 as Subpart("piston_inner_002") parent Scoop
using PistonOuter01 as Subpart("piston_outer") parent PistonInner01
using PistonOuter02 as Subpart("piston_outer") parent PistonInner02
using Emitter as Emitter("cargoscoop")

var isOpen = false
var duration = 100

# Functions
func reset() {
	Scoop.Reset()
	PistonInner01.Reset()
	PistonInner02.Reset()
	PistonOuter01.Reset()
	PistonOuter02.Reset()
}

func openScoop() {
    if (isOpen == false) {
        Scoop.Rotate([1, 0, 0], 45.0, duration, InQuad)
        PistonInner01.Rotate([1, 0, 0], -1.0, duration, InQuad)
        PistonInner02.Rotate([1, 0, 0], -1.0, duration, InQuad)
        PistonOuter01.Translate([0, -0.23, 0], duration, InQuad)
        PistonOuter02.Translate([0, -0.23, 0], duration, InQuad)

		Emitter.PlaySound("_GFA_XWing_CargoScoop")
        isOpen = true
    }
}

func closeScoop() {
    if (isOpen == true) {
        Scoop.Rotate([1, 0, 0], -45.0, duration, InQuad)
        PistonInner01.Rotate([1, 0, 0], 1.0, duration, InQuad)
        PistonInner02.Rotate([1, 0, 0], 1.0, duration, InQuad)
        PistonOuter01.Translate([0, 0.23, 0], duration, InQuad)
        PistonOuter02.Translate([0, 0.23, 0], duration, InQuad)

		Emitter.PlaySound("_GFA_XWing_CargoScoop")
        isOpen = false
		API.DelayFunction("reset", duration)
    }
}

# Actions
action Block() {
	Working() {
		openScoop()
	}
	NotWorking() {
		closeScoop()
	}
}

action Button(Scoop) {
	Hovering(hovering) {
		if (block.IsWorking() == false) {
			if (hovering == true) {
				openScoop()
			} else {
				closeScoop()
			}
		}
	}
}