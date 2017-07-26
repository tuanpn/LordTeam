using UnityEngine;
using System.Collections;

namespace GamePlay
{
    public class ButtonSkills : MonoBehaviour
    {
        public Sprite[] skillSprites;
        public Sprite[] skillSpriteLocks;

        public GameObject[] buttonSkills;
        public GameObject[] boardObjects;
        public Sprite boardSprite;

        private BitmapFont[] fontSkills;
        private GameObject[] labels;
        private int[] numberSkills = new int[]{0, 0, 0};

        public GameScreen gameScreen;

        private int skillNumber = 0;

        public void Start()
        {
            float[] btX = new float[] {1.75f, 2.65f, 3.55f};
            int[] skills = Attr.currentSkills;
            //int skillNumber = 0;
            labels = new GameObject[3];
            for (int i = 0; i < 3; i++)
            {
                if (skills[i] >= 0)
                {
                    buttonSkills[skillNumber].GetComponent<SpriteRenderer>().sprite = skillSprites[skills[i]];
                    buttonSkills[skillNumber].AddComponent<SkillRandom>().skillType = SkillRandom.getSkillType(skills[i]);
                    addClickListener(buttonSkills[skillNumber], skillSprites[skills[i]], skillSpriteLocks[skills[i]]);
                    boardObjects[skillNumber].GetComponent<SpriteRenderer>().sprite = boardSprite;
                    labels[skillNumber] = new GameObject("SkillNumber");
                    labels[skillNumber].transform.parent = gameObject.transform;
                    skillNumber++;
                }
            }

            for (int i = skillNumber - 1; i >= 0; i--)
            {
                Vector3 p = buttonSkills[i].gameObject.transform.localPosition;
                buttonSkills[i].gameObject.transform.localPosition = new Vector3(btX[2 - i], p.y, p.z);
                boardObjects[i].transform.localPosition = new Vector3(btX[2 - i] + 0.2f, p.y - 0.25f, -1);

                labels[i].transform.localPosition = new Vector3(btX[2-i] + 0.1f , p.y - 0.35f, -1);
            }
            fontSkills = new BitmapFont[3];
        }

        private void addClickListener(GameObject buttonSkill, Sprite enableSprite, Sprite disableSprite)
        {
            buttonSkill.AddComponent<InputProcessor>();
            SkillClickListener skillClickListener = buttonSkill.AddComponent<SkillClickListener>();
            skillClickListener.setDrawables(enableSprite, disableSprite);
            skillClickListener.setEnabled(true);

            
        }

        public void setFonts(BitmapFont shopFont)
        {
            for (int i = 0; i < skillNumber; i++)
            {
                //if (Attr.currentSkills[i] >= 0)
                if (buttonSkills[i].GetComponent<SkillClickListener>() != null)
                {
                    fontSkills[i] = new BitmapFont(shopFont, labels[i]);
                    fontSkills[i].setText("" + numberSkills[i], 0, 0, "GUI", "GUI");
                    buttonSkills[i].GetComponent<SkillClickListener>().setEnabled(numberSkills[i] > 0);
                }
                //else
                //{
                //    fontSkills[i] = null;
                //    if(Attr.currentSkills[i] >= 0)
                 //       buttonSkills[i].SetActive(false);
                //}
            }
        }

        public void eateSkill(SkillType type)
        {
            for (int i = 0; i < buttonSkills.Length; i++)
            {
                if (buttonSkills[i].GetComponent<SkillRandom>().skillType == type)
                {
                    numberSkills[i]++;
                    fontSkills[i].setText("" + numberSkills[i], 0, 0, "GUI", "GUI");
                    buttonSkills[i].GetComponent<SkillClickListener>().setEnabled(numberSkills[i] > 0);
                    break;
                }
            }
        }

        public void shot(SkillType type)
        {
            for (int i = 0; i < buttonSkills.Length; i++)
            {
                if (buttonSkills[i].GetComponent<SkillRandom>().skillType == type)
                {
                    numberSkills[i]--;
                    fontSkills[i].setText("" + numberSkills[i], 0, 0, "GUI", "GUI");
                    buttonSkills[i].GetComponent<SkillClickListener>().setEnabled(numberSkills[i] > 0);
                    break;
                }
            }
        }
    }
}
