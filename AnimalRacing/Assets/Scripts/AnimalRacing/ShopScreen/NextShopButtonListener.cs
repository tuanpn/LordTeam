using UnityEngine;
using System.Collections;

public class NextShopButtonListener : InputAdapter {

    public GameObject shopLayer;
    public GameObject pickSkillLayer;

	public void Start () {
        gameObject.AddComponent<Actor>().addAction(new ActionRepeat(ActionRepeat.FOREVER, new ActionSequence(
            new ActionScaleTo(0.95f, 0.95f, 0.2f, Interpolation.sine),
            new ActionScaleTo(1, 1, 0.2f, Interpolation.sine)
            )));
	}

    public override void OnTouchDown()
    {
        if (InputController.Name != InputNames.SHOP) return;
        base.OnTouchDown();
        gameObject.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
        SoundManager.playButtonSound();
    }
    public override void OnCheckUp()
    {
        if (InputController.Name != InputNames.SHOP) return;
        base.OnCheckUp();
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
    }
    public override void OnTouchUp()
    {
        if (InputController.Name != InputNames.SHOP) return;
        base.OnTouchUp();
        if (shopLayer.GetComponent<Actor>() != null)
            Destroy(shopLayer.GetComponent<Actor>());
        shopLayer.AddComponent<Actor>().addAction(new ActionSequence(new ActionMoveTo(-8, 0, 0.3f), new ActionRunnable(delegate() { shopLayer.SetActive(false); })));

        pickSkillLayer.SetActive(true);
        if (pickSkillLayer.GetComponent<Actor>() != null)
            Destroy(pickSkillLayer.GetComponent<Actor>());
        pickSkillLayer.AddComponent<Actor>().addAction(new ActionMoveTo(0, 0, 0.3f));
    }
}
