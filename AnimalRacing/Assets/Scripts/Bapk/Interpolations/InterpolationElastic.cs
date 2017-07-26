using UnityEngine;
using System.Collections;

public class InterpolationElastic : InterpolationProcess {
    protected float value, power;

    public InterpolationElastic(float value, float power)
    {
        this.value = value;
        this.power = power;
    }

    public override float apply(float a)
    {
        if (a <= 0.5f)
        {
            a *= 2;
            return Mathf.Pow(value, power * (a - 1)) * Mathf.Sin(a * 20) * 1.0955f / 2;
        }
        a = 1 - a;
        a *= 2;
        return 1 - Mathf.Pow(value, power * (a - 1)) * Mathf.Sin(a * 20) * 1.0955f / 2;
    }
}
