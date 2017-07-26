using UnityEngine;
using System.Collections;

public class UpgradeButtonClickListener : InputAdapter {

    public UpgradeLayer upgradeLayer;

    public override void OnTouchDown()
    {
        if (InputController.Name != InputNames.SHOP) return;
        base.OnTouchDown();
        transform.localScale = new Vector3(0.9f, 0.9f, transform.localScale.z);
        SoundManager.playButtonSound();
    }

    public override void OnCheckUp()
    {
        if (InputController.Name != InputNames.SHOP) return;
        base.OnCheckUp();
        transform.localScale = new Vector3(1, 1, transform.localScale.z);
    }

    public override void OnTouchUp()
    {
        if (InputController.Name != InputNames.SHOP) return;
        base.OnTouchUp();
        upgradeLayer.upgrade();
    }

}
