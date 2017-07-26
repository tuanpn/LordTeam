using UnityEngine;
using System.Collections;

public class InterpolationPowIn : InterpolationPow{
   // private int power;

    public InterpolationPowIn(int power) : base(power)
    {
        //this.power = power;
    }

    public float apply(float a)
    {
        return Mathf.Pow(a, power);
    }
}
