using UnityEngine;
using System.Collections;

namespace GamePlay
{
    public class Combo : MonoBehaviour
    {
        private BitmapFont fontCombo;
        public GameObject label;
        public GameObject task;
        public GameScreen gameScreen;

        private float stateTime;
        private bool isVisibled;

        private bool isRunning;

        private float totalTime;

        private int numberCombo;

        private ComboType comboType;

        private string[] texts;
        private int comboIndex;

        private bool isUpdateTask;

        private Vector3 pl;

        public void Start()
        {
            isRunning = true;
            totalTime = 5;
            texts = new string[] { "a", "s", "d", "f", "g", "h", "j", "k", "" };
            this.comboIndex = -1;
            this.comboType = ComboType.NONE;
            this.pl = label.transform.localPosition;
            gameObject.SetActive(false);
        }

        public void setFont(BitmapFont comboFont)
        {
            fontCombo = new BitmapFont(comboFont, label);
        }
         
        public void setCombo(int comboIndex)
        {
            if (comboIndex == -1)
            {
                SetVisible(false);
                numberCombo = 0;
                return;
            }
            if (this.comboIndex != comboIndex)
            {
                this.comboIndex = comboIndex;
                numberCombo = 1;
                SetVisible(false);
            }
            else
            {
                numberCombo++;
                if (numberCombo >= 2)
                {
                    SetVisible(true);
                    isUpdateTask = true;
                    stateTime = totalTime;
                }
            }
        }

        public int getNumberCombo()
        {
            return this.numberCombo;
        }

        private void SetVisible(bool isVisibled)
        {
            this.isVisibled = isVisibled;
            if (isVisibled)
            {
                gameObject.SetActive(true);
                stateTime = totalTime;
                task.transform.localScale = new Vector3(1, 1, task.transform.localScale.z);
                fontCombo.setText(texts[comboIndex] + "x" + numberCombo + " COMBO", -2, 18, "GUI", "GUI");
                /*
                label.GetComponent<Actor>().addAction(new ActionRepeat(4, new ActionSequence(
                    new ActionMoveTo(pl.x, pl.y + 0.1f, 0.2f, Interpolation.sine), 
                    new ActionMoveTo(pl.x, pl.y, 0.2f, Interpolation.sine))));
                 * */
            }
            else
            {
                if (label.GetComponent<Actor>() != null)
                {
                    Actor[] acts = label.GetComponents<Actor>();
                    for (int i = 0; i < acts.Length; i++)
                        Destroy(acts[i]);
                }
                //numberCombo = 0;
                gameObject.SetActive(false);
            }
        }

        public void Update()
        {
            if (isRunning)
            {
                if (isUpdateTask)
                {
                    stateTime -= Time.deltaTime;
                   
                    if (stateTime <= 0)
                    {
                        stateTime = 0;
                        SetVisible(false);
                        comboIndex = -1;
                        comboType = ComboType.NONE;
                        numberCombo = 0;
                        isUpdateTask = false;
                    }
                    task.transform.localScale = new Vector3(stateTime / totalTime, 1, task.transform.localScale.z);
                }
            }
        }
    }
}