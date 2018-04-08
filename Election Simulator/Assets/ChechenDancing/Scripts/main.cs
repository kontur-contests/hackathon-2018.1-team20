using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class main : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine(wait());
		
	}
	IEnumerator wait (){
		yield return new WaitForSeconds(3);
		SceneManager.LoadScene("ChechenDancing/Scenes/MainDance");
	}
	
}
