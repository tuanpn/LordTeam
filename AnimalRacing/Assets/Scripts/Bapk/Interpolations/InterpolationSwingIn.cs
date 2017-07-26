using UnityEngine;
using System.Collections;

public class InterpolationSwingIn : InterpolationProcess
{
    private float scale;

    public InterpolationSwingIn(float scale)
    {
        this.scale = scale;
    }

    public override float apply(float a)
    {
        return a * a * ((scale + 1) * a - scale);
    }

}
