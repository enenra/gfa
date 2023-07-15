@BlockID "GFA_SG_XWing_Cockpit"
@Version 2
@Author enenra

using Canopy as Subpart("canopy")
using Emitter as Emitter("canopy")

func openCanopy() {
	if (block.IsFunctional() == true) {
		Canopy.reset()
	}
	Canopy.rotate([1, 0, 0], -70.0, 70, InOutCubic)
	Emitter.playSound("_GFA_XWing_Cockpit_Open")
}

func closeCanopy() {
	if (block.IsFunctional() == true) {
		Canopy.reset().rotate([1, 0, 0], -70.0, 0, Linear).rotate([1, 0, 0], 70.0, 70, InOutCubic)
		Emitter.playSound("_GFA_XWing_Cockpit_Close")
	}
}

action block() {
	create() {
		Canopy.setResetPos()
	}
}

action Cockpit() {
	Enter() {
		closeCanopy()
	}
	Exit() {
		openCanopy()
	}
}

action Distance(7.5) {
    Arrive() {
		if (block.isOccupied() == false) {
			openCanopy()
		}
    }
    Leave() {
		if (block.isOccupied() == false) {
			closeCanopy()
		}
    }
}