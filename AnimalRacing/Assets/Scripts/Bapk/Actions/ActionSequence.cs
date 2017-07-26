using UnityEngine;
using System.Collections;

public class ActionSequence : ActionParallel{

    private int index;

    public ActionSequence() { }
    public ActionSequence(params Action[] actions) {
        for (int i = 0; i < actions.Length; i++)
        {
            this.myActions.Add(actions[i]);
        }
    }

    public void AddActions(params Action[] actions)
    {
        for (int i = 0; i < actions.Length; i++)
        {
            this.myActions.Add(actions[i]);
        }
    }

    public override bool Act(float delta)
    {
        if (index >= myActions.Count) return true;

        try
        {
            if (myActions[index].Act(delta))
            {
                if (actor == null)  return true;
                index++;
                if (index >= myActions.Count)   return true;
            }
            return false;
        }
        finally { }
    }

    public override void restart()
    {
        base.restart();
        index = 0;
    }
}
