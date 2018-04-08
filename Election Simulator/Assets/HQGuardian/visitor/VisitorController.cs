using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisitorController : MonoBehaviour {

	Vector2 currentSpeed;
	private static int maxSpeed = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Rigidbody2D> ().velocity = currentSpeed;
	}

	public void JoinNavalny (Vector2 target) {
		Debug.Log ("join navalny");
		currentSpeed = GetSpeedVector (target);
	}

	Vector2 GetSpeedVector (Vector2 target) {
		Vector2 dist = (target - GetComponent<Rigidbody2D>().position);
		dist.Normalize ();
		return new Vector2(
			dist.x * maxSpeed,
			dist.y * maxSpeed
		);
	}	
}
