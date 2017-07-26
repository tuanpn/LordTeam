using UnityEngine;
using System.Collections;

namespace GamePlay
{
    public class SkillClickListener : InputAdapter
    {
        private bool enabled;
        private Sprite enableSprite;
        private Sprite disableSprite;

        private bool accept;

        public void setDrawables(Sprite enableSprite, Sprite disableSprite)
        {
            this.enableSprite = enableSprite;
            this.disableSprite = disableSprite;
        }

        public void setEnabled(bool enabled)
        {
            this.enabled = enabled;
            gameObject.GetComponent<SpriteRenderer>().sprite = this.enabled ? enableSprite : disableSprite;
        }

        public override void OnTouchDown()
        {
            if (InputController.Name != InputNames.GAMESCREEN) return;
            base.OnTouchDown();
            if (enabled)
            {
                Animal player = gameObject.transform.parent.gameObject.GetComponent<ButtonSkills>().gameScreen.getAnimal(0).GetComponent<Animal>();
                accept = !player.isStanding && !player.IsRevival();
                if (accept)
                {
                    gameObject.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
                    SoundManager.playSound("Sounds/shot");
                }
            }
        }

        public override void OnCheckUp()
        {
            if (InputController.Name != InputNames.GAMESCREEN) return;
            base.OnCheckUp();
            if (enabled && accept)
                gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }

        public override void OnTouchUp()
        {
            if (InputController.Name != InputNames.GAMESCREEN) return;
            base.OnTouchUp();
            if (enabled)
            {
                if (accept)
                {
                    SkillType type = gameObject.GetComponent<SkillRandom>().skillType;
                    GameScreen gameScreen = gameObject.transform.parent.gameObject.GetComponent<ButtonSkills>().gameScreen;
                    gameScreen.addSkill(type, 0);
                    gameScreen.shot(type);
                }
            }
        }
    }
}
