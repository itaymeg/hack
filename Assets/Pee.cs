using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pee : MonoBehaviour {

	private int peeIndex = 2;
	private int wait = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.touchCount > 0) {
			if (Input.GetTouch (0).deltaPosition.x > 0) {
			//	Debug.Log ("user is swiping to the right");
				changepic();
			}
 
			if (Input.GetTouch (0).deltaPosition.x < 0) {
			//	Debug.Log ("user is swiping to the left");
				changepic();
			}
 
			if (Input.GetTouch (0).deltaPosition.y < 0) {
			//	Debug.Log ("user is swiping to down");
				changepic();
			}
 
			if (Input.GetTouch (0).deltaPosition.y > 0) {
			//	Debug.Log ("user is swiping to up");
				changepic();
			}
		}

	}

	void changepic(){
		if (peeIndex == 5) {
			SceneManager.LoadScene("Game");
		} 
		else 
		{
			Debug.Log ("pee" + peeIndex);
			if (wait != 8) {
				wait++;
			} else {
				var pee = Resources.Load<Sprite> ("pee" + peeIndex);
				this.GetComponent<SpriteRenderer> ().sprite = pee;
				peeIndex++;
				wait = 0;
			}
		}
	}
}
