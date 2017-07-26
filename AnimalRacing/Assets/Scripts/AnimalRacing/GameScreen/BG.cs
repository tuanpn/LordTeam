using UnityEngine;
using System.Collections;

namespace GamePlay
{

    public class BG : MonoBehaviour
    {

        public GameObject bg1;
        public GameObject bg2;

        public float speedX;

        private Vector3 speed;
        private Vector3 dis;

        private bool isRunning;

        public void Start()
        {
            speed = new Vector3(speedX, 0, 0);
            isRunning = true;
        }

        public void setSprite(Sprite sprite, float heightSprite)
        {
            bg1.GetComponent<SpriteRenderer>().sprite = sprite;
            bg2.GetComponent<SpriteRenderer>().sprite = sprite;
            bg1.transform.localPosition = new Vector3(bg1.transform.localPosition.x, heightSprite / 100.0f, bg1.transform.localPosition.z);
            bg2.transform.localPosition = new Vector3(bg2.transform.localPosition.x, heightSprite / 100.0f, bg2.transform.localPosition.z);
        }

        public void Update()
        {
            if (isRunning)
            {
                dis = speed * Time.deltaTime;
                bg1.transform.localPosition -= dis;
                bg2.transform.localPosition -= dis;

                if (bg2.transform.localPosition.x <= -8)
                    bg2.transform.localPosition = new Vector3(bg1.transform.localPosition.x + 8, bg2.transform.localPosition.y, bg2.transform.localPosition.z);
                if (bg1.transform.localPosition.x <= -8)
                    bg1.transform.localPosition = new Vector3(bg2.transform.localPosition.x + 8, bg1.transform.localPosition.y, bg1.transform.localPosition.z);
            }
        }

        public void setRunning(bool isRunning)
        {
            this.isRunning = isRunning;
        }
    }
}
