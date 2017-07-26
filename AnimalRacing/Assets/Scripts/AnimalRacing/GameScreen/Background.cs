using UnityEngine;
using System.Collections;

namespace GamePlay
{
    public class Background : MonoBehaviour
    {
        public GameObject bg0;
        public GameObject bg1;
        public GameObject bg2;

        public void Start()
        {
            int world = Attr.currentWorld;

            string[] mapnames = new string[] {"jungle","southpole","desert","volcano"};
            
            int[] heights1 = new int[] {250, 332, 480, 218};
            int[] heights2 = new int[] {125, 134, 304, 57};

            bg0.GetComponent<BG>().setSprite(Resources.Load<Sprite>("Textures/Game/" + mapnames[world] + "/bg"), 0);
            bg1.GetComponent<BG>().setSprite(Resources.Load<Sprite>("Textures/Game/" + mapnames[world] + "/bg1"), -240 + heights1[world]/2);
            bg2.GetComponent<BG>().setSprite(Resources.Load<Sprite>("Textures/Game/" + mapnames[world] + "/bg2"), -240 + heights2[world]/2);
        }

        public void Update()
        {
            gameObject.transform.localPosition = new Vector3(Camera.main.transform.localPosition.x, Camera.main.transform.localPosition.y, gameObject.transform.localPosition.z);
        }

        public void setRuning(bool isRunning)
        {
            bg0.GetComponent<BG>().setRunning(isRunning);
            bg1.GetComponent<BG>().setRunning(isRunning);
            bg2.GetComponent<BG>().setRunning(isRunning);
        }
    }
}