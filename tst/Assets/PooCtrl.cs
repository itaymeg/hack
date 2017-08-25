using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooCtrl : MonoBehaviour {
    public GameObject Poo;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            MakePoopGoAway();
        }
    
    int i = 0;
        while (i < Input.touchCount)
        {

            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                //Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position), -Vector2.up);

                Debug.Log("TAP !");
                MakePoopGoAway();

            }
            ++i;
        }

    }
    void MakePoopGoAway()
    {
        Poo.SetActive(false);
    }
}
