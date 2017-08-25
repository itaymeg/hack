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
			Debug.Log (fingerPos.x);
			Debug.Log (fingerPos.y);
			if (fingerPos.x < 300 && fingerPos.y < 300)
			{
				Debug.Log(fingerPos.x);
				SceneManager.LoadScene("Picked Dog");
			}
		}
	}
}
