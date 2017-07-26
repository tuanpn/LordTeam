using UnityEngine;
using System.Collections;

public abstract class RelativeTemporalAction : TemporalAction{

    private float lastPercent;

    protected override void begin()
    {
        lastPercent = 0;
    }

    protected override void end()
    {
    }

    protected override void UpdateAction(float percent)
    {
        updateRelative(percent - lastPercent);
        lastPercent = percent;
    }

    protected abstract void updateRelative(float percentDelta);
}
