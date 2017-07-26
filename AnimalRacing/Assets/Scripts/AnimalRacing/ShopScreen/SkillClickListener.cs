using UnityEngine;
using System.Collections;

public class SkillClickListener : InputAdapter
{
    public int skillIndex;
    public SkillDescription skillDescription;
    public Descriptions descriptions;
    public int starUnlock;
    public CirclesChoosed circleChoosed;

    public Vector3 oldPosition;
    public bool picked;
    public int cirlceIndex;

    public void Start()
    {
        oldPosition = gameObject.transform.localPosition;
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
        descriptions.setText(skillDescription.getDesciptions1(skillIndex), skillDescription.getDesciptions2(skillIndex), skillDescription.getName(skillIndex), starUnlock);
        circleChoosed.choosedSkill(gameObject, skillIndex, starUnlock);
    }
}