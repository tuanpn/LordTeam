using UnityEngine;
using System.Collections;

public class Gold : MonoBehaviour {

    private Vector3 speedLeft = new Vector3(-2, 0, 0);
    private bool isRunning;

    public void Start()
    {
        isRunning = true;
    }

    void Update()
    {
        if(isRunning)
            gameObject.transform.localPosition += speedLeft * Time.deltaTime;
    }

    public void setRunning(bool isRunning)
    {
        this.isRunning = isRunning;
    }
	
}
