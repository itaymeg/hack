using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;
using System;
using UnityEngine.UI;

public class NotificationManager : MonoBehaviour {
	private Image notification;
	// Use this for initialization
	void Start () {
	/*	GameObject g = GameObject.Find ("Canvas");
		notification = g.GetComponent<Image> ();
		Debug.Log (notification);*/
	//	notification.enabled = false;
	//	var a = DateTime.Now;
	//	gameObject.SetActive (false);

		//bla.GetComponent<Image> ().text = robot.GetComponent<Robot> ().hunger.ToString();

	}
	
	// Update is called once per frame
	void Update () {	 
		/*DateTime b =  DateTime.Now;
		Debug.Log (a.Millisecond);
		if (a.Millisecond - b.Millisecond > 2000) {
			Debug.Log (a + "sdf");
			a = b;
		}*/

			
	}


	 void TimerCallback(object sender, ElapsedEventArgs e){
		Debug.Log ("aaa");
		//gameObject.SetActive (true);	
		var sprite_renderer = GetComponent<SpriteRenderer> ();
		sprite_renderer.enabled = false;
	}
}
