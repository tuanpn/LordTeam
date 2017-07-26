using UnityEngine;
using System.Collections;

public class InterpolationElasticOut : InterpolationElastic {
    public InterpolationElasticOut(float value, float power) : base(value, power) {}

    public override float apply(float a)
    {
        a = 1 - a;
        return (1 - Mathf.Pow(value, power * (a - 1)) * Mathf.Sin(a * 20) * 1.0955f);
    }
}
