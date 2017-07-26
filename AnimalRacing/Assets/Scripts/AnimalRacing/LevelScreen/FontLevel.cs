using UnityEngine;
using System.Collections;

public class FontLevel : MonoBehaviour {
    private BoardLevel boardLevel;

    private BitmapFont levelFont;

    public Sprite starSprite;
    public GameObject bgObject;
    public ThreeStar threeStars;


    public void setStar(int star, bool isActive)
    {
        threeStars.setSprite(star, starSprite, isActive);
    }

    public void setBoardLevel(BoardLevel boardLevel)
    {
        this.boardLevel = boardLevel;
        levelFont = new BitmapFont(boardLevel.getFont(), gameObject);
    }

    public void setText(string text)
    {
        levelFont.setText(text, 0, 0);
    }
}
