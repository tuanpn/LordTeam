using UnityEngine;
using System.Collections;

namespace MainMenu
{
    public class MainMenuAnimals : MonoBehaviour
    {

        public GameObject[] animals;

        public void Start()
        {
            for (int i = 0; i < 3; i++)
                animals[i].GetComponent<Actor>().addAction(new ActionSequence(new ActionDelay(1.6f), new ActionScaleTo(1, 1, 0.2f, Interpolation.sine)));

            gameObject.AddComponent<Actor>().addAction(new ActionSequence(new ActionDelay(1.4f), new ActionRunnable(
                delegate() {
                    SoundManager.LoadBgMusic("Sounds/menu", false);
                    SoundManager.playSound("Sounds/roga");
                }
                )));

            float[] aY = new float[] { 0.01f, 0.01f, -0.02f };
            float[] ds = new float[] { 0.2f, 0.4f, 0.3f };
            for (int i = 0; i < 3; i++)
            {
                animals[i].GetComponent<Actor>().addAction(new ActionRepeat(ActionRepeat.FOREVER, new ActionSequence(
                    new ActionMoveTo(animals[i].transform.localPosition.x, aY[i] + 0.1f, 1 + ds[i], Interpolation.sine),
                    new ActionMoveTo(animals[i].transform.localPosition.x, aY[i], 1 + ds[i], Interpolation.sine)
                )));
            }
        }
    }
}