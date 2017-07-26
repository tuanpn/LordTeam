using UnityEngine;
using System.Collections;

namespace GamePlay
{
    public class Bullet : MonoBehaviour
    {
        private Vector3 speed;
        private bool isRunning;
        private float stateTime;

        public int animalIndex { get; set; }
        public SkillType skillType;// { get; set; }

        private bool isParabol;
        
        void Start()
        {
            speed = new Vector3(10f, 0, 0);
            stateTime = 3;
            isRunning = true;
        }

        void Update()
        {
            if (isRunning)
            {
                switch (skillType)
                {
                    case SkillType.LUA:
                    case SkillType.DONGBANG:
                    case SkillType.SET:
                    case SkillType.DOICHO:
                        line();
                        break;
                    case SkillType.THIENTHACH:
                        parabol();
                        break;
                }

            }
        }

        private void line()
        {
            //transform.Translate(speed * Time.deltaTime);
            //transform.localPosition += speed * Time.deltaTime;
            GetComponent<Rigidbody2D>().velocity = speed;
            stateTime -= Time.deltaTime;
            if (stateTime <= 0)
                Destroy(gameObject);
        }

        private void parabol()
        {
            if (!isParabol)
            {
                isParabol = true;
                Rigidbody2D body = gameObject.AddComponent<Rigidbody2D>();
                body.velocity = new Vector2(Random.Range(8f, 10f), 4);
            }
        }

        public void setRunning(bool isRunning)
        {
            this.isRunning = isRunning;
            //collider2D.enabled = isRunning;
            GetComponent<Rigidbody2D>().simulated = isRunning;
        }

        public void OnTriggerEnter2D(Collider2D otherCollider)
        {
            if (otherCollider.gameObject.name == MapObjectNames.DeadLine)
            {
                Destroy(gameObject);
            }
            else if (otherCollider.gameObject.name == MapObjectNames.FinishObject)
            {
                Destroy(gameObject);
            }
        }


    }
}
