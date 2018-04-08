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
	private GameObject cutchedVictim;
	public GameObject spawn;

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		fieldWidth = field.transform.localScale.x;
		fieldHeight = field.transform.localScale.y;
		state = Stay;
	}
	
	// Update is called once per frame
	void Update () {
		if (victim == null && cutchedVictim == null) TryFindVictim();
		state();
		animator.SetBool("IsWalking", rb2d.velocity.magnitude > 0);
	}

	void FixedUpdate() {
		animator.SetBool("IsWalking", rb2d.velocity.magnitude > 0);
	}

	private void TryFindVictim() {
		Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 4);
		foreach(var col in colliders) {
			var direction = transform.position - col.gameObject.transform.position;
			var angle = Math.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
			if (transform.rotation.z - 35 < angle && angle < transform.rotation.z + 35) {
				if (col.gameObject.tag.Equals("Human")) {
					var hc = col.gameObject.GetComponent<Human>();
					if (hc is HumanController) {
						if (hc.isAgitated) {
							victim = col.gameObject;
							state = RunToTarget;
							return;
						}
					}
				}
				if (col.gameObject.tag.Equals("Player")) {
					var pc = col.gameObject.GetComponent<PlayerController>();
					if (pc.isAgitating)
					{
                        FindObjectOfType<ScreenFader>().State = ScreenFader.FadeState.In;
					    StartCoroutine(GoNextScene(2));
					}
				}
			}
		}
	}

    IEnumerator GoNextScene(float delay)
    {
        yield return new WaitForSeconds(delay);
        FindObjectOfType<NextLevelLoader>().LoadNextLevel();
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

	private void MoveVictimToAuto() {
		var delta = (Vector2) spawn.transform.position - (Vector2)transform.position;
		rb2d.velocity = delta.normalized * speed;
		transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg - 90);
		if (Vector2.Distance(transform.position, spawn.transform.position) < 0.1f) 
			state = Stay;
	}

	void OnCollisionStay2D(Collision2D other) {
		OnCollisionEnter2D(other);
	}

	void OnCollisionEnter2D(Collision2D other) 
	{
		if (cutchedVictim != null) return;
		if (other.gameObject.tag.Equals("Human")) {
			var hc = other.gameObject.GetComponent<Human>();
			if (!(hc is HumanController)) return;
			if (hc.isAgitated) {
				victim = null;
				cutchedVictim = other.gameObject;
				state = MoveVictimToAuto;
				(hc as HumanController).SetArrested(gameObject);
				return;
			}
		}
		countOfStaying = 0;
		state = Stay;
	}

	void OnTriggerEnter2D(Collider2D other) {
			if (other.gameObject == spawn) {
				if (cutchedVictim != null) {
					Destroy(cutchedVictim);
					cutchedVictim = null;
					state = Stay;
					return;
				}
			}
	}
}
