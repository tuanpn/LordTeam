using UnityEngine;
using System.Collections;

public class LevelClickListener : InputAdapter {
    public int levelIndex;
    public GameObject boardLevel;

    public override void OnTouchDown()
    {
        base.OnTouchDown();
        gameObject.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 1);
        SoundManager.playButtonSound();
    }

    public override void OnCheckUp()
    {
        base.OnCheckUp();
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
    }

    public override void OnTouchUp()
    {
        base.OnTouchUp();
        Attr.currentLevel = levelIndex;
        if (boardLevel.GetComponent<Actor>() != null)
            Destroy(boardLevel.GetComponent<Actor>());

        boardLevel.AddComponent<Actor>().addAction(new ActionSequence(
            new ActionScaleTo(0, 0, 0.5f, Interpolation.swingIn),
            new ActionRunnable(new Runnable(delegate() { Application.LoadLevel("ShopScreen"); }))
            ));

    }
}
