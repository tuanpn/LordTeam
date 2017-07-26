using UnityEngine;
using System.Collections;

public class ARController {

    public static IContact iContact;

    public static void setBannerVisible(bool isVisible)
    {
        if (iContact != null)
            iContact.setBannerVisible(isVisible);
    }

    public static void showInterstitialAd()
    {
        if (iContact != null)
            iContact.showInterstitialAd();
    }
}
