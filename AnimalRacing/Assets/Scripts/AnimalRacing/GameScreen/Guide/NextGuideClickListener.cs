using UnityEngine;
using System.Collections;

namespace GamePlay
{
    public class NextGuideClickListener : InputAdapter
    {
        public GameObject guideObject;
        public Sprite[] guides;

        private int guideIndex;

        public void Start()
        {
            gameObject.AddComponent<Actor>().addAction(new ActionRepeat(ActionRepeat.FOREVER, new ActionSequence(
                new ActionScaleTo(0.9f, 0.9f, 0.2f, Interpolation.sine),
                new ActionScaleTo(1, 1, 0.2f, Interpolation.sine)
                )));
            guideIndex = 0;
        }

        public override void OnTouchDown()
        {
            if (InputController.Name != InputNames.DIALOG) return;
            base.OnTouchDown();
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
            SoundManager.playButtonSound();
        }
        public override void OnCheckUp()
        {
            if (InputController.Name != InputNames.DIALOG) return;
            base.OnCheckUp();
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }
        public override void OnTouchUp()
        {
            if (InputController.Name != InputNames.DIALOG) return;
            base.OnTouchUp();
            guideIndex++;
            if (guideIndex == 4)
            {
                gameObject.transform.parent.gameObject.GetComponent<GuideLayer>().hideGuide();
            }
            else
            {
                guideObject.GetComponent<SpriteRenderer>().sprite = guides[guideIndex];
            }
        }
    }
}