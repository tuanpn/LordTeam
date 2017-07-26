using UnityEngine;
using System.Collections;

public abstract class DelegateAction : Action {

    protected Action action;

    public void setAction(Action action)
    {
        this.action = action;
    }

    protected abstract bool UpdateDelegate (float delta);

    public override bool Act(float delta)
    {
        try
        {
            return UpdateDelegate(delta);
        }
        finally { }
    }

    public override void setActor(Actor actor)
    {
        if (action != null) action.setActor(actor);
        base.setActor(actor);
    }
}
