using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public void BackToMainMenu()
    {
        Scenes.Load(Scenes.MAIN_MENU);
    }
}
