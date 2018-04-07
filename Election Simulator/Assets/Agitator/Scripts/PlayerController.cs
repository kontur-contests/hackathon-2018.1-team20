using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;
	public bool isAgitating = false;
	private Rigidbody2D rb2d;
	private Animator animator;

	void Start () {
		rb2d = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
	}
	
	void Update () {
		var tmp = Input.GetKey("space");
		if (isAgitating != tmp) {

		isAgitating = tmp;
		animator.SetBool("IsAgitating", isAgitating);
		}
	}

	void FixedUpdate() {
		Move();
		animator.SetBool("IsWalking", rb2d.velocity.magnitude > 0);
	}

	private void Move() {
		var x = Input.GetKey("a") ? -1 : Input.GetKey("d") ? 1 : 0;
		var y = Input.GetKey("s") ? -1 : Input.GetKey("w") ? 1 : 0;
		var direction = new Vector2(x, y) * speed;
		rb2d.velocity = direction;
		if (x == 0 && y == 0) return;
		transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90);
	}

	private float GetAngleBetween(Vector2 center, Vector2 target) {
		var delta = target - center;
		return Mathf.Atan2(delta.y, delta.x);
	}
}
