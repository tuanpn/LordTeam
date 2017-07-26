using UnityEngine;
using System.Collections;

public class Cloud : MonoBehaviour {

    private Vector3 speed;

    public void Start()
    {
        speed = new Vector3(-1, 0, 0);
    }

	public void Update () {
        transform.localPosition += speed * Time.deltaTime;
        if (transform.localPosition.x <= -6)
            transform.localPosition = new Vector3(8, Random.Range(-2, 2), transform.localPosition.z);
	}
}
