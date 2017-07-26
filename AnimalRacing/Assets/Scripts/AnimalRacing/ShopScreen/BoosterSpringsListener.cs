using UnityEngine;
using System.Collections;

public class BoosterSpringsListener : InputAdapter {

    public GameObject chooseObject;
    public UpgradeLayer upgradeLayer;
    public int itemIndex;

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
        chooseObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y + 0.01f, chooseObject.transform.localPosition.z);
        upgradeLayer.UpdateUI(itemIndex);
    }

	public void Start () {
        if (chooseObject.GetComponent<Actor>() == null)
            chooseObject.AddComponent<Actor>().addAction(new ActionRepeat(ActionRepeat.FOREVER, new ActionSequence(
                new ActionScaleTo(0.95f, 0.95f, 0.2f, Interpolation.swingOut),
                new ActionScaleTo(1, 1, 0.2f, Interpolation.swingOut)
                )));
	}
}
