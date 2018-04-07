using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgitationController : MonoBehaviour {

	PlayerController playerController;

	void Start () {
		playerController = transform.parent.GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other) {
		OnTriggerStay2D(other);
	}

	void OnTriggerStay2D(Collider2D other) {
		if (!playerController.isAgitating) return;
		if (!other.gameObject.tag.Equals("Human")) return;
		var hc = other.gameObject.GetComponent<Human>();
		if (!hc.isAgitated) hc.SetAgitation(transform.parent.gameObject.transform.position, 1);
	}
}
