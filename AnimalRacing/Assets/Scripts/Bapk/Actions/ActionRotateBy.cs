using UnityEngine;
using System.Collections;

public class ActionRotateBy : RelativeTemporalAction {

    private float rotation;

    public ActionRotateBy() { }
    public ActionRotateBy(float rotation, float duration) {
        this.rotation = rotation;
        SetDuration(duration);
    }
    public ActionRotateBy(float rotation, float duration, Interpolation interpolation)
    {
        this.rotation = rotation;
        SetDuration(duration);
        SetInterpolation(interpolation);
    }

    protected override void updateRelative(float percentDelta)
    {
        actor.transform.Rotate(actor.transform.localRotation.x, actor.transform.localRotation.y, rotation * percentDelta);
    }
    
}
