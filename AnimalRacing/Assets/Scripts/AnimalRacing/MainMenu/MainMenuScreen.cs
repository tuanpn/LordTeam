using UnityEngine;
using System.Collections;

public class MainMenuScreen : MonoBehaviour {
    void Awake()
    {
        //SoundManager.LoadBgMusic("Sounds/menu", false);
    }

    public void Start()
    {
        ARController.setBannerVisible(false);
        ARController.showInterstitialAd();
        Data.InitData();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Backspace))
        {
            if(InputController.Name == InputNames.MAINMENU)
                Application.Quit();
        }
    }
}
