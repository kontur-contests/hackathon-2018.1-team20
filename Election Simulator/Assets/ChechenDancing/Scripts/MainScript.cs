using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainScript : MonoBehaviour
{
	private const float lengthInArrowTicks = 90;
	public GameObject arrowPrefab;
	public GameObject scoreTextObject;
	private System.Random random;
	private int currentScore = 0;
	void Start ()
	{
		random = new System.Random ();
		StartCoroutine (arrowCreator ());
	}

	void Update ()
	{

	}

	IEnumerator arrowCreator ()
	{
        var ticksElapsed = 0;
		float velocityModifier = 1;
		while (ticksElapsed < lengthInArrowTicks)
		{
			var instance = Instantiate (arrowPrefab);
			var direction = GetRandomDirection ();
			instance.GetComponent<ArrowScript> ().Direction = GetRandomDirection ();

			var r = instance.GetComponent<Rigidbody2D> ();
			r.velocity = new Vector2 (-2 * velocityModifier, 0);

			var delay = GetRandomDelay();
			ticksElapsed += delay;

			if (ticksElapsed % 10 == 0)
				velocityModifier = 1 + ticksElapsed / lengthInArrowTicks;


			yield return new WaitForSeconds ((float) delay / 2);
		}
        SceneManager.LoadScene("chechnya_final");

    }

    int GetRandomDelay ()
	{
		return random.Next (1, 4);
	}

	Direction GetRandomDirection ()
	{
		var values = Enum.GetValues (typeof (Direction));
		var result = (Direction) values.GetValue (random.Next (values.Length));
		return result;
	}
	public void IncrementScore(){
		currentScore += 10;
		scoreTextObject.GetComponent<Text>().text = currentScore.ToString();
		scoreTextObject.GetComponent<ScoreTextScript>().Blink(Color.green, 0.25f);
	}

	public void DecrementScore(){
		currentScore -= 10;
		scoreTextObject.GetComponent<Text>().text = currentScore.ToString();
		scoreTextObject.GetComponent<ScoreTextScript>().Blink(Color.red, 0.25f);
	}
}