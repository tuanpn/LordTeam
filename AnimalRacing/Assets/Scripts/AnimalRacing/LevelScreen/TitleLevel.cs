using UnityEngine;
using System.Collections;

public class TitleLevel : MonoBehaviour {

    public Sprite[] sprites;

    public void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = sprites[Attr.currentWorld];
    }
	
}
