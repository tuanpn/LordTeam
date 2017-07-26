using UnityEngine;
using System.Collections;

public class InterpolationExpOut : InterpolationExp {

    public InterpolationExpOut(float value, float power) : base(value, power)
    {
    }

    public override float apply(float a)
    {
        return 1 - (Mathf.Pow(value, -power * a) - min) * scale;
    }

}
