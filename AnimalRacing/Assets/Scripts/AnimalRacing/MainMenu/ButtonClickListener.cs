using UnityEngine;
using System.Collections;
using Settings;

namespace MainMenu
{
    public class ButtonClickListener : InputAdapter
    {
        public int buttonIndex;

        public SettingLayer settingLayer;

        public GameObject achievementLayer;

        public override void OnTouchDown()
        {
            if (InputController.Name == InputNames.MAINMENU)
            {
                transform.localScale = new Vector3(0.9f, 0.9f, transform.localPosition.z);
                gameObject.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
            }
        }

        public override void OnCheckUp()
        {
            if (InputController.Name == InputNames.MAINMENU)
            {
                transform.localScale = new Vector3(1, 1, transform.localPosition.z);
                gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            }
        }

        public override void OnTouchUp()
        {
            if (InputController.Name == InputNames.MAINMENU)
            {
                SoundManager.playButtonSound();
                switch (buttonIndex)
                {
                    case 0://more game                       
                      
                        Application.OpenURL("http://google.com"); // LINK MORE GAME
                        break;
                    case 1://setting
                        //GoogleMobileAdControll.AdmobControll.ShowInterstitial();
                        settingLayer.setVisibled(true, true);
                        //InputController.Name = InputNames.SETTING;
                        InputController.Name = InputNames.DIALOG;
                        break;
                }
            }
        }
    }
}