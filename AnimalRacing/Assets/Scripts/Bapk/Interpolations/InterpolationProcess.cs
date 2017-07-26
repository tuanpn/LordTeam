using UnityEngine;
using System.Collections;

public abstract class InterpolationProcess {
    public abstract float apply(float a);

    public float apply(float start, float end, float a)
    {
        return start + (end - start) * apply(a);
    }

    public static InterpolationProcess createInterpolation(Interpolation interpolation)
    {
        InterpolationProcess process = null;
        switch (interpolation)
        {
            case Interpolation.pow2:
                process = new InterpolationPow(2);
                break;
            case Interpolation.pow3:
                process = new InterpolationPow(3);
                break;
            case Interpolation.pow4:
                process = new InterpolationPow(4);
                break;
            case Interpolation.pow5:
                process = new InterpolationPow(5);
                break;
            case Interpolation.powIn2:
                process = new InterpolationPowIn(2);
                break;
            case Interpolation.powIn3:
                process = new InterpolationPowIn(3);
                break;
            case Interpolation.powIn4:
                process = new InterpolationPowIn(4);
                break;
            case Interpolation.powIn5:
                process = new InterpolationPowIn(5);
                break;
            case Interpolation.powOut2:
                process = new InterpolationPowOut(2);
                break;
            case Interpolation.powOut3:
                process = new InterpolationPowOut(3);
                break;
            case Interpolation.powOut4:
                process = new InterpolationPowOut(4);
                break;
            case Interpolation.powOut5:
                process = new InterpolationPowOut(5);
                break;
            case Interpolation.sine:
                process = new InterpolationSine();
                break;
            case Interpolation.sineIn:
                process = new InterpolationSineIn();
                break;
            case Interpolation.sineOut:
                process = new InterpolationSineOut();
                break;
            case Interpolation.exp10:
                process = new InterpolationExp(2, 10);
                break;
            case Interpolation.exp10In:
                process = new InterpolationExpIn(2, 10);
                break;
            case Interpolation.exp10Out:
                process = new InterpolationExpOut(2, 10);
                break;
            case Interpolation.exp5:
                process = new InterpolationExp(2, 5);
                break;
            case Interpolation.exp5In:
                process = new InterpolationExpIn(2, 5);
                break;
            case Interpolation.exp5Out:
                process = new InterpolationExpOut(2, 5);
                break;
            case Interpolation.elastic:
                process = new InterpolationElastic(2, 10);
                break;
            case Interpolation.elasticIn:
                process = new InterpolationElasticIn(2, 10);
                break;
            case Interpolation.elasticOut:
                process = new InterpolationElasticOut(2, 10);
                break;
            case Interpolation.swing:
                process = new InterpolationSwing(1.5f);
                break;
            case Interpolation.swingIn:
                process = new InterpolationSwingIn(2);
                break;
            case Interpolation.swingOut:
                process = new InterpolationSwingOut(2);
                break;
            case Interpolation.bounce:
                process = new InterpolationBounce(4);
                break;
            case Interpolation.bounceIn:
                process = new InterpolationBounceIn(4);
                break;
            case Interpolation.bounceOut:
                process = new InterpolationBounceOut(4);
                break;
            case Interpolation.fade:
                process = new InterpolationFade();
                break;
        }
        return process;
    }
}
