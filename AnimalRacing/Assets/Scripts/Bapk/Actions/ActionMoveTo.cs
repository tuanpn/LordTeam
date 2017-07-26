using UnityEngine;
using System.Collections;

public class ActionMoveTo : TemporalAction {

    private float startX, startY;
    private float endX, endY;

    public ActionMoveTo(float x, float y, float duration)
    {
        SetMoveTo(x, y);
        SetDuration(duration);
    }

    public ActionMoveTo(float x, float y, float duration, Interpolation interpolation)
    {
        SetMoveTo(x, y);
        SetDuration(duration);
        SetInterpolation(interpolation);
    }

    protected override void begin()
    {
        Vector3 position = actor.gameObject.transform.localPosition;
        startX = position.x;
        startY = position.y;
    }

    protected override void UpdateAction(float percent)
    {
        actor.gameObject.transform.localPosition = new Vector3(startX + (endX - startX) * percent, startY + (endY - startY) * percent, actor.gameObject.transform.localPosition.z);
    }

    private void SetMoveTo(float x, float y)
    {
        endX = x;
        endY = y;
    }

    protected override void end()
    {
    }
}
