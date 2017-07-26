using UnityEngine;
using System.Collections;

namespace GamePlay
{
    public class PauseButtonClickListener : InputAdapter
    {
        public GameScreen gameScreen;

        public override void OnTouchDown()
        {
            if (InputController.Name != InputNames.GAMESCREEN) return;
            base.OnTouchDown();
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
            SoundManager.playButtonSound();
        }

        public override void OnCheckUp()
        {
            if (InputController.Name != InputNames.GAMESCREEN) return;
            base.OnCheckUp();
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }

        public override void OnTouchUp()
        {
            if (InputController.Name != InputNames.GAMESCREEN) return;
            base.OnTouchUp();
            gameScreen.pauseGame();
        } 

        void Start()
        {

        }

        void Update()
        {

        }
    }
}