@BlockID "GFA_LG_REB_Field13"
@Version 2
@Author enenra

using Field as Subpart("field")
using Light1 as Light("light_1", 15.0, false, 1.3, 5.0)
using Light2 as Light("light_2", 15.0, false, 1.3, 5.0)
using Light3 as Light("light_3", 15.0, false, 1.3, 5.0)
using Emitter as Emitter("light_1")
using Emissive as Emissive("EmissiveAtlas")
using EmissiveSecondary as Emissive("Emissive0")

action Block() {
	Working() {
		Field.setvisible(true)

		Light1.setcolor(41, 112, 181)
		Light1.lighton()
		Light2.setcolor(41, 112, 181)
		Light2.lighton()
		Light3.setcolor(41, 112, 181)
		Light3.lighton()

		Emissive.setcolor(61, 132, 201, 10.0, false)
		EmissiveSecondary.setcolor(255, 255, 255, 1.0, false)

		Emitter.playSound("BlockProjectorOn")
	}

	NotWorking() {
		Field.setvisible(false)

		Light1.lightoff()
		Light2.lightoff()
		Light3.lightoff()

		Emissive.setcolor(0, 0, 0, 0.0, false)
		EmissiveSecondary.setcolor(0, 0, 0, 0.0, false)

		Emitter.playSound("BlockProjectorOff")
	}
}

action Door() {
	Open() {
		Field.setvisible(false)

		Light1.lightoff()
		Light2.lightoff()
		Light3.lightoff()

		Emissive.setcolor(0, 0, 0, 0.0, false)
		EmissiveSecondary.setcolor(0, 0, 0, 0.0, false)

		Emitter.playSound("BlockProjectorOff")
	}
	Close() {
		Field.setvisible(true)

		Light1.lighton()
		Light2.lighton()
		Light3.lighton()

		Emissive.setcolor(61, 132, 201, 10.0, false)
		EmissiveSecondary.setcolor(255, 255, 255, 1.0, false)

		Emitter.playSound("BlockProjectorOn")
	}
}