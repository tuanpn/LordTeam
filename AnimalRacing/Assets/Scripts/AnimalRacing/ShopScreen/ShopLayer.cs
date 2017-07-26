using UnityEngine;
using System.Collections;

public class ShopLayer : MonoBehaviour {

	public void Start () {
        gameObject.AddComponent<Actor>().addAction(new ActionSequence(
            new ActionScaleTo(0, 0, 0),
            new ActionScaleTo(1, 1, 0.5f, Interpolation.swingOut)
            ));
	}
}
