using UnityEngine;
using System.Collections;

public class InterpolationSwingOut : InterpolationProcess
{
    private float scale;

    public InterpolationSwingOut(float scale)
    {
        this.scale = scale;
    }

    public override float apply(float a)
    {
        a--;
        return a * a * ((scale + 1) * a + scale) + 1;
    }
}
