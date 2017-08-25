using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMap : MonoBehaviour {
    
	IEnumerator Start () {
        WWW www = new WWW("http://maps.google.com/maps/api/staticmap?center=32.11052,34.83603&zoom=17&size=1980x1024&type=hybrid&sensor=true&key=AIzaSyB-2-J3nsP9ZTGg6h3iwBqTJmRvi8chHOI");
        yield return www;
        Debug.Log(www);
        Material material = new Material(Shader.Find("Diffuse"));
       // material.color = new Color(210, 105, 30);
        material.mainTexture = www.texture as Texture;
        GetComponent<Renderer>().material = material;
    }
	
	// Update is called once per frame
	void Update () {
      
    }
    
}
