using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager1 : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//gameObject.SetActive(false)
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.touchCount > 0) {




			Vector2 fingerPos = Input.GetTouch (0).position;

			Debug.Log (fingerPos.x);
			Debug.Log (fingerPos.y);

			if (fingerPos.x > 800 && fingerPos.y > 700) {


				SceneManager.LoadScene("DogDetails");
			}

			if (fingerPos.x > 108 && fingerPos.x < 180 && fingerPos.y > 425 && fingerPos.y < 523) {


				SceneManager.LoadScene("3DWalking");
			}

			if (fingerPos.x > 800 && fingerPos.y < 700 && fingerPos.y > 500 ) {

				SceneManager.LoadScene("Pee");
			}
			if (fingerPos.x < 70 && fingerPos.y > 220) {
	
				SceneManager.LoadScene("Game");
			}
			if (fingerPos.x < 40 && fingerPos.y > 350 && fingerPos.y < 500) {
				
				SceneManager.LoadScene("Store");
			}
            /*
            if (fingerPos.x > ***someValue * * && fingerPos.y > ***someValue***)
            {
                SceneManager.LoadScene("3DWalking");
            }
            */
		}
	}
}
