using UnityEngine;
using System.Collections;

namespace GamePlay
{

    public class PauseLayerClickListener : InputAdapter
    {

        public int buttonIndex;

        public GameScreen gameScreen;

        public PauseLayer pauseLayer;

        public override void OnTouchDown()
        {
            if (InputController.Name != InputNames.DIALOG) return;
            base.OnTouchDown();
            gameObject.transform.localScale = new Vector3(0.9f, 0.9f, gameObject.transform.localScale.z);
            SoundManager.playButtonSound();
        }
        public override void OnCheckUp()
        {
            if (InputController.Name != InputNames.DIALOG) return;
            base.OnCheckUp();
            gameObject.transform.localScale = new Vector3(1, 1, gameObject.transform.localScale.z);
        }
        public override void OnTouchUp()
        {
            if (InputController.Name != InputNames.DIALOG) return;
            base.OnTouchUp();
            switch (buttonIndex)
            {
                case 0://music
                    SoundManager.isMusic = !SoundManager.isMusic;
                    pauseLayer.changeSprite(0, SoundManager.isMusic);
                    break;
                case 1://sound
                    SoundManager.isSound = !SoundManager.isSound;
                    pauseLayer.changeSprite(1, SoundManager.isSound);
                    break;
                case 2://help

                    pauseLayer.gameObject.SetActive(false);
                    gameScreen.guideGame();

                    break;
                case 3://resume
                    gameScreen.resumeGame();
                    break;
                case 4://restart
                    Application.LoadLevel("GameScreen");
                    break;
                case 5://menu
                    Application.LoadLevel("MapScreen");
                    SoundManager.LoadBgMusic("Sounds/menu", true);
                    break;
            }
        }

    }
}
