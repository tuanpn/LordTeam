using UnityEngine;
using System.Collections;

namespace GamePlay
{
    public class ResultLayer : MonoBehaviour
    {
        public GameObject[] stars;
        public GameObject[] buttons;
        public Sprite starSprite;

        private int score;
        private int star;

        private bool isShown;

        public GameObject[] labels;
        public GameObject scoreLabel;

        private int m_gold;

        void Start()
        {
//            ARController.showInterstitialAd();
			
            //GoogleMobileAdControll.AdmobControll.ShowInterstitial();
        }

        void Update()
        {
            if (isShown)
            {
                createGUI();
                if (star > 0)
                {
                    saveData();
                }
                isShown = false;
            }

            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Backspace))
            {
                SoundManager.LoadBgMusic("Sounds/menu", true);
                Application.LoadLevel("MapScreen");
            }
        }

        private void saveData()
        {
            int starSaved = Data.getData(Data.KEY_STAR + (Attr.currentWorld * 15 + Attr.currentLevel));
            if (star > starSaved)
            {
                Data.saveData(Data.KEY_STAR + (Attr.currentWorld * 15 + Attr.currentLevel), star);
                Data.saveData(Data.KEY_COIN, Data.getData(Data.KEY_COIN) + m_gold);

                if (Attr.currentLevel == 14)
                {
                    int worldSaved = Data.getData(Data.KEY_WORLD_MAP);
                    if (Attr.currentWorld == worldSaved - 1)
                    {
                        Data.saveData(Data.KEY_WORLD_MAP, worldSaved + 1);
                    }
                }
                else
                {
                    int levelSaved = Data.getData(Data.KEY_LEVEL + Attr.currentWorld);
                    if (Attr.currentLevel == levelSaved - 1)
                    {
                        Data.saveData(Data.KEY_LEVEL + Attr.currentWorld, levelSaved + 1);
                    }
                }
            }
            else
            {
                Data.saveData(Data.KEY_COIN, Data.getData(Data.KEY_COIN) + m_gold);
            }
        }

        private void createGUI()
        {
            for (int i = 0; i < 3; i++)
            {
                if (i < star)
                {
                    //  stars[i].GetComponent<SpriteRenderer>().sprite = starSprite;
                    int temp = i;
                    stars[temp].GetComponent<Actor>().addAction(new ActionSequence(
                        new ActionDelay(0.4f + 0.6f * temp),
                        new ActionRunnable(delegate()
                    {
                        stars[temp].GetComponent<SpriteRenderer>().sprite = starSprite;

                        createStars(stars[temp].transform.localPosition, temp);
                        if (temp == star - 1)
                        {
                            showButtons(true);
                        }
                    })
                        ));
                }
                else
                {
                    showButtons(false);
                }
            }
        }

        private void showButtons(bool showNext)
        {
            if (showNext)
            {
                for (int i = 0; i < 3; i++)
                {
                    buttons[i].SetActive(true);
                    buttons[i].AddComponent<Actor>().addAction(new ActionScaleTo(1, 1, 0.5f, Interpolation.swingOut));
                }
            }
            else
            {
                for (int i = 0; i < 2; i++)
                {
                    buttons[i].SetActive(true);
                    buttons[i].AddComponent<Actor>().addAction(new ActionScaleTo(1, 1, 0.5f, Interpolation.swingOut));
                }
                buttons[2].SetActive(false);
            }
        }

        private void createStars(Vector3 position, int starIndex)
        {
            for (int i = 0; i < 20; i++)
            {
                GameObject starObject = new GameObject("StarFly");
                starObject.layer = LayerMask.NameToLayer("GUI");
                starObject.transform.parent = gameObject.transform;
                starObject.transform.localPosition = new Vector3(position.x, position.y, -3);
                starObject.AddComponent<SpriteRenderer>().sprite = starSprite;
                starObject.GetComponent<SpriteRenderer>().sortingLayerName = "GUI";
                starObject.transform.localScale = new Vector3(0.5f, 0.5f, starObject.transform.localScale.z);

                starObject.AddComponent<Actor>().addAction(new ActionParallel(
                    new ActionRotateBy(720, 2),
                    new ActionScaleTo(0, 0, 1.5f)
                    ));

                if (starIndex == 0)
                {
                    starObject.AddComponent<Bezier>().setBezier(-1,
                        new Vector2(position.x, position.y),
                        new Vector2(position.x, Random.Range(1f, 2f)),
                        new Vector2(position.x - Random.Range(2f, 3f), Random.Range(1f, 2f)),
                        new Vector2(position.x - Random.Range(2f, 5.5f), Random.Range(-2f, -1f))
                        );
                }
                else if (starIndex == 1)
                {
                    starObject.AddComponent<Bezier>().setBezier(-1,
                        new Vector2(position.x, position.y),
                        new Vector2(position.x, Random.Range(1f, 2f)),
                        new Vector2(Random.Range(-1f, 1f), Random.Range(1f, 2f)),
                        new Vector2(Random.Range(-2f, 2f), Random.Range(-4f, -2f))
                        );
                }
                else if (starIndex == 2)
                {
                    starObject.AddComponent<Bezier>().setBezier(-1,
                        new Vector2(position.x, position.y),
                        new Vector2(position.x, Random.Range(1f, 2f)),
                        new Vector2(position.x + Random.Range(2f, 3f), Random.Range(1f, 2f)),
                        new Vector2(position.x + Random.Range(2f, 5.5f), Random.Range(-4f, -2f))
                        );
                }
                Destroy(starObject, 2);
            }
        }


        public void setParams(int score, int place, int gold, float time)
        {
            this.score = score;
            this.m_gold = gold + getBonus(place);
            isShown = true;
            if (place == 1) star = 3;
            else if (place > 1 && place <= 3) star = 2;
            else if (place > 3 && place <= 5) star = 1;
            else star = 0;

            BitmapFont scoreFont = new BitmapFont("Fonts/font_result", "Fonts/font_result_xml", scoreLabel);
            scoreFont.setText("" + score, 0, 0, "GUI", "GUI");
            scoreLabel.transform.localPosition = new Vector3(scoreLabel.transform.localPosition.x - scoreFont.width/2,
                scoreLabel.transform.localPosition.y, scoreLabel.transform.localPosition.z);

            BitmapFont desFont = new BitmapFont("Fonts/shop_font", "Fonts/shop_font_xml", gameObject);

            BitmapFont bonusFont = new BitmapFont(desFont, labels[0]);
            bonusFont.setText("BONUS: " + getBonus(place), 0, 15, "GUI", "GUI");

            BitmapFont timeFont = new BitmapFont(desFont, labels[1]);
            timeFont.setText("TIME: " + ((int)(time * 100))/100f, 0, 15, "GUI","GUI");

            BitmapFont placeFont = new BitmapFont(desFont, labels[2]);
            placeFont.setText("PLACE: " + place, 0,  15, "GUI", "GUI");

            BitmapFont goldFont = new BitmapFont(desFont, labels[3]);
            goldFont.setText("GOLD: " + gold, 0, 15, "GUI", "GUI");
        }

        private int getBonus(int place)
        {
            if (place == 1) return 200;
            else if (place > 1 && place <= 3) return 100;
            else if (place > 3 && place <= 5) return 50;
            else return 0;
        }

        public int getStar()
        {
            return star;
        }
    }
}