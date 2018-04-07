using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScoreCircleScript : MonoBehaviour
{

	HashSet<GameObject> currentCollidedObjects;
	void Start ()
	{
		currentCollidedObjects = new HashSet<GameObject> ();
	}

	// Update is called once per frame
	void Update ()
	{

	}

	void FixedUpdate ()
	{
		
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		currentCollidedObjects.Add (other.gameObject);
	}
	void OnTriggerExit2D (Collider2D other)
	{
		currentCollidedObjects.Remove (other.gameObject);
		Destroy (other.gameObject);
	}
}