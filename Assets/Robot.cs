using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour {

	[SerializeField]
	private int _hunger;
	[SerializeField]
	private int _happiness;


	private int cuddleIndex = 0;
	private int wait = 0;
	Animator animator; 
	//private bool _serverTime;
	// Use this for initialization
	void Start () {
		//GetComponent <Animator>("robot_blue").enabled = false;
	//	Animation bla = gameObject.GetComponent<Animation>("blue_robot");
		/*Animator blueRobot = GetComponent<Animator> ();
		blueRobot.enabled = false;
		Animator petting = GetComponent<Animator> ();
		petting.enabled = true;*/
		
		
		  Animator anim = GetComponent<Animator>();
     AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
	 Debug.Log(stateInfo);
 /*
     // APPLY DEFAULT ROOT MOTION, ONLY WHEN IN THESE ANIMATION STATES
     if (stateInfo.fullPathHash == Animator.StringToHash("blue_robot"))
     {
         anim.ApplyBuiltinRootMotion();
     }*/
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.touchCount > 0) {
			if (Input.GetTouch (0).deltaPosition.x > 0 || 
				Input.GetTouch (0).deltaPosition.x < 0 ||
				Input.GetTouch (0).deltaPosition.y > 0) {
				//	Debug.Log ("user is swiping to the right");
				//changepic ();
			}


		} else {
		//	GoBack ();
		}
	}

	void changepic(){
		//Animator petting = GetComponent<Animator> ();
		//petting.enabled = false;
		//Animator blueRobot = GetComponent<Animator> ();
		//blueRobot.enabled = true;
		//GetComponent<Animator> ("robot_blue").enabled = true;
		animator.runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load("robot_blue", typeof(RuntimeAnimatorController));
	}

	void G(){/*
		GetComponent <Animator>("robot_blue").enabled = false;
		GetComponent<Animator> ("Petting").enabled = true;
	*/}


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
