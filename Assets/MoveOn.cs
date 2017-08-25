using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveOn : MonoBehaviour {

	private long a;
	// Use this for initialization
	void Start()
	{
		a = DateTime.Now.Ticks;

	}

	// Update is called once per frame
	void Update()
	{
		//	Debug.Log ("notification");
		long b = DateTime.Now.Ticks;
		if ((b) - (a) > 10000000)
		{
			SceneManager.LoadScene("Game");
		}
	}
}
