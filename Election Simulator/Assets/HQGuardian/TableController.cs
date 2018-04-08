using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableController : MonoBehaviour {

	public GameObject exit;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D (Collider2D col) {
		if (col.CompareTag("Human")) {
			col.gameObject.GetComponent<VisitorController> ().JoinNavalny (exit.GetComponent<Rigidbody2D>().position);
		}
	}
}
