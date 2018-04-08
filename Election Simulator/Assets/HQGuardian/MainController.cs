using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainController : MonoBehaviour {

	public GameObject scoreField;
	int volunteersLost;
	int volunteersHired;
	int enemyPrevented;
	int enemyInvasions;

	public int totalScore {
		get {
			return volunteersHired * 2 + volunteersLost * -1 + enemyPrevented * 0 + enemyInvasions * -10;
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
		StartCoroutine ("End");
	}
	
	// Update is called once per frame
	void Update () {
		scoreField.GetComponent<Text> ().text = totalScore.ToString ();
	}

	IEnumerator End() {
		yield return new WaitForSeconds (45);
		SceneManager.LoadScene ("LoseScene");
	}
}
