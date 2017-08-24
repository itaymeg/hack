using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using UnityEngine;

public class Menu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        var form = new Form();
        form.ShowDialog();
        print("openDialog");	
	}
}
