@BlockID "GFA_SG_XWing_AstroBay"
@Version 2
@Author enenra


using Head as Subpart("head")
using Emitter as Emitter("head")


var isMoving = false
var isTalking = true


# Functions
func moveHead() {
	API.log("Move Loop happens")
	API.log(isMoving)
	if (isMoving == true) {
		var rotateAmount = Math.randomrange(-90.0, 90.0)
		Head.rotate([0, 1, 0], rotateAmount, Math.abs(rotateAmount), InOutQuart)
	}
}

func playAmbient() {
	API.log("Ambient Loop happens")
	API.log(isTalking)
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
		Head.reset()
	}
	Built() {
		API.log("<<< Built triggered")
		if (isTalking == true) {
			playArrive()
		}
		if (isMoving == true) {
			API.startloop(moveHead(), 1, 100, 60)
		}
	}
	Working() {
		API.log("<<< Working triggered")
		isTalking = true
		API.startloop(playAmbient(), 1, 100, 60)
	}
	NotWorking() {
		API.log("<<< NotWorking triggered")
		isTalking = false
	}
}

action Distance(15.0) {
    Arrive() {
		API.log("<<< Arrive triggered")
		isMoving = true
		API.startloop(moveHead(), 1, 100, 60)

		if (isTalking == true) {
			playArrive()
		}
		API.startloop(playAmbient(), 10, 100, 60)
    }
    Leave() {
		API.log("<<< Leave triggered")
		API.log("<<< reset:")
		API.stoploop(moveHead())
		Head.reset()

		API.log("<<< reset:")
		API.stoploop(playAmbient())
    }
}