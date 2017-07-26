using UnityEngine;
using System.Collections;

public class AnimalNameShop : MonoBehaviour {

    private BitmapFont nameFont;

    public void setFont(BitmapFont nameFont)
    {
        this.nameFont = new BitmapFont(nameFont, gameObject);
    }

    public void setName(string name)
    {
        this.nameFont.setText(name, 0, 0);
    }
}
