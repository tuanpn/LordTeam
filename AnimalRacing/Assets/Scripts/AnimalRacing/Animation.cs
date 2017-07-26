using UnityEngine;
using System.Collections;

namespace Bapk
{

    public class Animation : MonoBehaviour
    {
        public Sprite[] sprites;
        public float durationOneFrame;
        public bool repeat;
        public bool destroyWhenFinish;

        private int frame;
        private float stateTime;
        private int currentIndex;
        private bool isFinish;

        private bool isRunning;

        public void Start()
        {
            currentIndex = 0;
            gameObject.GetComponent<SpriteRenderer>().sprite = sprites[currentIndex];
            frame = sprites.Length;
            isRunning = true;
        }

        public void Update()
        {
            if (isRunning)
            {
                UpdateAnimation();
            }
        }

        private void UpdateAnimation()
        {
            stateTime += Time.deltaTime;
            if (stateTime >= durationOneFrame)
            {
                if (isFinish)
                {
                    if (destroyWhenFinish)
                    {
                        Destroy(gameObject);
                        return;
                    }
                }
                currentIndex++;
                if (currentIndex == frame - 1)
                {
                    if (!repeat)
                        isFinish = true;
                }
                if (currentIndex == frame)
                    currentIndex = 0;
                stateTime = 0;
                gameObject.GetComponent<SpriteRenderer>().sprite = sprites[currentIndex];
            }
        }

        public void setRunning(bool isRunning)
        {
            this.isRunning = isRunning;
        }
    }
}