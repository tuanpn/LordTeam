using UnityEngine;
using System.Collections;

public class ActionDelay : Action {

    private float duration, time;
    private bool complete;

    public ActionDelay(float duration) {
        this.duration = duration;
    }

    public override void restart()
    {
        base.restart();
        time = 0;
        complete = false;
    }

    public override bool Act(float delta)
    {
        if (complete) return true;
        time += delta;
        complete = time >= duration;
        return complete;
    }
}
