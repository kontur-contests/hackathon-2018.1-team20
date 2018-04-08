using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ReporterController : Human {

	public float speed = 10;
	private float processAgitation = 0;
	public float difficultAgitation;
	public GameObject field;
	public GameObject humanPrefab;
	public GameObject reporterPrefab;
	public GameObject policemanPrefab;
	public GameObject policemanSpawn;

	private Action state;
	private Vector2 target;
	private System.Random random = new System.Random();
	private Rigidbody2D rb2d;
	private Animator animator;
	private float fieldWidth;
	private float fieldHeight;
	private int maxCountOfStaying = 100;
	private int countOfStaying = 0;
	private int maxCountOfAgitated = 60 * 15;
	private int countOfAgitated = 0;

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D>();
		state = GoToField;	
		animator = GetComponent<Animator>();
		fieldWidth = field.transform.localScale.x;
		fieldHeight = field.transform.localScale.y;
	}
	
	// Update is called once per frame
	void Update () {	
		if (!isAgitated && processAgitation >= 100) 
		{
			isAgitated = true;
			CreatePrefabs();
		}
		if (isAgitated) countOfAgitated++;
		if (isAgitated && countOfAgitated >= maxCountOfAgitated){
			isAgitated = false;
			countOfAgitated = 0;
			processAgitation = 0;
		}
		state();
		animator.SetBool("IsWalking", rb2d.velocity.magnitude > 0);
	}

	void FixedUpdate() {
		animator.SetBool("IsWalking", rb2d.velocity.magnitude > 0);
	}

	private void CreatePrefabs() {
		for (var i = 0; i < 3; i++)
		{
			var human = Instantiate(humanPrefab, new Vector3(field.transform.position.x - 30, field.transform.position.y - 12 - i * 5, 0), transform.rotation);
			var hc = human.GetComponent<HumanController>();
			hc.field = field;
		}
		var reporter = Instantiate(reporterPrefab, new Vector3(field.transform.position.x, field.transform.position.y + 25, 0), transform.rotation);
		var rc = reporter.GetComponent<ReporterController>();
		rc.field = field;
		rc.policemanSpawn = policemanSpawn;
		var policeman = Instantiate(policemanPrefab, policemanSpawn.gameObject.transform.position, transform.rotation);
		var pc = policeman.GetComponent<PolicemanController>();
		pc.field = field;
		pc.spawn = policemanSpawn;
	}

	private void Stay() 
	{
		animator.SetBool("IsShoting", false);
		countOfStaying++;
		if (countOfStaying >= maxCountOfStaying)
		{
			var x = random.Next((int)fieldWidth);
			var y = random.Next((int)fieldHeight);
			target = new Vector2(field.transform.position.x - fieldWidth / 2 + x, field.transform.position.y - fieldHeight / 2 + y);
			state = Walk;
		}
	}

	private void Walk() 
	{
		animator.SetBool("IsShoting", false);
		var delta = target - (Vector2)transform.position;
		rb2d.velocity = delta.normalized * speed;
		transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg - 90);
		if (Vector2.Distance(target, transform.position) < 10) 
		{
			countOfStaying = 0;
			state = Stay;
		}
	}


	private float lastProgress = 0;
	private void GetAgitation() {
		if (lastProgress == processAgitation) {
			state = Stay;
			countOfStaying = 10;	
		}
		animator.SetBool("IsShoting", true);
		var delta = target - (Vector2)transform.position;
		transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg - 90);
		lastProgress = processAgitation;
	}

	private void GoToField() {
		animator.SetBool("IsShoting", false);
		var delta = (Vector2)field.transform.position - (Vector2)transform.position;
		rb2d.velocity = delta.normalized * speed;
		transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg - 90);
	}

	public override void SetAgitation(Vector2 agitatorPosition, float agitationPower) 
	{
		if (isAgitated) return;
		processAgitation += agitationPower * difficultAgitation;
		target = agitatorPosition;
		state = GetAgitation;
	}

	void OnCollisionEnter2D(Collision2D other) 
	{
		countOfStaying = 0;
		state = Stay;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag.Equals("Field")) {
			state = Stay;
			GetComponent<CircleCollider2D>().isTrigger = false;	
		}
	}
}
