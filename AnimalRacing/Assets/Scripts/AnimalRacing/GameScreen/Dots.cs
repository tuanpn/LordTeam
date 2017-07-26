using UnityEngine;
using System.Collections;

namespace GamePlay
{
    public class Dots : MonoBehaviour
    {
        public Animals animals;
        public GameObject[] dots;

        private int rankPlayer;
        private float animalX;

        private int rankTemp;

        public void Update()
        {
            GameObject player = animals.getAnimal(0);
            dots[0].transform.localPosition = new Vector3(player.transform.localPosition.x * 4.2f / 225 - 2.9f, dots[0].transform.localPosition.y, dots[0].transform.localPosition.z);

            animalX = player.transform.localPosition.x;
            rankTemp = 8;

            for (int i = 1; i < 8; i++)
            {
                GameObject animal = animals.getAnimal(i);
                dots[i].transform.localPosition = new Vector3(animal.transform.localPosition.x * 4.2f / 225 - 2.9f, dots[i].transform.localPosition.y, dots[i].transform.localPosition.z);
                if (animalX >= animal.transform.localPosition.x)
                {
                    rankTemp--;
                }
            }
            rankPlayer = rankTemp;
        }

        public int getRankPlayer()
        {
            return rankPlayer;
        }
    }
}