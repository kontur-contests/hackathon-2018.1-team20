using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;
	public bool isAgitating = false;
	private Rigidbody2D rb2d;

	void Start () {
		rb2d = GetComponent<Rigidbody2D>();
	}
	
	void Update () {
		isAgitating = Input.GetMouseButton(0);
		RotateToCursor();
	}

	void FixedUpdate() {
		Move();
	}

	private void RotateToCursor() {
		var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, GetAngleBetween(transform.position, mousePosition) * Mathf.Rad2Deg));
	}

	private void Move() {
		var x = Input.GetKey("a") ? -1 : Input.GetKey("d") ? 1 : 0;
		var y = Input.GetKey("s") ? -1 : Input.GetKey("w") ? 1 : 0;
		rb2d.velocity = new Vector2(x, y) * speed;
	}

	private float GetAngleBetween(Vector2 center, Vector2 target) {
		var delta = target - center;
		return Mathf.Atan2(delta.y, delta.x);
	}
}
