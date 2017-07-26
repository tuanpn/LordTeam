using UnityEngine;
using System.Collections;

namespace Dialog
{
    public class Button : InputAdapter
    {
        public int buttonIndex;

        private DialogButton dialogButton;

        public void addClickListener(DialogButton dialogButton)
        {
            this.dialogButton = dialogButton;
        }

        public override void OnTouchDown()
        {
            if (InputController.Name != InputNames.DIALOG) return;
            base.OnTouchDown();
            transform.localScale = new Vector3(0.9f, 0.9f, transform.localScale.z);
            SoundManager.playButtonSound();
        }
        public override void OnCheckUp()
        {
            if (InputController.Name != InputNames.DIALOG) return;
            base.OnCheckUp();

            transform.localScale = new Vector3(1, 1, transform.localScale.z);
        }
        public override void OnTouchUp()
        {
            if (InputController.Name != InputNames.DIALOG) return;
            base.OnTouchUp();
            dialogButton();
            gameObject.transform.parent.parent.gameObject.GetComponent<DialogUnity>().hideDialog();
        }
    }
}