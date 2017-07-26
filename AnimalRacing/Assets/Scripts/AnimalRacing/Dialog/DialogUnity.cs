using UnityEngine;
using System.Collections;

namespace Dialog
{
    public class DialogUnity : MonoBehaviour
    {
        public Buttons buttons;
        private BitmapFont bitmapFont;
        public GameObject label1;
        public GameObject label2;

        public void setDialogOne(DialogButton dialogOK)
        {
            buttons.setButtonNumber(1);
            buttons.buttonX.GetComponent<Button>().addClickListener(dialogOK);
        }

        public void setDialogTwo(DialogButton dialogOK, DialogButton dialogClose)
        {
            buttons.setButtonNumber(2);
            buttons.buttonOk.GetComponent<Button>().addClickListener(dialogOK);
            buttons.buttonClose.GetComponent<Button>().addClickListener(dialogClose);
        }

        public void setText(string oneline)
        {
            bitmapFont = new BitmapFont("Fonts/shop_font", "Fonts/shop_font_xml", label1);
            bitmapFont.setText(oneline, 0, 15);
            Transform[] fontTransforms = label1.GetComponentsInChildren<Transform>(true);
            for (int i = 0; i < fontTransforms.Length; i++)
            {
                if (fontTransforms[i].gameObject.GetComponent<SpriteRenderer>() != null)
                {
                    fontTransforms[i].gameObject.layer = LayerMask.NameToLayer("GUI");
                    fontTransforms[i].gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "GUI";
                }
            }
        }

        public void setText(string line1, string line2)
        {
            bitmapFont = new BitmapFont("Fonts/shop_font", "Fonts/shop_font_xml", label1);
            bitmapFont.setText(line1, 0, 15);
            Transform[] fontTransforms = label1.GetComponentsInChildren<Transform>(true);
            for (int i = 0; i < fontTransforms.Length; i++)
            {

                if (fontTransforms[i].gameObject.GetComponent<SpriteRenderer>() != null)
                {
                    fontTransforms[i].gameObject.layer = LayerMask.NameToLayer("GUI");
                    fontTransforms[i].gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "GUI";
                }
            }

            BitmapFont bitmapFont2 = new BitmapFont(bitmapFont, label2);
            bitmapFont2.setText(line2, 0, 15);
            Transform[] fontTransforms2 = label2.GetComponentsInChildren<Transform>(true);
            for (int i = 0; i < fontTransforms2.Length; i++)
            {
                if (fontTransforms2[i].gameObject.GetComponent<SpriteRenderer>() != null)
                {
                    fontTransforms2[i].gameObject.layer = LayerMask.NameToLayer("GUI");
                    fontTransforms2[i].gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "GUI";
                }
            }
        }

        public void Start()
        {
            gameObject.SetActive(false);
        }

        public void showDialog()
        {
            gameObject.SetActive(true);
            gameObject.transform.localPosition = new Vector3(0, 4.8f, gameObject.transform.localPosition.z);
            if (gameObject.GetComponent<Actor>() != null)
                Destroy(gameObject.GetComponent<Actor>());
            gameObject.AddComponent<Actor>().addAction(new ActionMoveTo(0, 0, 0.5f, Interpolation.swingOut));
        }

        public void hideDialog()
        {
            gameObject.SetActive(false);
            gameObject.transform.localPosition = new Vector3(0, 4.8f, gameObject.transform.localPosition.z);
        }
    }

    public delegate void DialogButton();
}