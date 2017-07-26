using UnityEngine;
using System.Collections.Generic;

public class BigAnimals : MonoBehaviour {

    public GameObject[] animals;
    public void Start()
    {
        setAnimalIndex(Attr.currentAnimal);
    }

    public void setAnimalIndex(int animalIndex)
    {
        setAllInvisible();
        animals[animalIndex].SetActive(true);
        Attr.currentAnimal = animalIndex;
    }

    private void setAllInvisible()
    {
        for (int i = 0; i < animals.Length; i++)
            animals[i].SetActive(false);
    }

    public GameObject getAnimalObject()
    {
        return animals[Attr.currentAnimal];
    }
}
