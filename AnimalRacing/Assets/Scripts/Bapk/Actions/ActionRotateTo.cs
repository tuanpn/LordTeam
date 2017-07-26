using UnityEngine;
using System.Collections;

public class ActionRotateTo : TemporalAction {
    private float startRotation;
    private float endRotation;


    public ActionRotateTo(float rotation, float duration)
    {
        this.endRotation = rotation;
        SetDuration(duration);
    }

    public ActionRotateTo(float rotation, float duration, Interpolation interpolation)
    {
        this.endRotation = rotation;
        SetDuration(duration);
        SetInterpolation(interpolation);
    }

    protected override void begin()
    {
        startRotation = actor.gameObject.transform.localRotation.z;
    }

    protected override void end()
    {
    }

    protected override void UpdateAction(float percent)
    {
        actor.gameObject.transform.localRotation = Quaternion.Euler(actor.gameObject.transform.localRotation.x, actor.gameObject.transform.localRotation.y, startRotation + (endRotation - startRotation) * percent);
    }
}
