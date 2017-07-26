using UnityEngine;
using System.Collections;

namespace GamePlay
{
    public class GuideLayer : MonoBehaviour
    {

        public GameScreen gameScreen { get; set; }

        public void hideGuide()
        {
            gameScreen.showCounter();
            Data.saveData(Data.KEY_GUIDE, 1);   
            Destroy(gameObject);
        }
    }
}