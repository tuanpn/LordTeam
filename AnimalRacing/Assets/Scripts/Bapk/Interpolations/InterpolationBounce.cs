using UnityEngine;
using System.Collections;

public class InterpolationBounce : InterpolationBounceOut {
    private float test;

    public InterpolationBounce(float[] widths, float[] heights) : base(widths, heights)
    {
    }
    public InterpolationBounce(int bounces) : base(bounces) { }

    private float Out(float a)
    {
        test = a + widths[0] / 2;
        if (test < widths[0]) 
            return test / (widths[0] / 2) - 1;
        return base.apply(a);
    }

    public override float apply(float a)
    {
        if (a <= 0.5f) 
            return (1 - Out(1 - a * 2)) / 2;
        return Out(a * 2 - 1) / 2 + 0.5f;
    }

}
