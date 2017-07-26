using UnityEngine;
using System.Collections;

public class InterpolationPow : InterpolationProcess {

    protected int power;

    public InterpolationPow(int power)
    {
        this.power = power;
    }

    public override float apply(float a)
    {
        if (a <= 0.5f) return Mathf.Pow(a * 2, power) / 2;
        return Mathf.Pow((a - 1) * 2, power) / (power % 2 == 0 ? -2 : 2) + 1;
    }
}
