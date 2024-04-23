@BlockID "GFA_SG_TIEFighter_Cockpit"
@Version 2
@Author enenra

using Light as Light("Cockpit", 0.0)

action Block() {
	Working() {
		Light.setcolor(65, 8, 5)
		Light.lighton()
	}
	NotWorking() {
		Light.lightoff()
	}
}