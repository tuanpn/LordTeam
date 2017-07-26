using UnityEngine;
using System.Collections.Generic;
using System.Xml;
using System.IO;

namespace GamePlay
{

    public class Animals : MonoBehaviour
    {
        private List<GameObject> animals;
        public GameScreen gameScreen;

        public void Start()
        {
            //string[] names = new string[] { "dog", "sheep", "pig", "fox", "giraffe", "panda", "rhino", "tiger", "elephant", "lion" };
            string[] names = new string[] { "hedgehog", "sheep", "pig", "fox", "giraffe", "panda", "rhino", "tiger", "elephant", "lion" };

            animals = new List<GameObject>();
            int animalIndex = 0;

            UpgradeInfo upgradeInfo = new UpgradeInfo();

            GameObject animalPlayer = (GameObject)Instantiate(Resources.Load<GameObject>("Animals/" + names[Attr.currentAnimal]));
            animalPlayer.transform.parent = gameObject.transform;
            animalPlayer.transform.localPosition = new Vector3(-3.5f + 0.7f * animalIndex, -0.9f, 0);
            setSortingLayer(animalPlayer, "Animal8");
            Animal player = animalPlayer.AddComponent<Animal>();
            player.setAnimalName(names[Attr.currentAnimal]);
            player.animalIndex = animalIndex;
            player.gameScreen = gameScreen;
            float speedPlayer = upgradeInfo.getItem(Attr.currentAnimal, 0, false);
            float jumpPlayer = upgradeInfo.getItem(Attr.currentAnimal, 1, false);
            player.setAnimalProperties(speedPlayer, jumpPlayer);
            animalPlayer.layer = LayerMask.NameToLayer("Animal1");

            animals.Add(animalPlayer);
            animalIndex++;

            animalPlayer.GetComponent<Animator>().Play("run");
            //animalPlayer.GetComponent<Animator>().SetBool(Animal.IS_PLAYER, true);

            TextAsset xml = Resources.Load<TextAsset>("Levels/WorldMap" + (Attr.currentWorld + 1));
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(new StringReader(xml.text));

            XmlNodeList xmlNodeList = xmlDoc.DocumentElement.ChildNodes;
            XmlNodeList levelNodeList = xmlNodeList.Item(Attr.currentLevel).ChildNodes;
            for (int i = 0; i < levelNodeList.Count; i++)
            {
                int numberAnimal = int.Parse(levelNodeList.Item(i).Attributes.Item(0).Value);
                string animalName = levelNodeList.Item(i).Attributes.Item(1).Value.ToLower();

                for (int k = 0; k < numberAnimal; k++)
                {
                    GameObject animalObject = (GameObject)Instantiate(Resources.Load<GameObject>("Animals/" + animalName));
                    animalObject.transform.parent = gameObject.transform;
                    animalObject.transform.localPosition = new Vector3(-3.5f + 0.7f*animalIndex, -0.9f, 0);
                    setSortingLayer(animalObject, "Animal" + animalIndex);
                    Animal animal = animalObject.AddComponent<Animal>();
                    animal.setAnimalName(animalName.ToLower() + "_blue");
                    animal.animalIndex = animalIndex;
                    animal.gameScreen = gameScreen;
                    float speed = upgradeInfo.getSpeed(i);
                    float jump = upgradeInfo.getJump(i);
                    animal.setAnimalProperties(speed, jump);
                    animalObject.layer = LayerMask.NameToLayer("Animal" + (animalIndex + 1));
                    animals.Add(animalObject);
                    animalIndex++;

                    animalObject.GetComponent<Animator>().Play("run_blue");

                    //animalObject.GetComponent<Animator>().SetBool(Animal.IS_PLAYER, false);
                }
            }
        }

        private void setSortingLayer(GameObject gObject, string sortingLayerName)
        {
            Transform[] transforms = gObject.GetComponentsInChildren<Transform>(true);
            for (int i = 0; i < transforms.Length; i++)
            {
                if (transforms[i].GetComponent<SpriteRenderer>() != null)
                    transforms[i].GetComponent<SpriteRenderer>().sortingLayerName = sortingLayerName;
            }
        }

        public GameObject getAnimal(int animalIndex)
        {
            return animals[animalIndex];
        }

        public void setRunning(bool isRunning)
        {
            for (int i = 0; i < animals.Count; i++)
            {
                animals[i].GetComponent<Animal>().setRunning(isRunning);
            }
        }
    }

}