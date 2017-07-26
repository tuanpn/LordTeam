using UnityEngine;
using System.Collections;

public class Resolution : MonoBehaviour {
	public void Start () {
        GetComponent<Camera>().aspect = 800f / 480f;
        Screen.SetResolution(800, 480, true);
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Screen.orientation = ScreenOrientation.AutoRotation;
        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToLandscapeRight = true;
	}
}