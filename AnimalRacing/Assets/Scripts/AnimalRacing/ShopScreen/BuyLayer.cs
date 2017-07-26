using UnityEngine;
using System.Collections;

public class BuyLayer : MonoBehaviour {

    public Label boosterLabel;
    public Label springsLabel;

    public Label starLabel;
    public Label goldLabel;

    private BitmapFont desFont;

    public void Start()
    {
    }

    public void setFont(BitmapFont fontShop)
    {
        starLabel.setFont(fontShop);
        goldLabel.setFont(fontShop);
        desFont = new BitmapFont("Fonts/bitmapfont1", "Fonts/bitmapfont1_xml", null);
        boosterLabel.setFont(desFont);
        springsLabel.setFont(desFont);
    }

    public void setParams(int booster, int springs, int starUnlock, int cost)
    {
        starLabel.setText(starUnlock + "", 0, 0);
        goldLabel.setText(cost + "", 0, 0);

        boosterLabel.gameObject.transform.localScale = new Vector3(1, 1, boosterLabel.gameObject.transform.localScale.z);
        springsLabel.gameObject.transform.localScale = new Vector3(1, 1, springsLabel.gameObject.transform.localScale.z);

        boosterLabel.setText("SPEED : " + booster, 0, 15);
        springsLabel.setText("JUMP : " + springs, 0, 15);

        boosterLabel.gameObject.transform.localScale = new Vector3(0.7f, 0.7f, boosterLabel.gameObject.transform.localScale.z);
        springsLabel.gameObject.transform.localScale = new Vector3(0.7f, 0.7f, springsLabel.gameObject.transform.localScale.z);
    }
}
