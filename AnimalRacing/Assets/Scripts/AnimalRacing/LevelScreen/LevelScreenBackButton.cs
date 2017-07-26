using UnityEngine;
using System.Collections;

public class LevelScreenBackButton : InputAdapter {

    public override void OnTouchDown()
    {
        base.OnTouchDown();
        gameObject.transform.localScale = new Vector3(0.9f, 0.9f, gameObject.transform.localScale.z);
        SoundManager.playButtonSound();
    }

    public override void OnCheckUp()
    {
        base.OnCheckUp();
        gameObject.transform.localScale = new Vector3(1, 1, gameObject.transform.localScale.z);
    }

    public override void OnTouchUp()
    {
        base.OnTouchUp();
        Application.LoadLevel("MapScreen");
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Backspace))
        {
            Application.LoadLevel("MapScreen");
        }
    }

}
