using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCircleScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collision2D col)
	{
		Destroy(col.otherCollider.gameObject);
	}
}
