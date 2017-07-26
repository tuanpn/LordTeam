using UnityEngine;
using System.Collections;

namespace GamePlay
{
    public class PauseLayer : MonoBehaviour
    {
        public SpriteRenderer musicSprite;
        public SpriteRenderer soundSprite;

        public Sprite[] onSprites;
        public Sprite[] offSprites;

        public void Start()
        {
            musicSprite.sprite =SoundManager.isMusic ? onSprites[0] : offSprites[0];
            soundSprite.sprite = SoundManager.isSound ? onSprites[1] : offSprites[1];
        }

        public void changeSprite(int item, bool isOn)
        {
            if (item == 0) musicSprite.sprite = isOn ? onSprites[0] : offSprites[0];
            else if (item == 1) soundSprite.sprite = isOn ? onSprites[1] : offSprites[1];
        }
    }
}