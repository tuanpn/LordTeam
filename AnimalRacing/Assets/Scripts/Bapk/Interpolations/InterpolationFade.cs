using UnityEngine;
using System.Collections;

public class InterpolationFade : InterpolationProcess {
    public override float apply(float a)
    {
        return clamp(a * a * a * (a * (a * 6 - 15) + 10), 0, 1);
    }

    private float clamp(float value, float min, float max)
    {
        if (value < min) return min;
        if (value > max) return max;
        return value;
    }
}
