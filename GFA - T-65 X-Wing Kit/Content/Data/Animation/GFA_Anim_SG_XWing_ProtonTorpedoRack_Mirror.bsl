@BlockID "GFA_SG_XWing_ProtonTorpedoRack_Mirror"
@Version 2
@Author enenra

using Top as Button("top", "detector_inventory_1")
using Torp1 as Subpart("torp_1")
using Torp2 as Subpart("torp_2")
using Torp3 as Subpart("torp_3")

func openRack() {
	Top.reset()
	Top.rotate([0, 0, 1], 70.0, 70, InOutCubic)
}

func closeRack() {
	Top.reset()
	Top.rotate([0, 0, 1], 70.0, 0, Linear)
	Top.rotate([0, 0, 1], -70.0, 70, InOutCubic)
}

func showTorps() {
	Torp1.setvisible(true)
	Torp2.setvisible(true)
	Torp3.setvisible(true)
}

func hideTorps() {
	Torp1.setvisible(false)
	Torp2.setvisible(false)
	Torp3.setvisible(false)
}

action Button(Top) {
	Hovering(hovering) {
		if (hovering == true) {
			openRack()
		} else {
			closeRack()
		}
	}
}

action Inventory() {
	Changed(changed) {
		if (changed == 0) {
			hideTorps()
		} else {
			showTorps()
		}
	}
}