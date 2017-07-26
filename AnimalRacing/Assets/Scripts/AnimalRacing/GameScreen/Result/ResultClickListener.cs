using UnityEngine;
using System.Collections;

public class ResultClickListener : InputAdapter {

    public override void OnTouchDown()
    {
        if (InputController.Name != InputNames.DIALOG) return;
        base.OnTouchDown();
        gameObject.transform.localScale = new Vector3(0.9f, 0.9f, gameObject.transform.localScale.z);
        SoundManager.playButtonSound();
    }

    public override void OnCheckUp()
    {
        if (InputController.Name != InputNames.DIALOG) return;
        base.OnCheckUp();
        gameObject.transform.localScale = new Vector3(1, 1, gameObject.transform.localScale.z);
    }

    public override void OnTouchUp()
    {
        if (InputController.Name != InputNames.DIALOG) return;
        base.OnTouchUp();
        switch(gameObject.name)
        {
            case "Bt_Menu":
                Application.LoadLevel("MapScreen");
                SoundManager.LoadBgMusic("Sounds/menu", true);
                break;
            case "Bt_Replay":
                Application.LoadLevel("GameScreen");
                break;
            case "Bt_Next":
                if (Attr.currentLevel == 14)
                {
                    Application.LoadLevel("MapScreen");
                }
                else 
                {
                    Attr.currentLevel++;
                    Application.LoadLevel("ShopScreen");
                    SoundManager.LoadBgMusic("Sounds/menu", true);
                }
                break;
        }
    }

	public void Start () {
	
	}
	
	public void Update () {
	
	}
}
