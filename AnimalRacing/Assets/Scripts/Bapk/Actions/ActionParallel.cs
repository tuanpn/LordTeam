using UnityEngine;
using System.Collections.Generic;

public class ActionParallel : Action {

    protected List<Action> myActions = new List<Action>(0);
    private bool complete;

    public ActionParallel() { }
    public ActionParallel(params Action[] actions) {
        for (int i = 0; i < actions.Length; i++)
        {
            this.myActions.Add(actions[i]);
        }
    }

    public override bool Act(float delta)
    {
        if (complete) return true;
        complete = true;
        try
        {
            for (int i = 0, n = myActions.Count; i < n && actor != null; i++)
            {
                if (!myActions[i].Act(delta)) complete = false;
                if (actor == null) return true;
            }
            return complete;
        }
        finally { }
    }

    public void addAction(Action action)
    {
        myActions.Add(action);
        if (actor != null) action.setActor(actor);
    }

    public override void setActor(Actor actor)
    {
        List<Action> actions = this.myActions;
        for (int i = 0, n = myActions.Count; i < n; i++)
            myActions[i].setActor(actor);
        base.setActor(actor);
    }

    public override void restart()
    {
        complete = false;
        List<Action> actions = this.myActions;
        for (int i = 0, n = myActions.Count; i < n; i++)
            myActions[i].restart();
    }
}
