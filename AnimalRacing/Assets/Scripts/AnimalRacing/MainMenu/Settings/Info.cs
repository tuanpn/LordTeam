using UnityEngine;
using System.Collections;

public class Info : MonoBehaviour {

    private Rect rect;
    private Texture2D texture;

    private float dis;

    private bool isVisible;

	void Start () {
        texture = gameObject.GetComponent<SpriteRenderer>().sprite.texture;
        rect = new Rect(0, 642, 300, 325);
        updateRect();
	}
	
	void Update () {
        resetSprite();

        if (!isVisible)
        {
            isVisible = true;
            gameObject.SetActive(true);
        }
	}

    private void resetSprite()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = Sprite.Create(texture, rect, Vector2.zero);
        dis = 100 * Time.deltaTime;
        rect.y -= dis;

        updateRect();
    }

    private void updateRect()
    {
        if (rect.y <= -325)
            rect.y = 642;
        if (rect.y > 316)
            rect.height = 642 - rect.y;
        else
            rect.height = 325;
    }

}
