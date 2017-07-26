using UnityEngine;
using System.Collections;

public class InterpolationSwing : InterpolationProcess {
    private float scale;

    public InterpolationSwing(float scale)
    {
        this.scale = scale;
    }

    public override float apply(float a)
    {
        if (a <= 0.5f)
        {
            a *= 2;
            return a * a * ((scale + 1) * a - scale) / 2;
        }
        a--;
        a *= 2;
        return a * a * ((scale + 1) * a + scale) / 2 + 1;
    }
}
