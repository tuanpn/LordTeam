using UnityEngine;
using System.Collections;

namespace GamePlay
{
    public class CounterLayer : MonoBehaviour
    {

        private float stateTime;
        public Sprite[] numbers;
        public GameObject numberObject;
        private int number;
        private bool isFinish;
        public GameScreen gameScreen;

        public void Start()
        {
            number = 0;
            SoundManager.playSoundLong("Sounds/counting", 4f);
        }

        public void Update()
        {
            stateTime += Time.deltaTime;
            if (isFinish)
            {
                if (stateTime >= 0.1f)
                {
                    //play game
                    gameScreen.resumeGame();
                    InputController.Name = InputNames.GAMESCREEN;
                    Destroy(gameObject);
                    SoundManager.LoadBgMusic("Sounds/bg1", true);
                }
            }else if (stateTime >= 1)
            {
                stateTime = 0;
                numberObject.GetComponent<SpriteRenderer>().sprite = numbers[number];
                number++;
                if (number >= 3)
                    isFinish = true;
            }
        }
    }
}
