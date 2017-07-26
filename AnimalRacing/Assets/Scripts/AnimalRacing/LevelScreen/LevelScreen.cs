using UnityEngine;
using System.Collections;

public class LevelScreen : MonoBehaviour {

    public GameObject bgObject;

	public void Start () {
        ARController.setBannerVisible(true);
        bgObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/bg" + (Attr.currentWorld + 1));
	}
}
