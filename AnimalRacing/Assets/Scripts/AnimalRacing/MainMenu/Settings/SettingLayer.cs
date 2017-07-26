using UnityEngine;
using System.Collections;

namespace Settings
{
    public class SettingLayer : MonoBehaviour
    {
        public GameObject blackScreen;
        public GameObject infoLayer;
        public GameObject setLayer;

        public void Start()
        {
            
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Backspace))
            {
                if (InputController.Name == InputNames.DIALOG)
                {
                    if (setLayer.activeSelf)
                    {
                        setVisibled(false, false);
                        InputController.Name = InputNames.MAINMENU;
                    }
                    else
                    {
                        setLayer.SetActive(true);
                        infoLayer.SetActive(false);
                    }
                }
            }
        }

        public void setVisibled(bool isVisibled, bool bsVisible)
        {
            if (isVisibled)
            {
                blackScreen.SetActive(bsVisible);
                gameObject.AddComponent<Actor>().addAction(new ActionMoveTo(0, 0, 0.5f, Interpolation.swingOut));
                gameObject.SetActive(true);
            }
            else
            {
                blackScreen.SetActive(bsVisible);
                if (gameObject.GetComponent<Actor>() != null)
                    Destroy(gameObject.GetComponent<Actor>());
                transform.localPosition = new Vector3(transform.localPosition.x, 4.8f, transform.localPosition.z);
                gameObject.SetActive(false);
            }
        }

    }
}
