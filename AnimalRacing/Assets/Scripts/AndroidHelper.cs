using UnityEngine;
using System.Collections;

public class AndroidHelper : MonoBehaviour {

#if UNITY_ANDROID
    private static AndroidJavaClass ajc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
    private static AndroidJavaObject ajo = ajc.GetStatic<AndroidJavaObject>("currentActivity");
#endif

}
