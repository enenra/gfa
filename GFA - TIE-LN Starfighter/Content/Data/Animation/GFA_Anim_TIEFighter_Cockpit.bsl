@BlockID "GFA_SG_TIEFighter_Cockpit"
@Version 2
@Author enenra

using Light as Light("Cockpit", 1.5, false)

action Block() {
	Working() {
		Light.setcolor(85, 10, 7)
		Light.lighton()
	}
	NotWorking() {
		Light.lightoff()
	}
}