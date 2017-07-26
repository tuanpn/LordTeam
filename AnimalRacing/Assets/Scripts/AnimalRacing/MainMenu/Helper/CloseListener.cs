using UnityEngine;
using System.Collections;

namespace Helper
{

    public class CloseListener : InputAdapter
    {
        public GameObject blackScreen;

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Backspace) || Input.GetKeyDown(KeyCode.Escape))
            {
                if (InputController.Name == InputNames.DIALOG)
                {
                    blackScreen.SetActive(false);
                    gameObject.transform.parent.gameObject.SetActive(false);
                    InputController.Name = InputNames.MAINMENU;
                }
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
                SoundManager.playButtonSound();
                blackScreen.SetActive(false);
                gameObject.transform.parent.gameObject.SetActive(false);
                InputController.Name = InputNames.MAINMENU;
            }
        }
    }
}