using UnityEngine;
using System.Collections;

namespace GamePlay
{
    public class Effect : MonoBehaviour
    {

        public Transform mapObjectTransform;
        public bool isFollowing;
        public float stateTime;
        public float offsetX;
        public float offsetY;

        private bool isRunning;

        public void Start()
        {
            isRunning = true;
            transform.localPosition = new Vector3(mapObjectTransform.localPosition.x + offsetX, mapObjectTransform.localPosition.y + offsetY, transform.localPosition.z);
        }

        void Update()
        {
            if (isRunning)
            {
                if (isFollowing && stateTime != -1)
                {
                    transform.localPosition = new Vector3(mapObjectTransform.localPosition.x + offsetX, mapObjectTransform.localPosition.y + offsetY, transform.localPosition.z);
                    stateTime -= Time.deltaTime;
                    if (stateTime <= 0)
                        Destroy(gameObject);
                    if (mapObjectTransform.gameObject.GetComponent<Animal>() != null)
                        if (mapObjectTransform.GetComponent<Animal>().IsRevival())
                        {
                            Destroy(gameObject);
                        }
                }
                else if (isFollowing)
                {
                    transform.localPosition = new Vector3(mapObjectTransform.localPosition.x + offsetX, mapObjectTransform.localPosition.y + offsetY, transform.localPosition.z);
                    if (mapObjectTransform.gameObject.GetComponent<Animal>() != null)
                        if (mapObjectTransform.GetComponent<Animal>().IsRevival())
                        {
                            Destroy(gameObject);
                        }
                }
            }
        }

        public void setRunning(bool isRunning)
        {
            this.isRunning = isRunning;
            if (gameObject.GetComponent<Bapk.Animation>() != null)
                gameObject.GetComponent<Bapk.Animation>().setRunning(isRunning);

        }           
    }
}