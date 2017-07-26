using UnityEngine;
using System.Collections;

public class ActionScaleTo : TemporalAction {

    private float startX, startY;
    private float endX, endY;

    public ActionScaleTo() { }
    public ActionScaleTo(float scaleX, float scaleY, float duration)
    {
        SetScaleTo(scaleX, scaleY);
        SetDuration(duration);
    }
    public ActionScaleTo(float scaleX, float scaleY, float duration, Interpolation interpolation)
    {
        SetScaleTo(scaleX, scaleY);
        SetDuration(duration);
        SetInterpolation(interpolation);
    }

    protected override void begin()
    {
        Vector3 scale = actor.gameObject.transform.localScale;
        startX = scale.x;
        startY = scale.y;
    }

    protected override void end()
    {
        
    }

    protected override void UpdateAction(float percent)
    {
        //actor.gameObject.transform.localScale.Set(startX + (endX - startX) * percent, startY + (endY - startY) * percent, actor.gameObject.transform.localScale.z);
        actor.gameObject.transform.localScale = new Vector3(startX + (endX - startX) * percent, startY + (endY - startY) * percent, actor.gameObject.transform.localScale.z);
    }

    public void SetScaleTo(float scaleX, float scaleY)
    {
        endX = scaleX;
        endY = scaleY;
    }
}
