using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
namespace MainMenu
{
    public class TouchText : MonoBehaviour
    {
        public GameObject text;

        public void Start()
        {
            Data.InitData();
            InputController.Enabled = false;

            gameObject.GetComponent<Actor>().addAction(new ActionSequence(
                new ActionDelay(2),
                new ActionMoveTo(0, -1.85f, 0.1f, Interpolation.sine),
                new ActionRunnable(delegate() {  enableInput(); })
                ));
            
            text.GetComponent<Actor>().addAction(new ActionSequence(new ActionDelay(2.1f), 
                new ActionRepeat(ActionRepeat.FOREVER, new ActionSequence(
                    new ActionColorTo(1, 1, 1, 1, 0.1f), new ActionColorTo(1, 1, 1, 0, 0.1f) 
                ))));
        }

        public void enableInput()
        {
            InputController.Enabled = true;
            InputController.Name = InputNames.MAINMENU;
        }

        public void LateUpdate()
        {
            if (InputController.Enabled && InputController.Name == InputNames.MAINMENU)
            {
                if (Input.GetButtonDown("Fire1") && InputController.IsScreen)
                {
                    SceneManager.LoadScene("MapScreen");
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    InputController.IsScreen = true;
                }
            }
        }
    }
}
