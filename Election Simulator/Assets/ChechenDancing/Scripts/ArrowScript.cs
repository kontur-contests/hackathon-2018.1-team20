using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{

	private Vector2 speed;
	private Direction direction;
	public Direction Direction
	{
		get { return direction; }
		set
		{
			var spriteRenderer = this.gameObject.GetComponent<SpriteRenderer> ();
			spriteRenderer.sprite = GetSpriteByDirection(value);
			direction = value;
		}
	}
	void Start ()
	{

	}

	void Update ()
	{
		if (transform.position.y < 0)
		{
			Destroy (this.gameObject);
		}
	}

	Sprite GetSpriteByDirection (Direction d)
	{
		if (d == Direction.Up)
			return Resources.Load<Sprite> ("CechenDancer/up");
		if (d == Direction.Right)
			return Resources.Load<Sprite> ("CechenDancer/right");
		if (d == Direction.Left)
			return Resources.Load<Sprite> ("CechenDancer/left");
		if (d == Direction.Down)
			return Resources.Load<Sprite> ("CechenDancer/down");
		return null;
	}
}