using UnityEngine;
using System.Collections;

namespace MapScreen
{

    public class WorldMapClickListener : InputAdapter
    {
        public int mapIndex;
        public override void OnTouchDown()
        {
            base.OnTouchDown();
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
            SoundManager.playButtonSound();
        }
        public override void OnCheckUp()
        {
            base.OnCheckUp();
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }
        public override void OnTouchUp()
        {
            base.OnTouchUp();
            if (!Maps.isDragged)
            {
                Attr.currentWorld = mapIndex;
                Scenes.Load(Scenes.LEVEL);
            }
            Maps.isDragged = false;
        }
    }
}
