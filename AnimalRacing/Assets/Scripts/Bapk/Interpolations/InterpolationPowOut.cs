using UnityEngine;
using System.Collections;

public class InterpolationPowOut : InterpolationPow {
    
    public InterpolationPowOut(int power) : base(power)
    { }

    public float apply(float a)
    {
        return Mathf.Pow(a - 1, power) * (power % 2 == 0 ? -1 : 1) + 1;
    }
}
