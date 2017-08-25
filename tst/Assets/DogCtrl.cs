using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogCtrl : MonoBehaviour {
    public Animator my_Animator;
    public Animator fatherAnimator;
	// Use this for initialization
	void Start () {
        my_Animator = GetComponent<Animator>();
	}

    // Update is called once per frame
    void Update()
    {
        if (my_Animator.GetFloat("Forward") > 0.3f)
        {
            my_Animator.SetBool("TimeToIdle", false);
        }
        my_Animator.SetFloat("Forward", fatherAnimator.GetFloat("Forward"));
        if (my_Animator.GetFloat("Forward") < 0.01f) {
            my_Animator.SetBool("TimeToIdle", true);
        }
        
        Debug.Log(my_Animator.GetFloat("Forward").ToString());
    }

    private bool isEpsilonFrom(float n1, float n2, float epsilon = 0.01f)
    {
        return n2 - epsilon <= n1 && n1 <= n2 + epsilon;
    }
}
