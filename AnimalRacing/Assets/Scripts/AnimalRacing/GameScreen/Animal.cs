using UnityEngine;
using System.Collections;

namespace GamePlay
{
    public class Animal : MonoBehaviour
    {
        public int animalIndex;

        public GameScreen gameScreen;

        [SerializeField]
        private int state;

        private float speedX;
        private float speedY;

        private int stepJump;

        private bool isBooster;
        private float boosterTime;

        private bool isSprings;

        private Sprite shadowSprite;
        private float shadowCreateTime;

        private Vector3 revivalPosition;

        private bool isRevival;
        private float revivalTime;

        public bool isStanding;
        private float standTime;

        public bool IsProtected { get; set; }
        public float ProtectedTime { get; set; }

        public bool IsSpeedUp { get; set; }
        public float SpeedUpTime { get; set; }
        private bool IsSpeedUpStart;

        [SerializeField]
        private bool isRunning;

        public Effect protectedEffect { get; set; }

        private AnimalProperties properties;

        [SerializeField]
        private Rigidbody2D animalBody;
        void Start()
        {
            CircleCollider2D animalCollider = gameObject.AddComponent<CircleCollider2D>();
            animalCollider.radius = 0.3f;
            animalCollider.offset = new Vector2(0, 0.27f);
            animalCollider.sharedMaterial = Resources.Load<PhysicsMaterial2D>("AnimalPhysics");
            animalBody = gameObject.AddComponent<Rigidbody2D>();
            //animalBody.fixedAngle = true;
            animalBody.constraints = RigidbodyConstraints2D.FreezeRotation;
            state = JUMPING;
            //gameObject.name = "Animal";

            //test
            //if (animalIndex != 0) gameObject.SetActive(false);

            stepJump = 2;
            isRunning = true;
        }

        public void setAnimalProperties(float speedX_decimal, float speedY_decimal)
        {
            properties = new AnimalProperties(speedX_decimal, speedY_decimal);
            // properties.printInfo();
        }

        public void setAnimalName(string animalName)
        {
            shadowSprite = Resources.Load<Sprite>("Animals/Shadows/" + animalName);
        }

        private void createShadow()
        {
            if (animalIndex != 0) return;
            shadowCreateTime += Time.deltaTime;
            if (shadowCreateTime >= 0.05f)
            {
                shadowCreateTime = 0;
                GameObject shadowObject = new GameObject("Shadow");
                shadowObject.transform.parent = gameScreen.shadowLayer.transform;
                shadowObject.layer = LayerMask.NameToLayer("AnimalStand");
                shadowObject.transform.localPosition = transform.localPosition + new Vector3(0, 0.3f, 0);
                shadowObject.AddComponent<SpriteRenderer>().sprite = shadowSprite;
                shadowObject.GetComponent<SpriteRenderer>().sortingLayerName = "MapObject";
                shadowObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.8f);
                shadowObject.AddComponent<Actor>().addAction(new ActionSequence(
                    new ActionColorTo(0, 0, 0, 0, 0.5f),
                    new ActionRunnable(delegate ()
                {
                    Destroy(shadowObject);
                })
                ));
            }
        }

        public void Jump()
        {
            if (stepJump > 0 && !isStanding && !isRevival)
            {
                isSprings = false;
                state = JUMP;
                stepJump--;
                if (animalIndex == 0)
                    SoundManager.playSound("Sounds/jump");
            }
        }

        private void booster()
        {
            isBooster = true;
            boosterTime = 2;
            isSprings = false;
            if (animalIndex == 0)
                SoundManager.playSound("Sounds/booster");
        }

        private void springs()
        {
            isSprings = true;
            isBooster = false;
            state = JUMP;
            if (animalIndex == 0)
                SoundManager.playSound("Sounds/springs");
        }

        private void SetVelocity(int action)
        {
            switch (action)
            {
                case RUN:
                    if (isBooster)
                    {
                        //speedX = 7;
                        speedX = properties.speedXBooster;
                        speedY = 0;
                    }
                    else
                    {
                        //speedX = 3;
                        speedX = properties.speedX;
                        speedY = 0;
                    }
                    break;
                case JUMP:
                    if (isBooster)
                    {
                        //speedX = 7;
                        speedX = properties.speedXBooster;
                        //speedY = 5;
                        speedY = properties.speedYBooster;
                    }
                    else if (isSprings)
                    {
                        //speedX = 9;
                        speedX = properties.speedXSprings;
                        //speedY = 7;
                        speedY = properties.speedYSprings;
                    }
                    else
                    {
                        //speedX = 3;
                        speedX = properties.speedX;
                        //speedY = 5;
                        speedY = properties.speedY;
                    }
                    break;
                case JUMPING:
                    break;
            }
        }

        public void changeToState(string state_key)
        {
            Animator animator = gameObject.GetComponent<Animator>();
            switch (state_key)
            {
                case STATE_RUN:
                    animator.Play("run" + (animalIndex == 0 ? "" : "_blue"));
                    /*
                    animator.SetBool(STATE_SHOCK, false);
                    animator.SetBool(STATE_FIRE, false);
                    animator.SetBool(STATE_RUN, true);
                    animator.SetBool(IS_PLAYER, animalIndex == 0);
                     * */
                    break;
                case STATE_SHOCK:
                    animator.Play("run_choang" + (animalIndex == 0 ? "" : "_blue"));
                    /*
                    animator.SetBool(STATE_SHOCK, true);
                    animator.SetBool(STATE_FIRE, false);
                    animator.SetBool(STATE_RUN, false);
                     * */
                    break;
                case STATE_FIRE:
                    animator.Play("run_black");
                    /*
                    animator.SetBool(STATE_SHOCK, false);
                    animator.SetBool(STATE_FIRE, true);
                    animator.SetBool(STATE_RUN, false);
                     * */
                    break;
            }
        }

        public void Update()
        {
            if (isRunning)
            {
                if (isStanding)
                {
                    standTime -= Time.deltaTime;
                    if (standTime <= 0)
                    {
                        isStanding = false;
                        setStand(false, 0, STATE_RUN);
                    }
                }
                else if (IsSpeedUp)
                {
                    SpeedUpTime -= Time.deltaTime;
                    if (!IsSpeedUpStart)
                    {
                        if (transform.localPosition.y <= 1f)
                            animalBody.velocity = new Vector2(4, 5);
                        else
                            IsSpeedUpStart = true;
                    }
                    else
                        animalBody.velocity = new Vector2(12, 0);

                    if (SpeedUpTime <= 0)
                    {
                        IsSpeedUp = false;
                    }
                }
                else
                {
                    UpdateAnimal();
                }

                if (IsProtected)
                {
                    ProtectedTime -= Time.deltaTime;
                    if (ProtectedTime <= 0)
                        IsProtected = false;
                }

            }
        }

        private void UpdateAnimal()
        {
            SetVelocity(state);
            switch (state)
            {
                case RUN:
                //GetComponent<Rigidbody2D>().velocity = new Vector2(speedX, speedY);
                //break;
                case JUMP:
                    animalBody.velocity = new Vector2(speedX, speedY);
                    state = JUMPING;
                    break;
                case JUMPING:
                    break;
            }
            if (isBooster)
            {
                boosterTime -= Time.deltaTime;
                if (boosterTime <= 0)
                {
                    isBooster = false;
                }
                createShadow();
            }
            else if (isSprings)
            {
                createShadow();
            }

            if (isRevival)
            {
                revivalTime -= Time.deltaTime;
                if (revivalTime <= 0)
                {
                    revival();
                }
            }
        }

        public void OnCollisionEnter2D(Collision2D otherCollision)
        {
            if (otherCollision.gameObject.name == MapObjectNames.GroundObject)
            {
                state = RUN;
                stepJump = 2;
                isSprings = false;
            }
            else if (otherCollision.gameObject.name == "Bullet")
            {
                if (gameObject.GetComponent<Animal>().IsProtected || gameObject.GetComponent<Animal>().isStanding) return;
                if (animalIndex != otherCollision.gameObject.GetComponent<Bullet>().animalIndex)
                {
                    if (otherCollision.gameObject.GetComponent<Bullet>().skillType == SkillType.THIENTHACH || otherCollision.gameObject.GetComponent<Bullet>().skillType == SkillType.BOM)
                    {
                        if (otherCollision.gameObject.GetComponent<Bullet>().animalIndex == 0)
                        {
                            gameScreen.addScore(10, gameObject.transform.localPosition);
                            gameScreen.setCombo(1, otherCollision.gameObject.GetComponent<Bullet>().skillType);
                        }
                        gameScreen.AnimalcollisionWithBullet(gameObject, otherCollision.gameObject);
                    }
                }
            }
        }

        public void OnTriggerEnter2D(Collider2D otherCollider)
        {
            string mapObjectName = otherCollider.gameObject.name;

            if (mapObjectName == MapObjectNames.MarkObject && animalIndex != 0)
            {
                int r = Random.Range(0, 2);
                if (r == 0)
                    if (!isBooster && !isSprings)
                        Jump();
            }
            else if (mapObjectName == MapObjectNames.HoleObject && animalIndex != 0)
            {
                Jump();
            }
            else if (mapObjectName == MapObjectNames.BoostObject)
            {
                booster();
                stepJump = 2;
                if (animalIndex == 0)
                {
                    gameScreen.setCombo(6);
                    gameScreen.addScore(5, transform.localPosition);
                }
            }
            else if (mapObjectName == MapObjectNames.SpringsObject)
            {
                springs();
                stepJump = 2;
                if (animalIndex == 0)
                {
                    gameScreen.setCombo(7);
                    gameScreen.addScore(5, transform.localPosition);
                }
            }
            else if (mapObjectName == MapObjectNames.RevivalObject)
            {
                setRevivalPosition(otherCollider.gameObject.GetComponent<RevivalObject>().revialIndex + 1);
            }
            else if (mapObjectName == MapObjectNames.DeadLine)
            {
                if (animalIndex == 0)
                {
                    gameScreen.showRevivalTask(2, EmotionType.BUON);
                }
                isRevival = true;
                setStand(false, 0, STATE_RUN);
                IsProtected = false;
                revivalTime = 2;
            }
            else if (mapObjectName == MapObjectNames.SkillObject)
            {
                gameScreen.eatSkillRandom(otherCollider.gameObject.GetComponent<SkillRandom>().skillType,
                       otherCollider.gameObject.transform.localPosition, animalIndex);
                if (animalIndex == 0)
                    SoundManager.playSound("Sounds/anskill");

            }
            else if (mapObjectName == MapObjectNames.FinishObject && animalIndex == 0)
            {
                gameScreen.finishGame();

                Destroy(otherCollider);//cho nay de tranh lap lai nhieu lan ham finishgame
            }
            else if (mapObjectName == MapObjectNames.Bullet)
            {
                if (animalIndex != otherCollider.gameObject.GetComponent<Bullet>().animalIndex)
                {
                    if (IsProtected) return;
                    gameScreen.AnimalcollisionWithBullet(gameObject, otherCollider.gameObject);
                    isSprings = false;
                    isBooster = false;

                    if (otherCollider.gameObject.GetComponent<Bullet>().animalIndex == 0)
                    {
                        gameScreen.addScore(10, gameObject.transform.localPosition);
                        gameScreen.setCombo(1, otherCollider.gameObject.GetComponent<Bullet>().skillType);
                    }
                    else
                    {
                        gameScreen.setCombo(-1);
                    }
                }
            }
            else if (otherCollider.gameObject.name == MapObjectNames.Coin && animalIndex == 0)//if is player
            {
                gameScreen.addDurtGold(otherCollider.gameObject.transform.localPosition);
                Destroy(otherCollider.gameObject);
                gameScreen.m_gold++;
                SoundManager.playSound("Sounds/anxu");
            }
        }

        public void OnCollisionExit2D(Collision2D otherCollision)
        {
            if (otherCollision.gameObject.name == MapObjectNames.GroundObject)
            {
                if (state == RUN)
                {
                    state = JUMPING;
                }
            }
        }

        private void setRevivalPosition(int revivalIndex)
        {
            revivalPosition = gameScreen.getRevivalPosition(revivalIndex);
        }

        public void revival()
        {
            transform.localPosition = new Vector3(revivalPosition.x / 100 - 4, revivalPosition.y / 100 + 2.5f, transform.localPosition.z);
            isRevival = false;
        }

        public bool IsRevival()
        {
            return this.isRevival;
        }

        public void setRunning(bool isRunning)
        {
            this.isRunning = isRunning;
            gameObject.GetComponent<Animator>().enabled = isRunning;
            gameObject.GetComponent<Rigidbody2D>().simulated = isRunning;
        }

        public void setStand(bool isStand, float standTime, string state_key)
        {
            this.isStanding = isStand;
            this.standTime = standTime;
            changeToState(state_key);
            if (isStand)
            {
                gameObject.layer = LayerMask.NameToLayer("AnimalStand");
                //gameObject.GetComponent<Animator>().Play("run_black");
            }
            else
            {
                gameObject.layer = LayerMask.NameToLayer("Animal" + (animalIndex + 1));
                //gameObject.GetComponent<Animator>().Play("run" + (animalIndex == 0?"":"_blue"));
            }
        }

        public void setProtected()
        {
            IsProtected = true;
            ProtectedTime = 5;
        }

        public void setSpeedUp()
        {
            IsSpeedUp = true;
            SpeedUpTime = 5;
            IsSpeedUpStart = false;
        }

        private const int JUMP = 1;
        private const int JUMPING = 2;
        private const int RUN = 3;

        public const string STATE_SHOCK = "shock";
        public const string STATE_RUN = "run";
        public const string STATE_FIRE = "fire";
        public const string IS_PLAYER = "isplayer";
    }
}