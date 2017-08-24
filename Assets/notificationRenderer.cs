using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class notificationRenderer : MonoBehaviour {
	private Image notification;
	private long a;
	private Image m;
	// Use this for initialization
	void Start () {
		a = DateTime.Now.Ticks;
		Debug.Log ("notification2");
		Transform notificationObj = transform.Find ("Notification");
	    m = notificationObj.GetComponent<Image> ();
		m.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	//	Debug.Log ("notification");
		long b =  DateTime.Now.Ticks;
		if ((b) - (a) > 50000000) {
			a = b;
			m.enabled = true;
		}
	}

	public static long DateTimeToInt(DateTime dateee){
		return (long)(dateee.Date - new DateTime (1900, 1, 1)).TotalMilliseconds;
	}
}
