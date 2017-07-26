using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    [SerializeField]
    int index;
    [SerializeField]
    Button startButton;

    void Start()
    {
        int world = Data.getData(Data.KEY_WORLD_MAP);
        Debug.Log("Check map " + world + "__" + index);
        //startButton.interactable = (world > index);
        //if (world > index)
        //{
        //    startButton.interactable = false;
        //}
        //else
        //{
        //    startButton.transition = Selectable.Transition.ColorTint;
        //}
    }

    public void StartMap()
    {
        Debug.Log("Start map " + index);
        Attr.currentWorld = index;
        Scenes.Load(Scenes.LEVEL);
    }

    //void Update()
    //{
    //    int world = Data.getData(Data.KEY_WORLD_MAP);
    //    if (Input.touchCount > 0)
    //    {
    //        //TouchPhase phase
    //    }
    //}
}
