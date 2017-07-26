using UnityEngine;
using System.Collections;

public class InterpolationExpIn : InterpolationExp {

    public InterpolationExpIn(float value, float power) : base(value, power)
    {
    }

    public override float apply(float a)
    {
        return (Mathf.Pow(value, power * (a - 1)) - min) * scale;
    }
}
