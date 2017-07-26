using UnityEngine;
using System.Collections;

public class Descriptions : MonoBehaviour {

    public Descript descript1;
    public Descript descript2;
    public Descript nameDes;
    

	public void Start () {
        BitmapFont desFont = new BitmapFont("Fonts/des_font", "Fonts/des_font_xml", gameObject);
        descript1.setDesFont(desFont, Color.yellow);
        descript2.setDesFont(desFont, Color.yellow);
        descript1.setText("Pick your skill");

        BitmapFont shopFont = new BitmapFont("Fonts/shop_font", "Fonts/shop_font_xml", gameObject);
        nameDes.setDesFont(shopFont);
	}

    public void setText(string text1, string text2, string name, int starUnlock)
    {
        if (starUnlock == -1)
        {
            descript1.setText(text1);
            descript2.setText(text2);
            nameDes.setText(name);
        }
        else {
            descript1.setText("");
            descript2.setText("");
            nameDes.setText("Need " + starUnlock + " *");
        }
    }
}
