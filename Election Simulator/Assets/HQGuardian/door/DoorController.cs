using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {
	public GameObject visitorPrefab;
	public GameObject mainController;
	public GameObject exit;
	public GameObject table;
	bool opened = false;
	private Transform doorTransform;
	private System.Random rand = new System.Random();
	int framesUntilSpawn = 60;
	int framesUntilTry = 60;
	int framesNotEmpty = 0;
	Vector2 mainAxis;
	Vector2 secondaryAxis;
	public bool horizontal;
	public bool right;
	int blockedFor = 0;
	Vector3 initialCenter;

	private Queue<Visitor> visitorsQueue = new Queue<Visitor>();
	private Dictionary<Visitor, GameObject> visitorObjects = new Dictionary<Visitor, GameObject>();

	// Use this for initialization
	void Start () {
		doorTransform = GetComponent<Transform> ();
		initialCenter = doorTransform.position;
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
		framesNotEmpty++;
		if (framesUntilTry < 0 && visitorsQueue.Count > 0 && framesNotEmpty > 180) {
			RevealPassed ();
			framesNotEmpty = 0;
			if (visitorsQueue.Count > 0 && opened) {
				visitorObjects [visitorsQueue.Peek ()].GetComponent<Transform> ().position -= new Vector3 (mainAxis.x, mainAxis.y, 0); 
			}
			framesUntilTry = 100;
		}

		DrawQueue ();
	}

	void RevealPassed() {
		Visitor first = visitorsQueue.Dequeue ();
		GameObject extractedObject = visitorObjects [first];
		MainController.VisitorAction act = opened ? MainController.VisitorAction.PASS : MainController.VisitorAction.BLOCK;
		mainController.GetComponent<MainController> ().triggerAction (first.Reveal(), act);
		if (!opened)
			Destroy (extractedObject);
		else if (first.Reveal () == MainController.VisitorType.VOLUNTEER) {
			extractedObject.GetComponent<VisitorController> ().JoinNavalny (table.GetComponent<Rigidbody2D> ().position);
		} else {
			Destroy (extractedObject);
		}
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
			if (order == 0 && opened) {
				visitorObjects [vis].GetComponent<Transform> ().position = initialCenter - Vector2to3 (mainAxis) * 0.2f;
			} else {
				Vector2 offset = mainAxis * 0.7f - secondaryAxis * order * 0.5f;
				visitorObjects [vis].GetComponent<Transform> ().position = initialCenter + Vector2to3 (offset);
			}
			order++;
		}
	}

	Vector3 Vector2to3 (Vector2 vec) {
		return new Vector3 (
			vec.x,
			vec.y,
			0
		);
	}
}
