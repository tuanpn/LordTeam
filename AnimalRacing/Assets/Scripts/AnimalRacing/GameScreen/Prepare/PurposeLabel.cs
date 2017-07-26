using UnityEngine;
using System.Collections;

namespace GamePlay
{
    public class PurposeLabel : MonoBehaviour
    {
        public GameObject purposeLabel1;
        public GameObject purposeLabel2;
        public GameObject purposeLabel3;

        public Sprite starSprite;
        public GameObject[] stars;

        // Use this for initialization
        void Start()
        {
            BitmapFont purposeFont1 = new BitmapFont("Fonts/shop_font", "Fonts/shop_font_xml", purposeLabel1);
            BitmapFont purposeFont2 = new BitmapFont(purposeFont1, purposeLabel2);
            BitmapFont purposeFont3 = new BitmapFont(purposeFont1, purposeLabel3);

            purposeFont1.setText("5th place or better : 50 #", 0, 12, "GUI", "GUI");
            purposeFont2.setText("3rd place or better : 100 #", 0, 12, "GUI", "GUI");
            purposeFont3.setText("1st place : 200 #", 0, 12, "GUI", "GUI");

            purposeLabel1.transform.localScale = new Vector3(0.8f, 0.8f, purposeLabel1.transform.localScale.z);
            purposeLabel2.transform.localScale = new Vector3(0.8f, 0.8f, purposeLabel2.transform.localScale.z);
            purposeLabel3.transform.localScale = new Vector3(0.8f, 0.8f, purposeLabel3.transform.localScale.z);

            int star = Data.getData(Data.KEY_STAR + (Attr.currentWorld * 15 + Attr.currentLevel));
            for (int i = 0; i < 3; i++)
            {
                if (i < star)
                {
                    stars[i].GetComponent<SpriteRenderer>().sprite = starSprite;
                }
            }

        }

        void Update()
        {

        }
    }
}
