using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager: MonoBehaviour {
	public GameObject flashText; 

	// Use this for initialization
	void Start () {
		InvokeRepeating ("flashTheText", 0f, 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log ("bla");
			if (Input.touchCount > 0) {
			Debug.Log ("asdf");
			SceneManager.LoadScene("Game");
		}
	}

	void flashTheText(){
		if (flashText.activeInHierarchy)
			flashText.SetActive (false);
		else
			flashText.SetActive(true);
	}
}
