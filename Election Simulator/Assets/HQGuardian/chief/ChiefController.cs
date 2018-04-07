using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChiefController : MonoBehaviour {

	public static float maxSpeed = 10;
	public static double distanceThreshold = 0.5;
	private Rigidbody2D chiefRigid;
	public GameObject targetMark;

	private Queue<Vector2> nextTargets = new Queue<Vector2>();

	public Vector2 currentTarget;
	public Vector2 currentSpeed;

	// Use this for initialization
	void Start () {
		chiefRigid = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonUp (0)) {
			nextTargets.Enqueue (Vector3to2 (Camera.main.ScreenPointToRay (Input.mousePosition).origin));
		} else if (TargetReached ()) {
			NextTarget ();
		}
			
		chiefRigid.velocity = currentSpeed;
	}

	bool TargetReached() {
		if (currentTarget == default(Vector2))
			return true;
		return Vector2.Distance (chiefRigid.position, currentTarget) < distanceThreshold;
	}

	public void SkipTarget() {
		Debug.Log ("Skip target");
		currentTarget = default(Vector2);
		currentSpeed = default(Vector2);
		currentSpeed = GetSpeedVector (currentTarget);
		NextTarget ();
	}

	public void NextTarget () {
		if (nextTargets.Count > 0) {
			currentTarget = nextTargets.Dequeue ();
			targetMark.SetActive (true);
			targetMark.transform.position = new Vector3 (
				currentTarget.x,
				currentTarget.y,
				-3
			);
			currentSpeed = GetSpeedVector (currentTarget);
		} else {
			currentTarget = default(Vector2);
			targetMark.SetActive (false);
			currentSpeed = Vector2.zero;
		}
	}

	Vector2 GetSpeedVector (Vector2 target) {
		Vector2 dist = (target - chiefRigid.position);
		dist.Normalize ();
		return new Vector2(
			dist.x * maxSpeed,
			dist.y * maxSpeed
		);
	}	

	Vector2 Vector3to2(Vector3 vec) {
		return new Vector2(vec.x, vec.y);
	}
}
