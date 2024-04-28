@BlockID "GFA_LG_Holo_Rebels_5x5"
@Version 2
@Author enenra

using Holo as Subpart("holo")
using Light as Light("subpart_holo", 5.0, false, 1.3, 5.0)
using Emissive as Emissive("EmissiveAtlas")

var loopDelay = 1000

func lightFlicker(length) {
	Light.lightoff().delay(length / 10).lighton().delay(length / 10).lightoff().delay(length / 10).lighton().delay(length / 10).lightoff().delay(length / 10).lighton().delay(length / 10).lightoff().delay(length / 10).lighton().delay(length / 10).lightoff().delay(length / 10).lighton().delay(length / 10)
}

func flicker() {
	var flickerLength = Math.RandomRange(30.0, 40.0)
	var amount = Math.RandomRange(1.0, 10.0)

	lightFlicker(flickerLength)
	Holo.vibrate(1.0, flickerLength).delay(flickerLength).resetpos()
}

func rotateHolo() {
	if (block.isfunctional() == true) {
		Holo.rotate([0, 1, 0], 180.0, loopDelay, Linear)
	}
}

action Block() {
	Working() {
		Holo.setresetpos()

		Holo.setvisible(true)
		API.startloop("rotateHolo", loopDelay, -1)

		Light.setcolor(41, 112, 181)
		Light.lighton()
		Emissive.setcolor(41, 112, 181, 1000.0, false)
	}

	NotWorking() {
		Holo.setvisible(false)
		API.stoploop("rotateHolo")

		Light.lightoff()
		Emissive.setcolor(255, 255, 255, 0.0, false)
	}
}