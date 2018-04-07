using System;

class Visitor {

	bool ally;

	public Visitor() {
		Random rand = new Random ();
		ally = rand.Next (0, 100) > 30;
	}

	public int Reveal() {
		if (ally)
			return 1;
		else
			return -2;
	}
}