using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScoreCircleScript : MonoBehaviour
{
	public GameObject Logic;
	private Dictionary<KeyCode, Direction> keyToDirection = new Dictionary<KeyCode, Direction>()
	{
		{KeyCode.LeftArrow, Direction.Left},
		{KeyCode.RightArrow, Direction.Right},
		{KeyCode.UpArrow, Direction.Up},
		{KeyCode.DownArrow, Direction.Down},
	};
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
		bool wasInput = false;
		foreach(var kv in keyToDirection){
			if (Input.GetKeyDown(kv.Key)){
				wasInput = true;
				var collided = currentCollidedObjects.FirstOrDefault(
				o => o.GetComponent<ArrowScript>().Direction == kv.Value);

				if (collided != null) {
					Logic.GetComponent<MainScript>().IncrementScore(); 
					currentCollidedObjects.Remove(collided);
					return;
				}
			}
		}
		if (wasInput) Logic.GetComponent<MainScript>().DecrementScore();
		
	}

	void HandleKey() {
		
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