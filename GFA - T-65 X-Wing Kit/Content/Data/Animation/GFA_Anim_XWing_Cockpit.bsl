@BlockID "GFA_SG_XWing_Cockpit"
@Version 2
@Author enenra


using Canopy as Subpart("canopy")
using Emitter as Emitter("canopy")


var isSeated = false
var isOpen = false


# Functions
func checkState() {
    isSeated = Block.IsOccupied()
	if (isSeated == true) {
		Canopy.reset()
	}
	else {
		openCanopy()
	}
}

func openCanopy() {
    if (isOpen == false) {
        Canopy.rotate([1, 0, 0], -70.0, 60, InOutCubic)
		Emitter.playSound("_GFA_XWing_Cockpit_Open")
        isOpen = true
    }
}

func closeCanopy() {
    if (isOpen == true) {
        Canopy.rotate([1, 0, 0], 70.0, 60, InOutCubic)
		Emitter.playSound("_GFA_XWing_Cockpit_Close")
        isOpen = false
    }
}


# Actions
action Block() {
	Create() {
		checkState()
	}
	Built() {
		isOpen = false
		checkState()
	}
	Working() {
		checkState()
	}
	NotWorking() {
		checkState()
	}
}

action Cockpit() {
	Enter() {
		if (isOpen == true) {
			closeCanopy()
		}
	}
	Exit() {
		if (isOpen == false) {
			openCanopy()
		}
	}
}

action Distance(7.5) {
    Arrive() {
		if (isOpen == false) {
			openCanopy()
		}
    }
    Leave() {
		if (isOpen == true) {
			closeCanopy()
		}
    }
}