using UnityEngine;
using System.Collections;

public class BackShopButtonListener : InputAdapter {

    public GameObject shopLayer;
    public GameObject pickSkillLayer;

    //BackButton
    public override void OnTouchDown()
    {
        if (InputController.Name != InputNames.SHOP) return;
        base.OnTouchDown();
        gameObject.transform.localScale = new Vector3(0.9f, 0.9f, 1);
        SoundManager.playButtonSound();
    }
    public override void OnCheckUp()
    {
        if (InputController.Name != InputNames.SHOP) return;
        base.OnCheckUp();
        gameObject.transform.localScale = new Vector3(1, 1, 1);
    }
    public override void OnTouchUp()
    {
        if (InputController.Name != InputNames.SHOP) return;
        base.OnTouchUp();
        backPressed();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Backspace))
        {
            if(InputController.Name != InputNames.DIALOG)
                backPressed();
        }
    }

    private void backPressed()
    {
        if (shopLayer.transform.localPosition.x == 0)
        {
            if (shopLayer.GetComponent<Actor>() != null)
                Destroy(shopLayer.GetComponent<Actor>());
            shopLayer.AddComponent<Actor>().addAction(new ActionSequence(
                new ActionScaleTo(0, 0, 0.5f, Interpolation.swingIn),
                new ActionRunnable(new Runnable(delegate()
                {
                    Application.LoadLevel("LevelScreen");
                }))
                ));
        }
        else if (shopLayer.transform.localPosition.x == -8)
        {
            shopLayer.SetActive(true);
            if (shopLayer.GetComponent<Actor>() != null)
                Destroy(shopLayer.GetComponent<Actor>());
            shopLayer.AddComponent<Actor>().addAction(new ActionMoveTo(0, 0, 0.3f));

            if (pickSkillLayer.GetComponent<Actor>() != null)
                Destroy(pickSkillLayer.GetComponent<Actor>());
            pickSkillLayer.AddComponent<Actor>().addAction(new ActionSequence(
                new ActionMoveTo(8, 0, 0.3f), new ActionRunnable(delegate() { pickSkillLayer.SetActive(false); })));
        }
    }
}
