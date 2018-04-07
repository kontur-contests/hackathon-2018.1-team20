using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {

	bool opened = false;
	private Transform doorTransform;
	private System.Random rand = new System.Random();
	int framesUntilSpawn = 60;
	int framesUntilTry = 60;

	private Queue<Visitor> visitorsQueue = new Queue<Visitor>();

	// Use this for initialization
	void Start () {
		doorTransform = GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
		framesUntilSpawn--;
		if (framesUntilSpawn < 0) {
			visitorsQueue.Enqueue (new Visitor ());
			framesUntilSpawn = rand.Next (60, 300);
		}

		framesUntilTry--;
		if (framesUntilTry < 0 && visitorsQueue.Count > 0) {
			int receivedPoints = visitorsQueue.Dequeue ().Reveal();
			if (opened) {
				Debug.Log (receivedPoints);
			} else {
				if (receivedPoints > 0)
					Debug.Log ("Volunteer lost");
				else
					Debug.Log ("Invasion prevented");
			}
			framesUntilTry = rand.Next (60, 180);
		}

	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			if (opened) {
				doorTransform.Rotate (new Vector3 (0, 0, 90));
			} else {
				doorTransform.Rotate (new Vector3 (0, 0, -90));
			}
			opened = !opened;
		}
	}
}
