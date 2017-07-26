using UnityEngine;
using System.Collections;

public abstract class Action  {

    protected Actor actor;

    public abstract bool Act(float delta);

    public Actor getActor()
    {
        return actor;
    }

    public virtual void setActor(Actor actor)
    {
        this.actor = actor;
    }

    public virtual void restart()
    {
    }
}
