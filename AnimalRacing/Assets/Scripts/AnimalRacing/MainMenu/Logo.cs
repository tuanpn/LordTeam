using UnityEngine;
using System.Collections;

namespace MainMenu
{
    public class Logo : MonoBehaviour
    {
        public void Start()
        {
            gameObject.AddComponent<Actor>().addAction(new ActionSequence(new ActionDelay(1.8f), new ActionScaleTo(1, 1, 0.5f, Interpolation.swingOut)));
        }
    }
}
