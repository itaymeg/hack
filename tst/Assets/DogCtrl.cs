using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogCtrl : MonoBehaviour {
    public GameObject Poo;
    public Animator my_Animator;
    public Animator fatherAnimator;
    private int timeToPeeIndex;
	// Use this for initialization
	void Start () {
        Poo.SetActive(false);
        my_Animator = GetComponent<Animator>();
        timeToPeeIndex = 0;
	}

    // Update is called once per frame
    void Update()
    {
        bool skip = false;
        if(timeToPeeIndex == -1)
        {
            my_Animator.SetBool("TimeToIdle", true);
            my_Animator.SetBool("TimeToPee", false);
            skip = true;
        }
        bool timeToPee = isTimeToPee();
        
        if(timeToPee)
        {
            my_Animator.SetBool("TimeToPee", timeToPee);
            my_Animator.SetBool("TimeToIdle", false);
            Poo.SetActive(true);
        }
        if (my_Animator.GetFloat("Forward") > 0.3f)
        {
            if(!skip) {
                my_Animator.SetBool("TimeToIdle", false);   
            }

        }
        my_Animator.SetFloat("Forward", fatherAnimator.GetFloat("Forward"));
        if (my_Animator.GetFloat("Forward") < 0.01f) {
            if (!my_Animator.GetBool("TimeToPee"))
            {
                my_Animator.SetBool("TimeToIdle", true);
            }
        }
        
        //Debug.Log(my_Animator.GetFloat("Forward").ToString());
    }

    private bool isTimeToPee()
    {
        if (timeToPeeIndex == 750)
        {
            timeToPeeIndex = -140;
            return true;
        }
        else
        {
            timeToPeeIndex++;
            return false;
        }
    }

    private bool isEpsilonFrom(float n1, float n2, float epsilon = 0.01f)
    {
        return n2 - epsilon <= n1 && n1 <= n2 + epsilon;
    }
}
