using UnityEngine;
using System.Collections;

public abstract class TemporalAction : Action {

    private float duration, time;
    private InterpolationProcess interpolationProcess;
    private bool reverse, began, complete;
    private float percent;
    /*
    public TemporalAction() { }
    public TemporalAction(float duration) {
        this.duration = duration;
    }
    public TemporalAction(float duration, Interpolation interpolation)
    {
        this.duration = duration;
        this.interpolation = interpolation;
    }
     * */

    public override bool Act(float delta)
    {
        if (complete) return true;

        try
        {
            if (!began)
            {
                begin();
                began = true;
            }
            time += delta;
            complete = time >= duration;
            if (complete) percent = 1;
            else
            {
                percent = time / duration;
                if (interpolationProcess != null) percent = interpolationProcess.apply(percent);
            }
            //UpdateAction(reverse ? 1 - percent : percent);
            UpdateAction(percent);
            if (complete) { 
                end();
            }
            return complete;
        }
        finally {
            //return false;
        }
    }

    protected abstract void begin();

    protected abstract void end();

    protected abstract void UpdateAction(float percent);

    public void SetDuration(float duration)
    {
        this.duration = duration;
    }

    public void SetInterpolation(Interpolation interpolation)
    {
        this.interpolationProcess = InterpolationProcess.createInterpolation(interpolation);
    }

    public override void restart()
    {
        time = 0;
        began = false;
        complete = false;
    }

    public virtual void finish()
    {
        time = duration;
    }
}
