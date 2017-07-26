using UnityEngine;
using System.Collections;

namespace GamePlay
{

    public class LevelText : MonoBehaviour
    {
        void Start()
        {
            BitmapFont fontResult = new BitmapFont("Fonts/font_result", "Fonts/font_result_xml", gameObject);
            fontResult.setText("Level " + ((Attr.currentWorld * 15) + Attr.currentLevel + 1), -1, 15, "GUI", "GUI");
        }
    }
}
