using UnityEngine;
using System.Collections;
using System.Windows.Forms;
using Assets.Scripts.MazeSolver;

public class MazeViewer : MonoBehaviour {

    protected Form Form { get; set; }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.W))
        {
            MazeManager.Start();
        }
	}
}
