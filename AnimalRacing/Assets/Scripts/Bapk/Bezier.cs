using UnityEngine;
using System.Collections;

public class Bezier : MonoBehaviour
{
    private Vector2[] vects;
    private float stateTime;
    private Vector2 position;
    private float duration;
    private float dDuration;

    private bool isRunning;

    public void Start()
    {
        isRunning = true;
    }

    public void Update()
    {
        if (isRunning)
        {
            position = bezier(Time.deltaTime);
            transform.localPosition = new Vector3(position.x, position.y, transform.localPosition.z);
        }
    }

    //duration default = 1
    public void setBezier(params Vector2[] vects)
    {
        this.vects = vects;
        this.duration = 1;
        this.position = vects[0];
    }

    public void setBezier(float duration, params Vector2[] vects)
    {
        this.vects = vects;
        this.duration = duration == -1 ? 1 : duration;
        this.dDuration = duration;
        this.position = vects[0];
    }

    public Vector2 bezier(float delta)
    {
        stateTime += delta;
        if (stateTime >= duration && dDuration != -1)
        {
            return vects[vects.Length - 1];
        }
        switch (vects.Length)
        {
            case 3:
                position = bezier2(stateTime / duration);
                break;
            case 4:
                position = bezier3(stateTime / duration);
                break;
            default:
                break;
        }
        return position;
    }

    private Vector2 bezier2(float t)
    {
        float x = (1 - t) * (1 - t) * vects[0].x + 2 * t * (1 - t) * vects[1].x + t * t * vects[2].x;
        float y = (1 - t) * (1 - t) * vects[0].y + 2 * t * (1 - t) * vects[1].y + t * t * vects[2].y;
        return new Vector2(x, y);
    }

    private Vector2 bezier3(float t)
    {
        float x = (1 - t) * (1 - t) * (1 - t) * vects[0].x + 3 * t * (1 - t) * (1 - t) * vects[1].x
                + 3 * t * t * (1 - t) * vects[2].x + t * t * t * vects[3].x;
        float y = (1 - t) * (1 - t) * (1 - t) * vects[0].y + 3 * t * (1 - t) * (1 - t) * vects[1].y
                + 3 * t * t * (1 - t) * vects[2].y + t * t * t * vects[3].y;
        return new Vector2(x, y);
    }

    public void setRunning(bool isRunning)
    {
        this.isRunning = isRunning;
    }
}
