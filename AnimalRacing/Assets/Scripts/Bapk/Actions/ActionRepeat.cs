using UnityEngine;
using System.Collections;

public class ActionRepeat : DelegateAction {

    public const int FOREVER = -1;

    private int repeatCount, executedCount;
    private bool finished;

    public ActionRepeat() { }

    public ActionRepeat(int repeatCount, Action repeateAction){
        this.repeatCount = repeatCount;
        this.action = repeateAction;
    }

    protected override bool UpdateDelegate(float delta)
    {
        if (executedCount == repeatCount) return true;
        if (action.Act(delta))
        {
            if (finished) return true;
            if (repeatCount > 0) executedCount++;
            if (executedCount == repeatCount) return true;
            if (action != null) action.restart();
        }
        return false;
    }

    public void finish() 
    {
        finished = true;
    }

    public override void restart()
    {
        base.restart();
        executedCount = 0;
        finished = false;
    }

    public void setCount(int count)
    {
        this.repeatCount = count;
    }

    public int getCount()
    {
        return this.repeatCount;
    }
}
