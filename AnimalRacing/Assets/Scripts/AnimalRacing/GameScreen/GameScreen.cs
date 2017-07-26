using UnityEngine;
using System.Collections.Generic;
using Dialog;

namespace GamePlay
{
    public class GameScreen : MonoBehaviour
    {
        private GameObject mapObject;
        public Animals animals;
        public GameObject goldLayer;
        public GameObject coinLayer;
        public GameObject boosterLayer;
        public GameObject springsLayer;
        public GameObject skillRandomLayer;
        public GameObject shadowLayer;
        public TaskBar taskbar;
        public ButtonSkills buttonSkills;

        public GameObject pauseLayer;

        public Background background;

        public GameObject bulletLayer;

        public GameObject effectLayer;

        public GameObject scoreLayer;

        private GameObject deadLine;

        public RevivalTask revivalTask;

        private List<Vector3> revivalPositions;

        public float m_time { get; set; }
        public int m_gold { get; set; }
        public int m_score { get; set; }

        private bool isRunning;
        private bool isPrepare;

        private GameObject guideLayer;
        public GameObject counterLayer;

        public GameObject resultLayer;

        private int[] skills;
        private bool useSkills;

        private BitmapFont shopFont;

        private int[] perSkills;

        public Combo combo1;
        public Combo combo2;

        public GameObject dialog;

        public GoldAddition goldAddition;

        public void Start()
        {
            InputController.Name = InputNames.DIALOG;

            string[] mapNames = new string[] {"jungle","southpole","desert","volcano"};
            int[] lvs = new int[] { 1, 2, 3, 2, 3, 4, 3, 4, 5, 1, 2, 4, 3, 4, 5 };

            Debug.Log("WorldIndex : " + Attr.currentWorld + ", LevelIndex : " + Attr.currentLevel);

            mapObject = (GameObject)Instantiate(Resources.Load("Maps/" + mapNames[Attr.currentWorld] + lvs[Attr.currentLevel]), new Vector3(-4, 2.5f, 0), Quaternion.identity);
            mapObject.transform.localScale = new Vector3(0.01f, 0.01f, mapObject.transform.localScale.z);

            revivalPositions = new List<Vector3>();

            getSkillChoosed();

            setupMap(mapObject);

            int mapLayer = LayerMask.NameToLayer("Map");
            mapObject.layer = mapLayer;

            Transform[] transforms = mapObject.GetComponentsInChildren<Transform>(true);
            for (int i = 0; i < transforms.Length; i++)
            {
                transforms[i].gameObject.layer = mapLayer;
            }

            shopFont = new BitmapFont("Fonts/shop_font", "Fonts/shop_font_xml", null);

            BitmapFont comboFont = new BitmapFont("Fonts/font_combo", "Fonts/font_combo_xml", null);
            combo1.setFont(comboFont);
            combo2.setFont(comboFont);

            createDialog();

            m_gold = 0;
            m_time = 0;
            m_score = 0;

            perSkills = new int[] { 30, 30, 35, 35, 40, 40, 45, 45, 50, 55, 60, 60, 65, 65, 70 };

            isRunning = true;
            //prepareGame();//khong goi o day vi co the chua chay het cac ham Start
            isPrepare = true;
        }

        private void getSkillChoosed()
        {
            List<int> sks = new List<int>();
            for (int i = 0; i < 3; i++)
            {
                if (Attr.currentSkills[i] != -1)
                {
                    sks.Add(Attr.currentSkills[i]);
                }
            }
            if (sks.Count > 0)//neu co chon skill
            {
                useSkills = true;
                skills = new int[sks.Count];
                for (int i = 0; i < sks.Count; i++)
                {
                    skills[i] = sks[i];
                }
            }
            else//neu khong chon skill thi lay tu cac skill unlock
            {
                int[] unlockStars = new int[] { 0, 0, 35, 135, 15, 55, 75, 95 };

                int totalStar = 0;
                for (int i = 0; i < 60; i++)
                    totalStar += Data.getData(Data.KEY_STAR + i);

                for (int i = 0; i < 8; i++)
                {
                    if (totalStar >= unlockStars[i])
                    {
                        sks.Add(i);
                    }
                }
                skills = new int[sks.Count];
                for (int i = 0; i < sks.Count; i++)
                {
                    skills[i] = sks[i];
                }
            }
        }

        private void setupMap(GameObject mObject)
        {
            GameObject markGounds = new GameObject("MarkGrounds");
            markGounds.transform.parent = mObject.transform;
            markGounds.transform.localScale = new Vector3(1, 1, 1);
            markGounds.transform.localPosition = new Vector3(0, 0, 0);
    
            for (int i = 0; i < mObject.transform.childCount; i++)
            {
                Transform childTransform = mObject.transform.GetChild(i);
                string childname = childTransform.gameObject.name;
                if (childname == MapObjectNames.MarkObject)
                {
                    createMarkObjects(childTransform);
                }
                else if (childname == MapObjectNames.BoostObject)
                {
                    createBoosterObjects(childTransform);
                }
                else if (childname == MapObjectNames.SpringsObject)
                {
                    createSpringsObjects(childTransform);
                }
                else if (childname == MapObjectNames.HoleObject)
                {
                    createHoles(childTransform);
                }
                else if (childname == MapObjectNames.Coin)
                {
                    createCoins(childTransform);
                }
                else if (childname == MapObjectNames.SkillObject)
                {
                    createSkillRandoms(childTransform);
                }
                else if (childname == MapObjectNames.RevivalObject)
                {
                    createRevivalObjects(childTransform);
                }
                else if (childname == MapObjectNames.GroundObject)
                {
                    createGrounds(childTransform, markGounds.transform);
                }
                else if (childname == MapObjectNames.FinishObject)
                {
                    createFinishObject(childTransform);
                }
            }

            createDeadLine();
        }

        private void createMarkObjects(Transform parentTransform)
        {
            for (int i = 0; i < parentTransform.childCount; i++)
            {
                GameObject markObject = parentTransform.GetChild(i).gameObject;
                markObject.name = MapObjectNames.MarkObject;
                if (markObject.GetComponent<BoxCollider2D>() != null)
                    Destroy(markObject.GetComponent<BoxCollider2D>());
                BoxCollider2D childCollider = markObject.AddComponent<BoxCollider2D>();
                childCollider.size = new Vector2(20, 1000);
                childCollider.isTrigger = true;
            }
        }

        private void createBoosterObjects(Transform parentTransform)
        {
            for (int i = 0; i < parentTransform.childCount; i++)
            {
                GameObject boosterObject = (GameObject)Instantiate(Resources.Load<GameObject>("Booster"));
                GameObject c = parentTransform.GetChild(i).gameObject;
                boosterObject.transform.position = c.transform.position;
                boosterObject.transform.localScale = new Vector3(1, 1, 0);
                boosterObject.transform.parent = boosterLayer.transform;
                BoxCollider2D childCollider = boosterObject.AddComponent<BoxCollider2D>();
                childCollider.size = new Vector2(0.9f, 0.2f);
                childCollider.isTrigger = true;
                boosterObject.transform.position -= new Vector3(-0.45f, 0.1f, 0);
                boosterObject.name = MapObjectNames.BoostObject;

                Destroy(c);
            }
        }

        private void createSpringsObjects(Transform parentTransform)
        {
            for (int i = 0; i < parentTransform.childCount; i++)
            {
                GameObject springsObject = (GameObject)Instantiate(Resources.Load<GameObject>("Springs"));
                GameObject c = parentTransform.GetChild(i).gameObject;
                springsObject.transform.position = c.transform.position;
                springsObject.transform.parent = springsLayer.transform;
                springsObject.transform.position -= new Vector3(-0.2f, 0.3f, 0);
                springsObject.name = MapObjectNames.SpringsObject;
                Destroy(c);
            }
        }

        private void createHoles(Transform parentTransform)
        {
            for (int i = 0; i < parentTransform.childCount; i++)
            {
                BoxCollider2D childCollider = parentTransform.GetChild(i).gameObject.GetComponent<BoxCollider2D>();
                childCollider.isTrigger = true;
                parentTransform.GetChild(i).gameObject.name = MapObjectNames.HoleObject;
            }
        }

        private void createCoins(Transform parentTransform)
        {
            for (int i = 0; i < parentTransform.childCount; i++)
            {
                GameObject coinObject = (GameObject)Instantiate(Resources.Load<GameObject>("Coin"));
                coinObject.name = MapObjectNames.Coin;
                GameObject c = parentTransform.GetChild(i).gameObject;
                coinObject.transform.position = c.transform.position;
                coinObject.transform.localScale = new Vector3(1, 1, 0);
                coinObject.transform.parent = coinLayer.transform;
                BoxCollider2D childCollider = coinObject.AddComponent<BoxCollider2D>();
                childCollider.size = new Vector2(0.3f, 0.3f);
                childCollider.isTrigger = true;

                Destroy(c);
            }
        }

        private void createSkillRandoms(Transform parentTransform)
        {
            for (int i = 0; i < parentTransform.childCount; i++)
            {
                GameObject skillObject = (GameObject)Instantiate(Resources.Load<GameObject>("SkillRandom"));
                skillObject.name = MapObjectNames.SkillObject;
                GameObject c = parentTransform.GetChild(i).gameObject;
                skillObject.transform.position = c.transform.position;
                skillObject.transform.parent = skillRandomLayer.transform;
                skillObject.transform.position -= new Vector3(-0.2f, 0.2f, 0);

                skillObject.AddComponent<SkillRandom>().skillType = SkillRandom.getSkillType(skills[Random.Range(0, skills.Length)]);
               // Debug.Log(skillObject.GetComponent<SkillRandom>().skillType.ToString());
                
                Destroy(c);
            }
        }

        private void createRevivalObjects(Transform parentTransform)
        {
            for (int i = 0; i < parentTransform.childCount; i++)
            {
                GameObject revivalObject = parentTransform.GetChild(i).gameObject;
                if (revivalObject.GetComponent<BoxCollider2D>() != null)
                    Destroy(revivalObject.GetComponent<BoxCollider2D>());
                BoxCollider2D childCollider = revivalObject.AddComponent<BoxCollider2D>();
                childCollider.size = new Vector2(20, 1000);
                childCollider.isTrigger = true;
                revivalObject.name = MapObjectNames.RevivalObject;
                revivalPositions.Add(revivalObject.transform.localPosition);
                revivalObject.AddComponent<RevivalObject>().revialIndex = i;
            }
        }

        public Vector3 getRevivalPosition(int revivalIndex)
        { 
            if(revivalIndex >= revivalPositions.Count)
                return revivalPositions[revivalPositions.Count - 1];
            return revivalPositions[revivalIndex];
        }

        private void createGrounds(Transform parentTransform, Transform markGounds)
        {
            for (int i = 0; i < parentTransform.childCount; i++)
            {
                GameObject groundObject = parentTransform.GetChild(i).gameObject;
                groundObject.name = "GroundObject";
                Rigidbody2D groundBody = groundObject.AddComponent<Rigidbody2D>();
                groundBody.isKinematic = true;
                groundBody.gravityScale = 0;

                BoxCollider2D groundCollider = groundObject.GetComponent<BoxCollider2D>();

                //them 1 cai gameobject chua boxcollider ngay truoc cac khoi dat
                float height = groundObject.GetComponent<BoxCollider2D>().size.y;
                GameObject markObject = new GameObject("MarkGround");
                markObject.transform.parent = markGounds;
                markObject.transform.localPosition = groundObject.transform.localPosition;
                BoxCollider2D markCollider = markObject.AddComponent<BoxCollider2D>();
                markCollider.size = new Vector2(0.1f, (height - 4)/100);
                markCollider.offset = new Vector2(0, - height / 200);
            }
        }

        private void createDeadLine()
        {
            deadLine = new GameObject(MapObjectNames.DeadLine);
            deadLine.transform.localPosition = new Vector3(120, -3.4f, 0);
            BoxCollider2D deadLineCollider = deadLine.AddComponent<BoxCollider2D>();
            deadLineCollider.size = new Vector2(250, 0.5f);
            deadLineCollider.isTrigger = true;

            GameObject hellObject = new GameObject(MapObjectNames.HellObject);
            hellObject.transform.localPosition = new Vector3(120, -6, 0);
            BoxCollider2D hellCollider = hellObject.AddComponent<BoxCollider2D>();
            hellCollider.size = new Vector2(250, 0.5f);
        }

        private void createFinishObject(Transform parentTransform)
        {
            for (int i = 0; i < parentTransform.childCount; i++)
            {
                GameObject finishObject = parentTransform.GetChild(i).gameObject;
                finishObject.name = MapObjectNames.FinishObject;
                if (finishObject.GetComponent<BoxCollider2D>() != null)
                    Destroy(finishObject.GetComponent<BoxCollider2D>());
                BoxCollider2D finishCollider = finishObject.AddComponent<BoxCollider2D>();
                finishCollider.size = new Vector2(20, 1000);
                finishCollider.isTrigger = true;
            }
        }

        public void Update()
        {
            if (isRunning)
            {
                m_time += Time.deltaTime;

                taskbar.setParams(m_gold, m_score, m_time);
            }
        }

        public void LateUpdate()
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            {
                if (InputController.Name == InputNames.GAMESCREEN)
                {
                    if (InputController.IsScreen)
                    {
                        if(Time.timeScale != 0)
                            animals.getAnimal(0).GetComponent<Animal>().Jump();
                        //createBuiTien(-1.2f, 1f);
                    }
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                InputController.IsScreen = true;
            }
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Backspace))
            {
                if (!isPrepare)
                {
                    if (dialog != null && dialog.activeSelf && InputController.Name == InputNames.DIALOG)
                    {
                        dialog.GetComponent<DialogUnity>().hideDialog();
                        pauseLayer.SetActive(true);
                    }else if (InputController.Name == InputNames.GAMESCREEN)
                    {
                        pauseGame();
                        dialog.GetComponent<DialogUnity>().showDialog();
                        pauseLayer.SetActive(false);
                    }
                    else if (pauseLayer.activeSelf)
                    {
                        resumeGame();
                    }
                }
            }
            if (isPrepare)
            {
                isPrepare = false;
                
                buttonSkills.setFonts(shopFont);
                prepareGame();
            }
        }

        private void createDialog()
        {
            dialog = (GameObject)Instantiate(dialog);
            Vector3 p = dialog.transform.localPosition;
            dialog.transform.localPosition = new Vector3(p.x, p.y, -8);
            DialogUnity dialogUnity = dialog.GetComponent<DialogUnity>();
            dialogUnity.setText("Do you want to quit?", "");
            dialogUnity.setDialogTwo(
                delegate()
                {
                    SoundManager.LoadBgMusic("Sounds/menu", true);
                    Application.LoadLevel("MapScreen");
                },
                delegate()
                {
                    pauseLayer.SetActive(true);
                });
        }

        public void addDurtGold(Vector3 coinPosition)
        {
            GameObject durtGold = (GameObject)Instantiate(Resources.Load<GameObject>("Gold"));
            durtGold.transform.localPosition = new Vector3(coinPosition.x, coinPosition.y, 0);
            durtGold.transform.parent = goldLayer.transform;
            durtGold.GetComponent<SpriteRenderer>().sortingLayerName = "MapObject";
        }

        public void showRevivalTask(float duration, EmotionType emotionType)
        {
            revivalTask.setIcon(emotionType);
            revivalTask.show(duration);
            setCombo(-1);
        }

        public void addScore(int score, Vector3 position)
        {
            if (score == 5)
            {
                GameObject addScore = new GameObject("Score");
                addScore.transform.parent = scoreLayer.transform;
                addScore.transform.localPosition = new Vector3(position.x, position.y, -1);
                addScore.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/add5");
                addScore.layer = LayerMask.NameToLayer("Map");
                addScore.GetComponent<SpriteRenderer>().sortingLayerName = "MapObject";
                addScore.AddComponent<Actor>().addAction(new ActionMoveTo(position.x + 2, position.y + 1.5f, 1.5f));
                Destroy(addScore, 1.5f);
            }
            else if (score == 10)
            {
                GameObject addScore = new GameObject("Score");
                addScore.transform.parent = scoreLayer.transform;
                addScore.transform.localPosition = new Vector3(position.x, position.y, -1);
                addScore.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/add10");
                addScore.layer = LayerMask.NameToLayer("Map");
                addScore.GetComponent<SpriteRenderer>().sortingLayerName = "MapObject";
                addScore.AddComponent<Actor>().addAction(new ActionSequence(
                    new ActionMoveTo(position.x + 2, position.y + 1.5f, 1.5f),
                    new ActionRunnable(delegate() {
                        Destroy(addScore);
                    })
                    ));
               // Destroy(addScore, 1.5f);
            }
            m_score += score;
        }

        public void eatSkillRandom(SkillType type, Vector3 position, int animalIndex)
        {
            if (animalIndex == 0)
            {
                if (useSkills)
                {
                    GameObject skillRand = new GameObject("SkilRand");
                    skillRand.transform.parent = scoreLayer.transform;
                    skillRand.transform.localPosition = new Vector3(position.x, position.y, -1);
                    skillRand.AddComponent<SpriteRenderer>().sprite = buttonSkills.skillSprites[SkillRandom.getSkillIndex(type)];
                    skillRand.layer = LayerMask.NameToLayer("Map");
                    skillRand.GetComponent<SpriteRenderer>().sortingLayerName = "MapObject";
                    skillRand.AddComponent<Actor>().addAction(new ActionSequence(
                        new ActionMoveTo(position.x + 3, position.y + 1.5f, 1.5f),
                        new ActionRunnable(delegate() {
                            Destroy(skillRand);
                        })
                        ));
                    skillRand.transform.localScale = new Vector3(0.5f, 0.5f, skillRand.transform.localScale.z);
                    //Destroy(skillRand, 1.5f);

                    buttonSkills.eateSkill(type);

                    addScore(5, position);
                }
            }
            else
            {
                int ra = Random.Range(1, 100);
                if(ra <= perSkills[Attr.currentLevel])
                    addSkill(type, animalIndex);
            }
        }

        public void pauseGame()
        {
            animals.setRunning(false);
            background.setRuning(false);
            InputController.Name = InputNames.DIALOG;
            pauseLayer.SetActive(true);
            isRunning = false;
            revivalTask.setRunning(false);
            setAnimationRunning(false);
            setBulletRunning(false);
            SoundManager.PauseMusic();
        }

        public void resumeGame()
        {
            InputController.Name = InputNames.GAMESCREEN;
            animals.setRunning(true);
            pauseLayer.SetActive(false);
            background.setRuning(true);
            isRunning = true;
            revivalTask.setRunning(true);
            setAnimationRunning(true);
            setBulletRunning(true);
            SoundManager.ResumeMusic("Sounds/bg1");
        }

        public void finishGame()
        {
            ARController.showInterstitialAd();
            prepareGame();
            //show result
            resultLayer = (GameObject)Instantiate(resultLayer);
            resultLayer.GetComponent<ResultLayer>().setParams(m_score, taskbar.getRankPlayer(), m_gold, m_time);

            InputController.Name = InputNames.DIALOG;
            SoundManager.stopMusic();
            if (resultLayer.GetComponent<ResultLayer>().getStar() > 0)
            {
                SoundManager.playSoundLong("Sounds/votay");
            }
            else
            {
                SoundManager.playSoundLong("Sounds/thua");
            }
           
        }

        private void setAnimationRunning(bool isRunning)
        {
            for (int i = 0; i < coinLayer.transform.childCount; i++)
            {
                coinLayer.transform.GetChild(i).gameObject.GetComponent<Bapk.Animation>().setRunning(isRunning);
            }
            for (int i = 0; i < goldLayer.transform.childCount; i++)
            {
                goldLayer.transform.GetChild(i).gameObject.GetComponent<Bapk.Animation>().setRunning(isRunning);
                goldLayer.transform.GetChild(i).gameObject.GetComponent<Gold>().setRunning(isRunning);
            }
            for (int i = 0; i < boosterLayer.transform.childCount; i++)
            {
                boosterLayer.transform.GetChild(i).gameObject.GetComponent<Bapk.Animation>().setRunning(isRunning);
            }
            for (int i = 0; i < shadowLayer.transform.childCount; i++)
            {
                if (shadowLayer.transform.GetChild(i).gameObject.GetComponent<Actor>() != null)
                    shadowLayer.transform.GetChild(i).gameObject.GetComponent<Actor>().setRunning(isRunning);
            }
        }

        private void setBulletRunning(bool isRunning)
        {
            for (int i = 0; i < bulletLayer.transform.childCount; i++)
            {
                GameObject bulletObject = bulletLayer.transform.GetChild(i).gameObject;
                bulletObject.GetComponent<Bullet>().setRunning(isRunning);
                if (bulletObject.GetComponent<Bapk.Animation>() != null)
                    bulletObject.GetComponent<Bapk.Animation>().setRunning(isRunning);
            }

            for (int i = 0; i < effectLayer.transform.childCount; i++)
            {
                GameObject effectObject = effectLayer.transform.GetChild(i).gameObject;
                effectObject.GetComponent<Effect>().setRunning(isRunning);
            }

            for (int i = 0; i < scoreLayer.transform.childCount; i++)
            {
                GameObject scoreObject = scoreLayer.transform.GetChild(i).gameObject;
                if (scoreObject.GetComponent<Actor>() != null)
                    scoreObject.GetComponent<Actor>().setRunning(isRunning);
            }
        }

        public void guideGame()
        {
            guideLayer = (GameObject)Instantiate(Resources.Load<GameObject>("GuideLayer"));
            guideLayer.GetComponent<GuideLayer>().gameScreen = this;
        }

        private void prepareGame()
        {
            animals.setRunning(false);
            background.setRuning(false);
            InputController.Name = InputNames.DIALOG;
            isRunning = false;
            revivalTask.setRunning(false);
            setAnimationRunning(false);
        }

        public void OnApplicationPause(bool pauseStatus)
        {
            if (!pauseStatus)
            {
                if (InputController.Name != InputNames.DIALOG && InputController.Name != "")
                {
                    pauseGame();
                    Debug.Log("Pause");
                }
            }
            else
            {
                Debug.Log("Pause true");
            }
        }

        public void showCounter()
        {
            if (counterLayer != null)
                counterLayer.SetActive(true);
            else
                pauseGame();
        }

        public void addSkill(SkillType type, int animalIndex)
        {
            switch (type) 
            {
                case SkillType.LUA:
                    {
                        GameObject bulletObject = (GameObject)Instantiate(Resources.Load<GameObject>("Bullets/LUA"));
                        bulletObject.name = "Bullet";
                        bulletObject.layer = LayerMask.NameToLayer("Animal" + (animalIndex + 1));
                        bulletObject.transform.parent = bulletLayer.transform;
                        GameObject animal = animals.getAnimal(animalIndex);
                        bulletObject.transform.localPosition = new Vector3(animal.transform.localPosition.x, animal.transform.localPosition.y + 0.2f, bulletObject.transform.localPosition.z);
                        Bullet bullet = bulletObject.GetComponent<Bullet>();
                        bullet.animalIndex = animalIndex;
                    }
                    break;
                case SkillType.DONGBANG:
                    {
                        GameObject bulletObject = (GameObject)Instantiate(Resources.Load<GameObject>("Bullets/DONGBANG"));
                        bulletObject.name = "Bullet";
                        bulletObject.layer = LayerMask.NameToLayer("Animal" + (animalIndex + 1));
                        bulletObject.transform.parent = bulletLayer.transform;
                        GameObject animal = animals.getAnimal(animalIndex);
                        bulletObject.transform.localPosition = new Vector3(animal.transform.localPosition.x, animal.transform.localPosition.y + 0.2f, bulletObject.transform.localPosition.z);
                        Bullet bullet = bulletObject.GetComponent<Bullet>();
                        bullet.animalIndex = animalIndex;
                    }
                    break;
                case SkillType.THIENTHACH:
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            GameObject bulletObject = (GameObject)Instantiate(Resources.Load<GameObject>("Bullets/THIENTHACH"));
                            bulletObject.name = "Bullet";
                            bulletObject.layer = LayerMask.NameToLayer("Animal" + (animalIndex + 1));
                            bulletObject.transform.parent = bulletLayer.transform;
                            GameObject animal = animals.getAnimal(animalIndex);
                            bulletObject.transform.localPosition = new Vector3(animal.transform.localPosition.x + 0.2f, animal.transform.localPosition.y + 0.4f, bulletObject.transform.localPosition.z);
                            Bullet bullet = bulletObject.GetComponent<Bullet>();
                            bullet.animalIndex = animalIndex;
                        }
                    }
                    break;
                case SkillType.SET:
                    {
                        GameObject bulletObject = (GameObject)Instantiate(Resources.Load<GameObject>("Bullets/SET"));
                        bulletObject.name = "Bullet";
                        bulletObject.layer = LayerMask.NameToLayer("Animal" + (animalIndex + 1));
                        bulletObject.transform.parent = bulletLayer.transform;
                        GameObject animal = animals.getAnimal(animalIndex);
                        bulletObject.transform.localPosition = new Vector3(animal.transform.localPosition.x + 0.2f, animal.transform.localPosition.y + 0.2f, bulletObject.transform.localPosition.z);
                        Bullet bullet = bulletObject.GetComponent<Bullet>();
                        bullet.animalIndex = animalIndex;
                    }
                    break;
                case SkillType.BOM:
                    {
                        GameObject bulletObject = (GameObject)Instantiate(Resources.Load<GameObject>("Bullets/BOM"));
                        bulletObject.name = "Bullet";
                        bulletObject.layer = LayerMask.NameToLayer("Animal" + (animalIndex + 1));
                        bulletObject.transform.parent = bulletLayer.transform;
                        GameObject animal = animals.getAnimal(animalIndex);
                        bulletObject.transform.localPosition = new Vector3(animal.transform.localPosition.x , animal.transform.localPosition.y + 0.5f , bulletObject.transform.localPosition.z);
                        Bullet bullet = bulletObject.GetComponent<Bullet>();
                        bullet.animalIndex = animalIndex;
                    }
                    break;
                case SkillType.DOICHO:
                    {
                        GameObject bulletObject = (GameObject)Instantiate(Resources.Load<GameObject>("Bullets/DOICHO"));
                        bulletObject.name = "Bullet";
                        bulletObject.layer = LayerMask.NameToLayer("Animal" + (animalIndex + 1));
                        bulletObject.transform.parent = bulletLayer.transform;
                        GameObject animal = animals.getAnimal(animalIndex);
                        bulletObject.transform.localPosition = new Vector3(animal.transform.localPosition.x + 0.3f, animal.transform.localPosition.y + 0.2f, bulletObject.transform.localPosition.z);
                        Bullet bullet = bulletObject.GetComponent<Bullet>();
                        bullet.animalIndex = animalIndex;
                    }
                    break;
                case SkillType.BAOVE:
                    {
                        Animal animal = animals.getAnimal(animalIndex).GetComponent<Animal>();
                        if (!animal.IsProtected)
                        {
                            GameObject effectObject = (GameObject)Instantiate(Resources.Load<GameObject>("Effects/BAOVE"));
                            effectObject.name = "Effect";
                            effectObject.transform.parent = effectLayer.transform;
                            effectObject.GetComponent<Effect>().mapObjectTransform = animal.gameObject.transform;
                            animal.protectedEffect = effectObject.GetComponent<Effect>();
                        }
                        else
                        {
                            animal.protectedEffect.stateTime = 5;
                        }
                        animals.getAnimal(animalIndex).GetComponent<Animal>().setProtected();
                    }
                    break;
                case SkillType.TANGTOC:
                    {
                        GameObject effectObject = (GameObject)Instantiate(Resources.Load<GameObject>("Effects/TANGTOC"));
                        effectObject.name = "Effect";
                        effectObject.transform.parent = effectLayer.transform;
                        effectObject.GetComponent<Effect>().mapObjectTransform = animals.getAnimal(animalIndex).transform;
                        animals.getAnimal(animalIndex).GetComponent<Animal>().setSpeedUp();
                    }
                    break;
            }
        }

        public void shot(SkillType type)
        {
            buttonSkills.shot(type);
        }

        public void setCombo(int type, SkillType skillType)
        {
            
            int skillIndex = SkillRandom.getSkillIndex(skillType);
            if (type == 1)
            {
                combo1.setCombo(skillIndex);
                if (combo1.getNumberCombo() >= 2)
                {
                    goldAddition.addGold1(20);
                    m_gold += combo1.getNumberCombo() * 5;
                }
            }
        }

        public void setCombo(int itemIndex)
        {
            if (itemIndex == -1)
            {
                combo1.setCombo(-1);
                combo2.setCombo(-1);
                return;
            }
            combo2.setCombo(itemIndex);
            if (combo2.getNumberCombo() >= 2)
            {
                goldAddition.addGold2(20);
                m_gold += combo1.getNumberCombo() * 5;
            }
        }

        public GameObject getAnimal(int animalIndex)
        {
            return animals.getAnimal(animalIndex);
        }

        public void AnimalcollisionWithBullet(GameObject animalObject, GameObject bulletObject)
        {
            if (animalObject.GetComponent<Animal>().isStanding) return;
            SkillType type = bulletObject.GetComponent<Bullet>().skillType;
            int animalIndex = animalObject.GetComponent<Animal>().animalIndex;
            switch (type)
            {
                case SkillType.LUA:
                    {
                        GameObject effectObject = (GameObject)Instantiate(Resources.Load<GameObject>("Effects/LUA"));
                        effectObject.transform.parent = effectLayer.transform;
                        effectObject.GetComponent<Effect>().mapObjectTransform = animalObject.transform;
                        Destroy(bulletObject);
                        animalObject.GetComponent<Animal>().setStand(true, 2, Animal.STATE_FIRE);
                        animalObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -300));
                        if (animalIndex == 0)
                        {
                            showRevivalTask(2, EmotionType.CUOIDEU);
                        }
                    }
                    break;
                case SkillType.DONGBANG:
                    {
                        GameObject effectObject = (GameObject)Instantiate(Resources.Load<GameObject>("Effects/DONGBANG"));
                        effectObject.transform.parent = effectLayer.transform;
                        effectObject.GetComponent<Effect>().mapObjectTransform = animalObject.transform;
                        Destroy(bulletObject);
                        animalObject.GetComponent<Animal>().setStand(true,2, Animal.STATE_RUN);
                        animalObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -300));
                        if (animalIndex == 0)
                        {
                            showRevivalTask(2, EmotionType.HOAMAT);
                        }
                    }
                    break;
                case SkillType.THIENTHACH:
                    {
                        {
                            GameObject effectObject = (GameObject)Instantiate(Resources.Load<GameObject>("Effects/THIENTHACH"));
                            effectObject.transform.parent = effectLayer.transform;
                            effectObject.GetComponent<Effect>().mapObjectTransform = animalObject.transform;
                        }
                        {
                            GameObject effectObject = (GameObject)Instantiate(Resources.Load<GameObject>("Effects/CHOANG"));
                            effectObject.transform.parent = effectLayer.transform;
                            effectObject.GetComponent<Effect>().mapObjectTransform = animalObject.transform;
                        }
                        Destroy(bulletObject);
                        animalObject.GetComponent<Animal>().setStand(true, 2, Animal.STATE_SHOCK);
                        animalObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -300));
                        if (animalIndex == 0)
                        {
                            showRevivalTask(2, EmotionType.HOAMAT);
                        }
                    }
                    break;
                case SkillType.SET:
                    {
                        GameObject effectObject = (GameObject)Instantiate(Resources.Load<GameObject>("Effects/SET"));
                        effectObject.transform.parent = effectLayer.transform;
                        effectObject.GetComponent<Effect>().mapObjectTransform = animalObject.transform;
                        animalObject.GetComponent<Animal>().setStand(true, 2, Animal.STATE_FIRE);
                        animalObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -300));
                        if (animalIndex == 0)
                        {
                            showRevivalTask(2, EmotionType.HOAMAT);
                        }
                    }
                    break;
                case SkillType.BOM:
                    {
                        GameObject effectObject = (GameObject)Instantiate(Resources.Load<GameObject>("Effects/LUA"));
                        effectObject.transform.parent = effectLayer.transform;
                        effectObject.GetComponent<Effect>().mapObjectTransform = animalObject.transform;
                        Destroy(bulletObject);
                        animalObject.GetComponent<Animal>().setStand(true, 2, Animal.STATE_FIRE);
                        animalObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -300));
                    }
                    break;
                case SkillType.DOICHO:
                    {
                        GameObject effectObject = (GameObject)Instantiate(Resources.Load<GameObject>("Effects/KHOI"));
                        effectObject.transform.parent = effectLayer.transform;
                        effectObject.GetComponent<Effect>().mapObjectTransform = animalObject.transform;
                        int animalIndexb = bulletObject.GetComponent<Bullet>().animalIndex;
                        Destroy(bulletObject);
                        GameObject animal2 = animals.getAnimal(animalIndexb);
                        GameObject effectObject2 = (GameObject)Instantiate(Resources.Load<GameObject>("Effects/KHOI"));
                        effectObject.transform.parent = effectLayer.transform;
                        effectObject2.GetComponent<Effect>().mapObjectTransform = animal2.transform;
                        Vector3 p1 = animalObject.transform.localPosition;
                        Vector3 p2 = animal2.transform.localPosition;
                        animal2.transform.localPosition = new Vector3(p1.x, p1.y, p2.z);
                        animalObject.transform.localPosition = new Vector3(p2.x, p2.y, p1.z);
                    }
                    break;
            }
        }
    }
}