using UnityEngine;
using System.Collections;
using Dialog;

public class PickPlayButton : InputAdapter {

    public CirclesChoosed circleChoosed;
    public GameObject dialog;

	public void Start () {
        gameObject.AddComponent<Actor>().addAction(new ActionRepeat(ActionRepeat.FOREVER, new ActionSequence(
            new ActionRotateTo(5, 0.1f, Interpolation.sine),
            new ActionRotateTo(-5, 0.1f, Interpolation.sine),
            new ActionRotateTo(5, 0.1f, Interpolation.sine),
            new ActionRotateTo(-5, 0.1f, Interpolation.sine),
            new ActionRotateTo(5, 0.1f, Interpolation.sine),
            new ActionRotateTo(-5, 0.1f, Interpolation.sine),
            new ActionRotateTo(0, 0.1f, Interpolation.sine),
            new ActionDelay(2)
            )));

        dialog = (GameObject)Instantiate(dialog);
        DialogUnity dialogUnity = dialog.GetComponent<DialogUnity>();
        dialogUnity.setText("Do you want to play", "game without skills?");
        dialogUnity.setDialogTwo(
            delegate()
            {
                InputController.Name = InputNames.SHOP;
                for (int i = 0; i < 3; i++)
                    Attr.currentSkills[i] = -1;
                Application.LoadLevel("GameScreen");
            },
            delegate()
            {
                InputController.Name = InputNames.SHOP;
                //Khong lam gi ca
            });
         
	}

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Backspace))
        {
            if (InputController.Name == InputNames.DIALOG)
            {
                if (dialog != null)
                {
                    dialog.GetComponent<DialogUnity>().hideDialog();
                    InputController.Name = InputNames.SHOP;
                }
            }
        }
    }

    public override void OnTouchDown()
    {
        if (InputController.Name != InputNames.SHOP) return;
        base.OnTouchDown();
        gameObject.transform.localScale = new Vector3(0.9f, 0.9f, gameObject.transform.localScale.z);
        SoundManager.playButtonSound();
    }

    public override void OnCheckUp()
    {
        if (InputController.Name != InputNames.SHOP) return;
        base.OnCheckUp();
        gameObject.transform.localScale = new Vector3(1, 1, gameObject.transform.localScale.z);
    }

    public override void OnTouchUp()
    {
        if (InputController.Name != InputNames.SHOP) return;
        base.OnTouchUp();

        int[] skillChoosed = circleChoosed.getSkillChoosed();
        bool hasSkill = false;
        for (int i = 0; i < skillChoosed.Length; i++)
        {
            if (skillChoosed[i] > -1)
            {
                hasSkill = true;
                break;
            }
        }

        Attr.currentSkills = skillChoosed;
        if (hasSkill)
        {
            SoundManager.stopMusic();
            Application.LoadLevel("GameScreen");
        }
        else
        {
            dialog.GetComponent<DialogUnity>().showDialog();
            InputController.Name = InputNames.DIALOG;
        }
    }
}
