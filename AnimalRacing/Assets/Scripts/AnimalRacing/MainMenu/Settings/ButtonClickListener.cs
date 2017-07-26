using UnityEngine;
using System.Collections;

namespace Settings
{
    public class ButtonClickListener : InputAdapter
    {
        public int buttonIndex;
        public SettingLayer settingLayer;
        
        //info
        public GameObject infoLayer;
        public GameObject setLayer;

        //help
        public GameObject helpLayer;

        public Sprite[] musicSprites;
        public Sprite[] soundSprites;

        public void Start()
        {
            if (buttonIndex == 0)//music
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = SoundManager.isMusic ? musicSprites[0] : musicSprites[1];
            }
            else if (buttonIndex == 1)//Sound
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = SoundManager.isSound ? soundSprites[0] : soundSprites[1];
            }
        }

        public override void OnTouchDown()
        {
            if (InputController.Name == InputNames.DIALOG)
            {
                base.OnTouchDown();
                transform.localScale = new Vector3(0.9f, 0.9f, transform.localScale.z);
                gameObject.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
            }
        }

        public override void OnCheckUp()
        {
            if (InputController.Name == InputNames.DIALOG)
            {
                base.OnCheckUp();
                transform.localScale = new Vector3(1, 1, transform.localScale.z);
                gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            }
        }

        public override void OnTouchUp()
        {
            if (InputController.Name == InputNames.DIALOG)
            {
                base.OnTouchUp();
                SoundManager.playButtonSound();
                switch (buttonIndex)
                {
                    case 0://music
                        SoundManager.isMusic = !SoundManager.isMusic;
                        gameObject.GetComponent<SpriteRenderer>().sprite = SoundManager.isMusic ? musicSprites[0] : musicSprites[1];
                        if (SoundManager.isMusic)
                        {
                            SoundManager.LoadBgMusic("Sounds/menu", false);
                        }
                        else
                        {
                            SoundManager.stopMusic();
                        }
                        break;
                    case 1://sound
                        SoundManager.isSound = !SoundManager.isSound;
                        gameObject.GetComponent<SpriteRenderer>().sprite = SoundManager.isSound ? soundSprites[0] : soundSprites[1];
                        break;
                    case 2://help
                        settingLayer.setVisibled(false, true);
                        InputController.Name = InputNames.DIALOG;
                        helpLayer.SetActive(true);
                        break;
                    case 3://info
                        setLayer.SetActive(false);
                        infoLayer.SetActive(true);
                        break;
                    case 4://close
                        if (setLayer.activeSelf)
                        {
                            settingLayer.setVisibled(false, false);
                            InputController.Name = InputNames.MAINMENU;
                        }
                        else
                        {
                            setLayer.SetActive(true);
                            infoLayer.SetActive(false);
                        }
                        break;
                }
            }
        }
    }
}
