using UnityEngine;
using System.Collections;

public class InterpolationSine : InterpolationProcess {
    public override float apply(float a)
    {
        return (1 - Mathf.Cos(a * Mathf.PI )) / 2;
    }  
}
