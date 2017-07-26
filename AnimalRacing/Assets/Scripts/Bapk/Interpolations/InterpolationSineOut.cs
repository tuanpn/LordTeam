using UnityEngine;
using System.Collections;

public class InterpolationSineOut : InterpolationProcess {
    public override float apply(float a)
    {
        return Mathf.Sin(a * Mathf.PI / 2);
    }  
}
