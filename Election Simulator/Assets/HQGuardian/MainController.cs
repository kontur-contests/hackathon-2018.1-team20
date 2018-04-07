using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainController : MonoBehaviour {

	public GameObject scoreField;

	int volunteersLost;
	int volunteersHired;
	int enemyPrevented;
	int enemyInvasions;

	public int totalScore {
		get {
			return volunteersHired * 2 + volunteersLost * -2 + enemyPrevented * 2 + enemyInvasions * -2;
			}
	}

	public enum VisitorAction {
		PASS,
		BLOCK
	};

	public enum VisitorType {
		VOLUNTEER,
		ENEMY
	};

	public void triggerAction(VisitorType vis, VisitorAction act) {
		if (vis == VisitorType.VOLUNTEER) {
			if (act == VisitorAction.BLOCK) {
				volunteersLost++;
			} else {
				volunteersHired++;
			}
		} else {
			if (act == VisitorAction.BLOCK) {
				enemyPrevented++;
			} else {
				enemyInvasions++;
			}
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		scoreField.GetComponent<Text> ().text = totalScore.ToString ();
	}
}
