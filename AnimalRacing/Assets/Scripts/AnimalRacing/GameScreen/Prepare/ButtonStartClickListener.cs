using UnityEngine;
using System.Collections;

namespace GamePlay
{
    public class ButtonStartClickListener : InputAdapter
    {
        public GameScreen gameScreen;

        public void Start()
        {
            gameObject.AddComponent<Actor>().addAction(new ActionRepeat(ActionRepeat.FOREVER, new ActionSequence(
                new ActionScaleTo(0.75f, 0.75f, 0.2f, Interpolation.sine),
                new ActionScaleTo(0.8f, 0.8f, 0.2f, Interpolation.sine)
                )));
        }

        public override void OnTouchDown()
        {
            if (InputController.Name != InputNames.DIALOG) return;
            base.OnTouchDown();
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
            SoundManager.playButtonSound();
        }
        public override void OnCheckUp()
        {
            if (InputController.Name != InputNames.DIALOG) return;
            base.OnCheckUp();
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }
        public override void OnTouchUp()
        {
            if (InputController.Name != InputNames.DIALOG) return;
            base.OnTouchUp();
            if (Data.getData(Data.KEY_GUIDE) == 0)
            {
                gameScreen.guideGame();
                Destroy(transform.parent.gameObject);
            }
            else
            {
                InputController.Name = "";
                //InputController.Name = InputNames.GAMESCREEN;
                //gameScreen.resumeGame();
                gameScreen.showCounter();
                Destroy(transform.parent.gameObject);
            }
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Backspace))
            {
                SoundManager.LoadBgMusic("Sounds/menu", true);
                Application.LoadLevel("MapScreen");
            }
        }
    }
}