using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseThis : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.touchCount > 0)
		{
			Vector2 fingerPos = Input.GetTouch(0).position;
			if (fingerPos.x > 150 && fingerPos.x < 400 && fingerPos.y < -200 && fingerPos.y > -400)
			{
				Debug.Log(fingerPos.x);
				SceneManager.LoadScene("Picked Dog");
			}
		}
	}
}
