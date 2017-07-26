using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
namespace MapScreen
{
    public class BackButton : InputAdapter
    {
        public override void OnTouchDown()
        {
            base.OnTouchDown();
            transform.localScale = new Vector3(0.9f, 0.9f, transform.localScale.z);
            SoundManager.playButtonSound();   
        }

        public override void OnCheckUp()
        {
            base.OnCheckUp();
            transform.localScale = new Vector3(1, 1, transform.localScale.z);
        }

        public override void OnTouchUp()
        {
            base.OnTouchUp();
            SceneManager.LoadScene("MainMenu");
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Backspace))
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
    }

}