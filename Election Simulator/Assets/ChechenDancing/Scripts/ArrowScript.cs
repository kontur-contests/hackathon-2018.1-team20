using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour {

	private Vector2 speed;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y < 0) {
			Destroy(this.gameObject);
		}
	}
}
