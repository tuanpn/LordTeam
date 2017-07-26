using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour
{
    public const string GAME = "GameScreen";
    public const string MAIN_MENU = "MainMenu";
    public const string LEVEL = "LevelScreen";
    public const string MAP = "MapScreen";
    public const string SHOP = "ShopScreen";

    public static void Load(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
