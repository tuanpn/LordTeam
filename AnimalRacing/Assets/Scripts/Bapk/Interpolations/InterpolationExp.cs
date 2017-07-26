using UnityEngine;
using System.Collections;

public class InterpolationExp : InterpolationProcess {

    protected float value, power, min, scale;

    public InterpolationExp(float value, float power)
    {
        this.value = value;
        this.power = power;
        min = Mathf.Pow(value, -power);
        scale = 1 / (1 - min);
    }

    public override float apply(float a)
    {
        if (a <= 0.5f) return (Mathf.Pow(value, power * (a * 2 - 1) - min)) * scale / 2;
        return (2 - (Mathf.Pow(value, -power * (a * 2 - 1)) - min) * scale)/2;
    }
}
