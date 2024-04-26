@BlockID "GFA_SG_TIEFighter_Hatch"
@Version 2
@Author enenra

using Hatch as Button("hatch", "detector_conveyor_1")
using Emitter as Emitter("hatch")

var isOpen = false
var duration = 70

func reset() {
	Hatch.Reset()
}

func openHatch() {
    if (isOpen == false && API.iscontrollingentity() == false) {
		reset()
		Hatch.rotate([1, 0, 0], -90.0, duration, InOutCubic)
		
		Emitter.playSound("_GFA_TIEFighter_Hatch_Open")
        isOpen = true
	}
}

func closeHatch() {
    if (isOpen == true) {
		Hatch.rotate([1, 0, 0], 90.0, duration, InOutCubic)
		
		Emitter.playSound("_GFA_TIEFighter_Hatch_Close")
        isOpen = false
		API.DelayFunction("reset", duration)
	}
}

action Button(Hatch) {
	Hovering(hovering) {
		if (block.IsWorking() == true) {
			if (hovering == true) {
				openHatch()
			} else {
				closeHatch()
			}
		}
	}
}