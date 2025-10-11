@BlockID "GFA_LG_IMP_FieldSmall"
@Version 2
@Author enenra

using Field as Subpart("field")
using Light as Light("light", 15.0, false, 1.3, 2.0)
using Emitter as Emitter("light")
using Emissive as Emissive("EmissiveAtlas")
using EmissiveSecondary as Emissive("Emissive0")

action Block() {
	Working() {
		Field.setvisible(true)

		Light.setcolor(41, 112, 181)
		Light.lighton()

		Emissive.setcolor(61, 132, 201, 10.0, false)
		EmissiveSecondary.setcolor(255, 255, 255, 1.0, false)

		Emitter.playSound("BlockProjectorOn")
	}

	NotWorking() {
		Field.setvisible(false)

		Light.lightoff()

		Emissive.setcolor(0, 0, 0, 0.0, false)
		EmissiveSecondary.setcolor(0, 0, 0, 0.0, false)

		Emitter.playSound("BlockProjectorOff")
	}
}

action Door() {
	Open() {
		Field.setvisible(false)
		Light.lightoff()
		Emissive.setcolor(0, 0, 0, 0.0, false)
		EmissiveSecondary.setcolor(0, 0, 0, 0.0, false)
		Emitter.playSound("BlockProjectorOff")
	}
	Close() {
		Field.setvisible(true)
		Light.lighton()
		Emissive.setcolor(61, 132, 201, 10.0, false)
		EmissiveSecondary.setcolor(255, 255, 255, 1.0, false)
		Emitter.playSound("BlockProjectorOn")
	}
}