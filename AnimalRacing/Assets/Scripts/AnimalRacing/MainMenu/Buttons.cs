using UnityEngine;
using System.Collections.Generic;

namespace MainMenu
{

    public class Buttons : MonoBehaviour
    {

        public List<GameObject> buttons;

        public void Start()
        {
            float[] btX = new float[] { 3.3f, 3.55f};
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].GetComponent<Actor>().addAction(new ActionMoveTo(btX[i], buttons[i].gameObject.transform.localPosition.y, 0.8f + 0.2f * i, Interpolation.swingOut));
                if (i == 0)
                {
                    buttons[i].GetComponent<Actor>().addAction(new ActionRepeat(ActionRepeat.FOREVER, new ActionSequence(new ActionScaleTo(0.9f, 0.9f, 0.1f), new ActionScaleTo(1, 1, 0.1f))));
                }
            }
        }


    }
}