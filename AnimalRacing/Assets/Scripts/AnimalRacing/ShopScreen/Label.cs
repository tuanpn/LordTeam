using UnityEngine;
using System.Collections;

public class Label : MonoBehaviour {

    private BitmapFont shopFont;

    public void setFont(BitmapFont shopFont)
    {
        this.shopFont = new BitmapFont(shopFont, gameObject);
    }

    public void setText(string text, float kerning, float space)
    {
        shopFont.setText(text, kerning, space, "GUI", "GUI");
    }

}
