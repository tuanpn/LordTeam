using UnityEngine;
using System.Collections;

public class InterpolationElasticIn : InterpolationElastic {
    public InterpolationElasticIn(float value, float power) : base(value, power)
    {   
    }

    public float apply(float a)
    {
        return Mathf.Pow(value, power * (a - 1)) * Mathf.Sin(a * 20) * 1.0955f;
    }
}
