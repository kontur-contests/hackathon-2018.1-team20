using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PolicemanController : MonoBehaviour {

	public float speed = 10;
	public GameObject field;

	private Action state;
	private Vector2 target;
	private System.Random random = new System.Random();
	private Rigidbody2D rb2d;
	private Animator animator;
	private float fieldWidth;
	private float fieldHeight;
	private int maxCountOfStaying = 100;
	private int countOfStaying = 0;
	private GameObject victim;

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D>();
		fieldWidth = field.transform.localScale.x;
		fieldHeight = field.transform.localScale.y;
		state = Stay;
	}
	
	// Update is called once per frame
	void Update () {
		state();
	}

	private void Walk() 
	{
		//animator.SetBool("IsShoting", false);
		var delta = target - (Vector2)transform.position;
		rb2d.velocity = delta.normalized * speed;
		transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg - 90);
		if (Vector2.Distance(target, transform.position) < 10) 
		{
			countOfStaying = 0;
			state = Stay;
		}
	}

	private void Stay() 
	{
		//animator.SetBool("IsShoting", false);
		countOfStaying++;
		if (countOfStaying >= maxCountOfStaying)
		{
			var x = random.Next((int)fieldWidth);
			var y = random.Next((int)fieldHeight);
			target = new Vector2(field.transform.position.x - fieldWidth / 2 + x, field.transform.position.y - fieldHeight / 2 + y);
			state = Walk;
		}
	}

	private void RunToTarget() {
		if (victim == null){
			state = Stay;
			return;
		}
		var delta = (Vector2)victim.gameObject.transform.position - (Vector2)transform.position;
		rb2d.velocity = delta.normalized * speed;
		transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg - 90);
	}

	void OnCollisionEnter2D(Collision2D other) 
	{
		countOfStaying = 0;
		state = Stay;
	}
}
