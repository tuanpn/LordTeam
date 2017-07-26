using UnityEngine;
using System.Collections;

public class Descript : MonoBehaviour {

    private BitmapFont desFont;
    private string des;

    public void setDesFont(BitmapFont desFont)
    {
        this.desFont = new BitmapFont(desFont, gameObject);
    }

    public void setDesFont(BitmapFont desFont, Color color)
    {
        this.desFont = new BitmapFont(desFont, gameObject);
        this.desFont.setColor(color);
    }

    public void setText(string des)
    {
        this.des = des;
        this.desFont.setText(des, 0, 15);
    }
}
