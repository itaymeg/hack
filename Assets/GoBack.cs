﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoBack : MonoBehaviour {

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

			/*
			if (fingerPos.x > 300 && fingerPos.y > 200)
			{
				Debug.Log(fingerPos.x);
				SceneManager.LoadScene("DogDetails");
			}
			*/
			if (fingerPos.x < 140 && fingerPos.y > 640)
			{
				
				Debug.Log("ok" +fingerPos.x);
				SceneManager.LoadScene("Game");
			}
		}
	}
}
