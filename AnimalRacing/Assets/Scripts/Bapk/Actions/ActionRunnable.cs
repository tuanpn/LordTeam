using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActionRunnable : Action {
    public Runnable runnable;

    private bool Ran;
    public ActionRunnable() { }

    public ActionRunnable(Runnable runnable)
    {
        this.runnable = runnable;
    }

    public void setRunnable(Runnable runnable)
    {
        this.runnable = runnable;
    }
    public override bool Act(float delta)
    {
        if (!Ran)
        {
            Ran = true;
            runnable();
        }
        return true;
    }

    public override void restart()
    {
        Ran = false;
    }
}

public delegate void Runnable();
