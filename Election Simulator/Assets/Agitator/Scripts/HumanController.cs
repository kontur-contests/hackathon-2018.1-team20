using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HumanController : Human 
{

	public float speed = 10;
	private float processAgitation = 0;
	public float difficultAgitation;
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
	private GameObject policeman;

	// Use this for initialization
	void Start () 
	{
		rb2d = GetComponent<Rigidbody2D>();
		state = GoToField;	
		animator = GetComponent<Animator>();
		fieldWidth = field.transform.localScale.x;
		fieldHeight = field.transform.localScale.y;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!isAgitated && processAgitation >= 100) 
		{
			isAgitated = true;
			animator.SetBool("IsAgitated", isAgitated);
		}
		state(); 

		animator.SetBool("IsWalking", rb2d.velocity.magnitude < 0.5 ? false : true);
	}

	void FixedUpdate() {
		animator.SetBool("IsWalking", rb2d.velocity.magnitude > 0);
	}

	public override void SetAgitation(Vector2 agitatorPosition, float agitationPower) 
	{
		if (isAgitated) return;
		processAgitation += agitationPower * difficultAgitation;
		target = agitatorPosition;
		state = GetAgitation;
	}

	private float GetAngleBetween(Vector2 center, Vector2 target) {
		var delta = target - center;
		return Mathf.Atan2(delta.y, delta.x);
	}

	private void GoToField() {
		var delta = (Vector2)field.transform.position - (Vector2)transform.position;
		rb2d.velocity = delta.normalized * speed;
		transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg - 90);
	}

	private void Walk() 
	{
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
		countOfStaying++;
		if (countOfStaying >= maxCountOfStaying)
		{
			var x = random.Next((int)fieldWidth);
			var y = random.Next((int)fieldHeight);
			target = new Vector2(field.transform.position.x - fieldWidth / 2 + x, field.transform.position.y - fieldHeight / 2 + y);
			state = Walk;
		}
	}

	private float lastProgress = 0;
	private void GetAgitation() {
		if (lastProgress == processAgitation) {
			state = Stay;
			countOfStaying = 10;
		}
		var delta = target - (Vector2)transform.position;
		transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg - 90);
		lastProgress = processAgitation;
	}

	private void MoveToPoliceman() {
		var delta = (Vector2)policeman.transform.position - (Vector2)transform.position;
		rb2d.velocity = delta.normalized * speed;
		transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg - 90);
	}

	void OnCollisionEnter2D(Collision2D other) 
	{
		if (policeman != null) return;
		countOfStaying = 0;
		state = Stay;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag.Equals("Field")) {
			state = Stay;
			GetComponent<CircleCollider2D>().isTrigger = false;	
		}
	}

	public void SetArrested(GameObject policeman) {
		animator.SetBool("IsArrested", true);
		this.policeman = policeman;
		state = MoveToPoliceman;
	}

}
