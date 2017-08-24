using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public GameObject hungerText;
	public GameObject robot;
	// Update is called once per frame
	void Update () {
		hungerText.GetComponent<Text> ().text = robot.GetComponent<Robot> ().hunger.ToString();
	}
}
