using System;

class Visitor {

	MainController.VisitorType type;

	public Visitor() {
		Random rand = new Random ();
		if (rand.Next (0, 100) > 30) {
			type = MainController.VisitorType.VOLUNTEER;
		} else {
			type = MainController.VisitorType.ENEMY;
		}
	}

	public MainController.VisitorType Reveal() {
		return type;
	}
}