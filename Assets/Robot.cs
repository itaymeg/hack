using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour {

	[SerializeField]
	private int _hunger;
	[SerializeField]
	private int _happiness;

	//private bool _serverTime;
	// Use this for initialization
	void Start () {
		updateStatus ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void updateStatus(){
		if (!PlayerPrefs.HasKey ("hunger")) {
			_hunger = 100;
			PlayerPrefs.SetInt ("hunger", _hunger);
		} else {
			_hunger = PlayerPrefs.GetInt ("hunger");

		}

	/*	if (_serverTime) {
			updateServer ();
		} else {*/
			updateDevice ();
		//}
	}

	public int hunger{
		get {return _hunger;}
		set {_hunger = value;}
	}

	void updateServer(){

	}

	void updateDevice(){

	}
}
