using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {
	public GameObject visitorPrefab;
	public GameObject mainController;
	bool opened = false;
	private Transform doorTransform;
	private System.Random rand = new System.Random();
	int framesUntilSpawn = 60;
	int framesUntilTry = 60;

	private Queue<Visitor> visitorsQueue = new Queue<Visitor>();
	private Dictionary<Visitor, GameObject> visitorObjects = new Dictionary<Visitor, GameObject>();

	// Use this for initialization
	void Start () {
		doorTransform = GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
		framesUntilSpawn--;
		if (framesUntilSpawn < 0) {
			Visitor newVisitor = new Visitor ();
			GameObject newVisitorObject = Instantiate (visitorPrefab);
			newVisitorObject.GetComponent<Animator> ().SetBool ("volunteer", newVisitor.Reveal () == MainController.VisitorType.VOLUNTEER);
			visitorsQueue.Enqueue (newVisitor);
			visitorObjects.Add (newVisitor, newVisitorObject);
			framesUntilSpawn = rand.Next (30, 180);
		}

		framesUntilTry--;
		if (framesUntilTry < 0 && visitorsQueue.Count > 0) {
			Visitor extracted = visitorsQueue.Dequeue ();
			Destroy (visitorObjects [extracted]);
			MainController.VisitorAction act = opened ? MainController.VisitorAction.PASS : MainController.VisitorAction.PASS;
			mainController.GetComponent<MainController> ().triggerAction (extracted.Reveal(), act);
			framesUntilTry = 300;
		}

		DrawQueue ();
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

	void DrawQueue() {
		int order = 0;
		foreach (Visitor vis in visitorsQueue) {
			Vector3 visPosition = new Vector3 (
				doorTransform.position.x - order * 0.5f,
				doorTransform.position.y - order * 0.5f,
				-3
			);
			visitorObjects[vis].GetComponent<Transform> ().position = visPosition;
			order++;
		}
	}
}
