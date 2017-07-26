using UnityEngine;
using System.Collections.Generic;

public class Actor : MonoBehaviour {

    private List<Action> actions = new List<Action>(0);

    private bool isRunning;

	void Start () {
        isRunning = true;
	}

    public void addAction(Action action)
    {
        action.setActor(this);
        actions.Add(action);
    }
	
	void Update () {
        if(isRunning)
            Act(Time.deltaTime);
	}

    public void Act(float delta)
    {
        for (int i = 0; i < actions.Count; i++)
        {
            Action action = actions[i];
            if (action.Act(delta) && i < actions.Count)
            {
                actions.RemoveAt(i);
                action.setActor(null);
                i--;
            }
        }
    }

    public int getActionCounter()
    {
        return actions.Count;
    }

    public void setRunning(bool isRunning)
    {
        this.isRunning = isRunning;
    }
}
