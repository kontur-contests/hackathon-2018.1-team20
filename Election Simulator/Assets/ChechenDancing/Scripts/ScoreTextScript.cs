using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTextScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Blink(Color color, float delayInSeconds){
		StartCoroutine(BlinkCoroutine(color, delayInSeconds));
	}
	private IEnumerator BlinkCoroutine(Color color, float delayInSeconds){
		var textComponent = this.gameObject.GetComponent<Text>();
		var firstColor = Color.white;
		textComponent.color = color;
		yield return new WaitForSeconds(delayInSeconds);
		textComponent.color = firstColor;
	}
}
