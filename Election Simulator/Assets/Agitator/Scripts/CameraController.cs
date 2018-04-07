using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject target;
	public GameObject field;
	
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		FollowTo(target.transform.position);
	}

	void FollowTo(Vector2 target) {
		transform.position = new Vector3(target.x, target.y, transform.position.z);
	}
}
