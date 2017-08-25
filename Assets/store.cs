using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class store : MonoBehaviour {

	[SerializeField]
	private int _money;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.touchCount > 0) {




			Vector2 fingerPos = Input.GetTouch (0).position;

			Debug.Log (fingerPos.x);
			Debug.Log (fingerPos.y);

			if (fingerPos.y< 700) {
				GameObject.Find ("Money").GetComponent<Text> ().text = "83"; 
				//PlayerPrefs.SetString ("Money", "80");
			}
	}
}
}

