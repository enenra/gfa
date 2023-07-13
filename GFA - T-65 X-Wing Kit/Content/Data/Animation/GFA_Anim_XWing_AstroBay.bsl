@BlockID "GFA_SG_XWing_AstroBay"
@Version 2
@Author enenra


using Head as Subpart("head")
using Emitter as Emitter("head")
using Emissive as Emissive("Emissive") parent Head


var isMoving = false
var isTalking = true
var moveDelay = 300
var talkDelay = 600
var moveLoopActive = false
var talkLoopActive = false


# Functions
func moveHead() {
	if (isMoving == true) {
		var rotateAmount = Math.RandomRange(-180.0, 180.0)
		Head.rotate([0, 1, 0], rotateAmount, Math.abs(rotateAmount), InOutQuart)
	}
}

func playAmbient() {
	if (isTalking == true) {
		Emitter.playSound("_GFA_Astromech_Ambient")
	}
}

func playArrive() {
	if (isTalking == true) {
		Emitter.playSound("_GFA_Astromech_Arrive")
	}
}


# Actions
action Block() {
	Create() {
		Head.Reset()
	}
	Built() {
		if (isTalking == true) {
			playArrive()
		}
		if (isMoving == true) {
			if (moveLoopActive == false) {
				API.StartLoop("moveHead", moveDelay, -1, 0)
			}
		}
	}
	Working() {
		isTalking = true
		if (talkLoopActive == false) {
			API.StartLoop("playAmbient", talkDelay, -1, 0)
		}
		Emissive.SetColor(20, 0, 255, 1.0, false)
	}
	NotWorking() {
		isTalking = false
		Emissive.SetColor(255, 0, 20, 1.0, false)
	}
}

action Distance(15.0) {
    Arrive() {
		isMoving = true
		if (moveLoopActive == false) {
			API.StartLoop("moveHead", moveDelay, -1, 0)
		}

		if (isTalking == true) {
			playArrive()
			if (talkLoopActive == false) {
				API.StartLoop("playAmbient", talkDelay, -1, talkDelay)
			}
		}
		
    }
    Leave() {
		API.StopLoop("moveHead")
		Head.MoveToOrigin(60, InOutQuart)

		API.StopLoop("playAmbient")
    }
}