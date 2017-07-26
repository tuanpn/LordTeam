using UnityEngine;
using System.Collections;

namespace GamePlay
{

    public class CameraController : MonoBehaviour
    {

        public Animals animals;

        private GameObject animalPlayer;

        public void Start()
        {
            //animalPlayer = animals.getAnimal(0);
        }

        void Update()
        {
            if(animalPlayer == null){
                animalPlayer = animals.getAnimal(0);
                return;
            }
            
            if (animalPlayer.transform.localPosition.x >= -1)
                transform.localPosition = new Vector3(animalPlayer.transform.localPosition.x + 1, 0, transform.localPosition.z);
        }
    }
}