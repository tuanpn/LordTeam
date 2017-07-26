using UnityEngine;
using System.Collections.Generic;

namespace Helper
{
    public class ScrollHelp : MonoBehaviour
    {
        public List<Texture2D> textureHelps;

        private int Current;

        private Rect rect1;
        private Rect rect2;
        private Rect rect3;
        private Rect rect4;

        private Rect bounds;

        private float touchX;
        private float deltaX;
        private float offsetX;
        private bool isUpdate;
        private float step;
        private float statetime;
        private float[] rxs;

        public GameObject dot0;
        public GameObject dot1;
        public GameObject dot2;
        public GameObject dot3;
        public GameObject bigDot;

        public void Start()
        {
            rect1 = new Rect(0, 0, 538, 322);
            rect2 = new Rect(538 + 10, 0, 538, 322);
            rect3 = new Rect(538 * 2 + 20, 0, 538, 322);
            rect4 = new Rect(538 * 3 + 30, 0, 538, 322);
            bounds = new Rect(128, 68, 538, 322);
            rxs = new float[] { 0, 0, 0, 0 };
        }


        public void OnGUI()
        {
            GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(Screen.width / 800f, Screen.height / 480f, 1f));
            GUI.BeginGroup(bounds);

            // scrollPosition = GUI.BeginScrollView(new Rect(0, 0, 538, 350), scrollPosition, new Rect(0, 0, 538 * 4 + 30, 330));

            GUILayout.BeginHorizontal(GUILayout.Width(538), GUILayout.Height(322));

            GUI.DrawTexture(rect1, textureHelps[0]);
            GUI.DrawTexture(rect2, textureHelps[1]);
            GUI.DrawTexture(rect3, textureHelps[2]);
            GUI.DrawTexture(rect4, textureHelps[3]);

            GUILayout.EndHorizontal();

            //   GUI.EndScrollView();

            GUI.EndGroup();
        }

        public void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                touchX = Input.mousePosition.x;
                isUpdate = false;
                updateDots();
            }
            else if (Input.GetButton("Fire1"))//su kien giu chuot
            {
                deltaX = Input.mousePosition.x - touchX;

                rect1.x += deltaX;
                rect2.x += deltaX;
                rect3.x += deltaX;
                rect4.x += deltaX;

                touchX = Input.mousePosition.x;

            }
            else if (Input.GetButtonUp("Fire1"))
            {
                Current = currentBg(rect1.x);
                offsetX = -538 * Current - 10 * Current - rect1.x;
                step = offsetX * 5;
                isUpdate = true;
                rxs[0] = rect1.x + offsetX;
                rxs[1] = rect2.x + offsetX;
                rxs[2] = rect3.x + offsetX;
                rxs[3] = rect4.x + offsetX;
            }


            if (isUpdate)
            {
                if (offsetX < 0)
                {
                    if (rect1.x > rxs[0])
                    {
                        rect1.x += step * Time.deltaTime;
                        rect2.x += step * Time.deltaTime;
                        rect3.x += step * Time.deltaTime;
                        rect4.x += step * Time.deltaTime;
                    }
                    else
                    {
                        rect1.x = rxs[0];
                        rect2.x = rxs[1];
                        rect3.x = rxs[2];
                        rect4.x = rxs[3];
                        isUpdate = false;
                        updateDots();
                    }
                }
                else
                {
                    if (rect1.x < rxs[0])
                    {
                        rect1.x += step * Time.deltaTime;
                        rect2.x += step * Time.deltaTime;
                        rect3.x += step * Time.deltaTime;
                        rect4.x += step * Time.deltaTime;
                    }
                    else
                    {
                        rect1.x = rxs[0];
                        rect2.x = rxs[1];
                        rect3.x = rxs[2];
                        rect4.x = rxs[3];
                        isUpdate = false;
                        updateDots();
                    }
                }
            }
        }

        private int currentBg(float rect1X)
        {
            if (rect1X >= -269)
                return 0;
            else if (rect1X < -269 && rect1X >= -817)
                return 1;
            else if (rect1X < -817 && rect1X >= -1355)
                return 2;
            else
                return 3;
        }

        private void updateDots()
        {
            switch (Current)
            {
                case 0:
                    bigDot.transform.position = new Vector3(dot0.transform.position.x, dot0.transform.position.y, bigDot.transform.position.z);
                    break;
                case 1:
                    bigDot.transform.position = new Vector3(dot1.transform.position.x, dot1.transform.position.y, bigDot.transform.position.z);
                    break;
                case 2:
                    bigDot.transform.position = new Vector3(dot2.transform.position.x, dot2.transform.position.y, bigDot.transform.position.z);
                    break;
                case 3:
                    bigDot.transform.position = new Vector3(dot3.transform.position.x, dot3.transform.position.y, bigDot.transform.position.z);
                    break;
            }
        }
    }
}
