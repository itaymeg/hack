using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Adopt : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.touchCount > 0)
		{
			
			Vector2 fingerPos = Input.GetTouch(0).position;
			Debug.Log (fingerPos.x);
			Debug.Log (fingerPos.y);
			if (fingerPos.x > 570 && fingerPos.y < 288) {
				Debug.Log (fingerPos.x);
				SceneManager.LoadScene ("First Level Finish");
			} else if (fingerPos.x < 570 && fingerPos.x > 440 && fingerPos.y < 66 && fingerPos.y > 173) {
				SceneManager.LoadScene ("Adopt Page");
			}
		}
	}
}
