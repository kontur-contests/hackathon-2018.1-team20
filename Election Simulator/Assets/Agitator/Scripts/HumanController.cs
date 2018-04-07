using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HumanController : MonoBehaviour 
{

	public float speed = 10;
	public bool isAgitated = false;
	private float processAgitation = 0;
	public float difficultAgitation;
	public GameObject field;

	private Action state;
	private Vector2 target;
	private System.Random random = new System.Random();
	private Rigidbody2D rb2d;
	private float fieldWidth;
	private float fieldHeight;
	private int maxCountOfStaying = 100;
	private int countOfStaying = 0;

	// Use this for initialization
	void Start () 
	{
		rb2d = GetComponent<Rigidbody2D>();
		state = Walk;	
		fieldWidth = field.transform.localScale.x;
		fieldHeight = field.transform.localScale.y;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!isAgitated && processAgitation >= 100) 
		{
			isAgitated = true;
		}
		state(); 
	}

	public void SetAgitation(float agitationPower) 
	{
		if (isAgitated) return;
		processAgitation += agitationPower * difficultAgitation;
	}

	private float GetAngleBetween(Vector2 center, Vector2 target) {
		var delta = target - center;
		return Mathf.Atan2(delta.y, delta.x);
	}

	private void Walk() 
	{
		if (target == null) 
		{
			state = Stay;
			return;
		}
		var delta = target - (Vector2)transform.position;
		rb2d.velocity = delta.normalized * speed;
		transform.rotation = Quaternion.Euler(0, 0, GetAngleBetween(transform.position, target) * Mathf.Deg2Rad);
		if (Vector2.Distance(target, transform.position) < 10) 
		{
			countOfStaying = 0;
			state = Stay;
		}
	}

	private void Stay() 
	{
		countOfStaying++;
		if (countOfStaying >= maxCountOfStaying)
		{
			var x = random.Next((int)fieldWidth);
			var y = random.Next((int)fieldHeight);
			Debug.Log(x+ " " + y);
			Debug.Log((field.transform.position.x - fieldWidth / 2 + x) + " " + (field.transform.position.y - fieldHeight / 2 + y));
			Debug.Log(field.transform.position.x - fieldWidth / 2);
			target = new Vector2(field.transform.position.x - fieldWidth / 2 + x, field.transform.position.y - fieldHeight / 2 + y);
			state = Walk;
		}
	}

}
