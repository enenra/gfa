@BlockID "GFA_SG_XWing_AstroBay"
@Version 2
@Author enenra

using Head as Subpart("head")
using Emitter as Emitter("head")
using Emissive as Emissive("Emissive") parent Head

var isMoving = false
var isTalking = true
var moveDelay = 600
var talkDelay = 3600

func moveHead() {
	if (block.IsFunctional() == true && isMoving == true) {
		var rotateAmount = Math.RandomRange(-180.0, 180.0)
		Head.rotate([0, 1, 0], rotateAmount, Math.abs(rotateAmount), InOutQuart)
	}
}

func playAmbient() {
	if (block.IsFunctional() == true && isTalking == true) {
		Emitter.playSound("_GFA_Astromech_Ambient")
	}
}

action Block() {
	Create() {
		API.StartLoop("moveHead", moveDelay, -1)
		API.StartLoop("playAmbient", talkDelay, -1, 0)
		Emissive.SetColor(20, 0, 255, 10.0, false)
	}

	Working() {
		isTalking = true
		Emissive.SetColor(20, 0, 255, 10.0, false)
	}

	NotWorking() {
		Emissive.SetColor(255, 0, 20, 10.0, false)
	}
}

action Distance(15.0) {
    Arrive() {
		isMoving = true

		if (block.IsFunctional() == true)  {
			isTalking = true
			Emitter.playSound("_GFA_Astromech_Arrive")
		}
		
    }
    Leave() {
		isTalking = false
		isMoving = false

		API.StopDelays()
		Head.MoveToOrigin(60, InOutQuart)
    }
}