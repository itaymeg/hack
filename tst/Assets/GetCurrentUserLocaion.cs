using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class GetCurrentUserLocaion : MonoBehaviour {
    static string LocationString = "";
    static float lastLongitude = 0;
    static float lastLatitude = 0;
    private void Awake()
    {
        Debug.Log("Loaded Location Services");
    }
    IEnumerator Start()
	{
		// First, check if user has location service enabled
		if (!Input.location.isEnabledByUser)
			yield break;

        // Start service before querying location
        if(Input.location.status != LocationServiceStatus.Running) {
           Input.location.Start(); 
        }
		

		// Wait until service initializes
		int maxWait = 20;
		while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
		{
			yield return new WaitForSeconds(1);
			maxWait--;
		}

		// Service didn't initialize in 20 seconds
		if (maxWait < 1)
		{
            LocationString = "Timed out";
            Changed();
			yield break;
		}

		// Connection has failed
		if (Input.location.status == LocationServiceStatus.Failed)
		{
            LocationString = "Unable to determine device location";
            Changed();
			yield break;
		}
		else
		{
            // Access granted and location value could be retrieved
            if(CheckLocationChanged())
            {
                TriggerMove();
            }
            LocationString = "Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp;
            Changed();
        }


	}

    public static bool GetLocationSafe()
    {
        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
            return false;

        // Start service before querying location
        if (Input.location.status != LocationServiceStatus.Running)
        {
            Input.location.Start();
        }


        // Wait until service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            Thread.Sleep(1000);
            maxWait--;
        }

        // Service didn't initialize in 20 seconds
        if (maxWait < 1)
        {
            LocationString = "Timed out";
            Changed();
            return false;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            LocationString = "Unable to determine device location";
            Changed();
            return false;
        }
        else
        {
            // Access granted and location value could be retrieved
            return true;
        }
        return false;
    }

    private void TriggerMove()
    {
        //CrossPlatformInputManager.SetAxis("Horizontal",?);
        //CrossPlatformInputManager.SetAxis("Vertical",?);
    }

    private bool CheckLocationChanged()
    {
        if( isWithinEpsilon(lastLatitude, 0) ||  isWithinEpsilon(lastLongitude, 0)){ // not first time
            return !isWithinEpsilon(Input.location.lastData.latitude, lastLatitude)
                      || !isWithinEpsilon(Input.location.lastData.longitude, lastLongitude);
        }
        return false;
    }

    private void OnGUI()
    {
        // for debugging puposes without a debugger log
        //GUI.Label(new Rect(10,60,200, 200), LocationString);
    }
    private void OnDisable()
    {
        // Stop service if there is no need to query location updates continuously
        if(Input.location.status != LocationServiceStatus.Stopped) {
          Input.location.Stop();  
        }
    }
    static void Changed()
    {
        Debug.Log("Changed" + LocationString);
    }

    private bool isWithinEpsilon(float n1, float n2, float epsilon = 0.001f)
    {
        return (n2 - epsilon <= n1) && (n1 <= n2 + epsilon);
    }
}
