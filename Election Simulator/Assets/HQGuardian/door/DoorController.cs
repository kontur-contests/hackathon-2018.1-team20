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
	Vector2 mainAxis;
	Vector2 secondaryAxis;
	public bool horizontal;
	public bool right;
	int blockedFor = 0;

	private Queue<Visitor> visitorsQueue = new Queue<Visitor>();
	private Dictionary<Visitor, GameObject> visitorObjects = new Dictionary<Visitor, GameObject>();

	// Use this for initialization
	void Start () {
		doorTransform = GetComponent<Transform> ();
		if (horizontal) {
			if (right) {
				mainAxis = new Vector2 (0, -1);
				secondaryAxis = new Vector2 (-1, 0);
			} else {
				mainAxis = new Vector2 (0, 1);
				secondaryAxis = new Vector2 (-1, 0);
			}
		} else {
			if (right) {
				mainAxis = new Vector2 (1, 0);
				secondaryAxis = new Vector2 (0, -1);
			} else {
				mainAxis = new Vector2 (-1, 0);
				secondaryAxis = new Vector2 (0, -1);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		blockedFor--;
		framesUntilSpawn--;
		if (framesUntilSpawn < 0) {
			Visitor newVisitor = new Visitor ();
			GameObject newVisitorObject = Instantiate (visitorPrefab);
			newVisitorObject.GetComponent<Animator> ().SetBool ("volunteer", newVisitor.Reveal () == MainController.VisitorType.VOLUNTEER);
			visitorsQueue.Enqueue (newVisitor);
			visitorObjects.Add (newVisitor, newVisitorObject);
			framesUntilSpawn = rand.Next (60, 180);
		}

		framesUntilTry--;
		if (framesUntilTry < 0 && visitorsQueue.Count > 0) {
			Visitor extracted = visitorsQueue.Dequeue ();
			GameObject extractedObject = visitorObjects [extracted];
//			Rigidbody2D extractedRigid = extractedObject.GetComponent<Rigidbody2D> ();
//			extractedRigid.AddForce (new Vector2 (0,-0.5f));
//			extractedRigid.
			Destroy (extractedObject);
			if (visitorsQueue.Count > 0 && opened) {
				visitorObjects [visitorsQueue.Peek ()].GetComponent<Transform> ().position -= new Vector3 (mainAxis.x, mainAxis.y, 0); 
			}
			MainController.VisitorAction act = opened ? MainController.VisitorAction.PASS : MainController.VisitorAction.PASS;
			mainController.GetComponent<MainController> ().triggerAction (extracted.Reveal(), act);
			framesUntilTry = 240;
		}

		DrawQueue ();
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (blockedFor < 0 && other.gameObject.tag == "Player") {
			Vector3 doorCenter = doorTransform.position;
			// Magic numbers
			Vector2 offset2D = mainAxis * 0.6f + secondaryAxis * 0.7f;
			Vector3 offset = new Vector3(offset2D.x, offset2D.y, 0);

			if (opened) {
				doorTransform.RotateAround (doorCenter, Vector3.back, 90);
				doorTransform.position = doorCenter - offset;
			} else {
				doorTransform.RotateAround (doorCenter, Vector3.forward, 90);
				doorTransform.position = doorCenter + offset;
			}
			opened = !opened;
			blockedFor = 60;
		}
	}

	void DrawQueue() {
		int order = 0;
		foreach (Visitor vis in visitorsQueue) {
			Vector2 offset = mainAxis * 0.7f - secondaryAxis * order * 0.5f;
			Vector3 newPosition = new Vector3 (
				doorTransform.position.x + offset.x,
				doorTransform.position.y + offset.y,
				-5
			);
			visitorObjects[vis].GetComponent<Transform> ().position = newPosition;
			order++;
		}
	}
}
