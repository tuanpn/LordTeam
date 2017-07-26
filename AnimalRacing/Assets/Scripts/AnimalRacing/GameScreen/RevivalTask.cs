using UnityEngine;
using System.Collections;

namespace GamePlay
{
    public class RevivalTask : MonoBehaviour
    {
        public Sprite[] icons;
        public GameObject Icon;
        public GameObject Task;

        private bool isVisible;
        private float duration;
        private float stateTime;
        private float scaleX;

        private bool isRunning;

        public void setIcon(EmotionType emotion)
        {
            Icon.GetComponent<SpriteRenderer>().sprite = icons[(int)emotion];

            isRunning = true;

            /*
            switch (skillType)
            {
                case SkillType.BUON:
                    Icon.GetComponent<SpriteRenderer>().sprite = icons[0];
                    break;
                case SkillType.CUOI:
                    Icon.GetComponent<SpriteRenderer>().sprite = icons[1];
                    break;
                case SkillType.CUOIDEU:
                    Icon.GetComponent<SpriteRenderer>().sprite = icons[2];
                    break;
                case SkillType.TUC:
                    Icon.GetComponent<SpriteRenderer>().sprite = icons[3];
                    break;
                case SkillType.HOAMAT:
                    Icon.GetComponent<SpriteRenderer>().sprite = icons[4];
                    break;
            }
             * */
        }

        public void Update()
        {
            if (isVisible && isRunning)
            {
                stateTime -= Time.deltaTime;
                scaleX = stateTime / duration;
                if (stateTime <= 0)
                {
                    isVisible = false;
                    scaleX = 0;
                    gameObject.SetActive(false);
                }
                Task.transform.localScale = new Vector3(scaleX, 1, 1);
            }
        }

        public void show(float duration)
        {
            this.duration = duration;
            this.isVisible = true;
            this.stateTime = duration;
            this.scaleX = 1;
            gameObject.SetActive(true);
        }

        public void setRunning(bool isRunning)
        {
            this.isRunning = isRunning;
        }
    }
}
