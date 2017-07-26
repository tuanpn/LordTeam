using UnityEngine;
using System.Collections;

public class InterpolationBounceIn : InterpolationBounceOut {
    public InterpolationBounceIn(float[] widths, float[] heights) : base(widths, heights) 
    { }

    public InterpolationBounceIn(int bounces) : base(bounces) { }

    public override float apply(float a)
    {
        return 1 - base.apply(1 - a);
    }
}
