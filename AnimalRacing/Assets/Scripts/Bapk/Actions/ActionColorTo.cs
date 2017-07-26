using UnityEngine;
using System.Collections;

public class ActionColorTo : TemporalAction {

    private float endRed;
    private float endGreen;
    private float endBlue;
    private float endAlpha;

    private float startRed;
    private float startGreen;
    private float startBlue;
    private float startAlpha;

    public ActionColorTo(Color color, float duration, Interpolation interpolation) {
        endRed = color.r;
        endGreen = color.g;
        endBlue = color.b;
        endAlpha = color.a;
        SetDuration(duration);
        SetInterpolation(interpolation);
    }
    public ActionColorTo(float r, float g, float b, float duration, Interpolation interpolation) {
        endRed = r;
        endGreen = g;
        endBlue = b;
        endAlpha = 1;
        SetDuration(duration);
        SetInterpolation(interpolation);
    }
    public ActionColorTo(float r, float g, float b, float a, float duration, Interpolation interpolation) {
        endRed = r;
        endGreen = g;
        endBlue = b;
        endAlpha = a;
        SetDuration(duration);
        SetInterpolation(interpolation);
    }
    public ActionColorTo(Color color, float duration)
    {
        endRed = color.r;
        endGreen = color.g;
        endBlue = color.b;
        endAlpha = color.a;
        SetDuration(duration);
    }
    public ActionColorTo(float r, float g, float b, float duration)
    {
        endRed = r;
        endGreen = g;
        endBlue = b;
        endAlpha = 1;
        SetDuration(duration);
    }
    public ActionColorTo(float r, float g, float b, float a, float duration)
    {
        endRed = r;
        endGreen = g;
        endBlue = b;
        endAlpha = a;
        SetDuration(duration);
    }

    protected override void begin()
    {
        Color color = actor.gameObject.GetComponent<SpriteRenderer>().color;
        startRed = color.r;
        startGreen = color.g;
        startBlue = color.b;
        startAlpha = color.a;
    }

    protected override void end()
    {
    }

    protected override void UpdateAction(float percent)
    {
        actor.gameObject.GetComponent<SpriteRenderer>().color = new Color(
            calculate(startRed, endRed, percent),
            calculate(startGreen, endGreen, percent),
            calculate(startBlue, endBlue, percent),
            calculate(startAlpha, endAlpha, percent));
    }

    private float calculate(float start_value, float end_value, float percent)
    {
        return start_value + (end_value - start_value) * percent;
    }
}
