using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScript : MonoBehaviour {

	private const int DefaultFallSpeed = 2;
	private int arrowSpeed;
	private Queue<GameObject> arrows;
	private float secondDelay = 1;
	public GameObject arrow;
	void Start () {
		StartCoroutine(arrowCreator());
		//var i = Instantiate(arrow, new Vector2(12.5f, 5.5f), Quaternion.identity);
		//var r = i.GetComponent<Rigidbody2D>();
		//r.velocity = new Vector2(0, -2);
	}

	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator arrowCreator()
	{
		while (true)
		{
			var instance = Instantiate(arrow, new Vector2(12.5f, 5.5f), Quaternion.identity);
			var r = instance.GetComponent<Rigidbody2D>();
			r.velocity = new Vector2(0, -3);
			yield return new WaitForSecondsRealtime(secondDelay);
		}
	}
} 
