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

			if (fingerPos.x > 275 && fingerPos.y < -285)
			{
				Debug.Log(fingerPos.x);
				SceneManager.LoadScene("Game");
			}
		}
	}
}
