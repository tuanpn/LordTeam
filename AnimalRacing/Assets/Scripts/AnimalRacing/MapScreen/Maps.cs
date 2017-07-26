using UnityEngine;
using System.Collections.Generic;

namespace MapScreen
{

    public class Maps : MonoBehaviour
    {

        private float touchX;
        private float deltaX;
        private float offsetX;
        private float rx;

        private bool isUpdate;
        public static bool isDragged;

        private int current;

        public List<Sprite> mapSprites;
        public List<Sprite> lockSprites;
        public GameObject[] mapObjects;
        public GameObject[] dots;

        void Start()
        {
            int world = Data.getData(Data.KEY_WORLD_MAP);

            for (int i = 0; i < 4; i++)
            {
                if (i >= world)
                {
                    mapObjects[i].GetComponent<SpriteRenderer>().sprite = lockSprites[i - 1];
                }
                else
                {
                    mapObjects[i].GetComponent<SpriteRenderer>().sprite = mapSprites[i];
                    mapObjects[i].AddComponent<InputProcessor>();
                    WorldMapClickListener mapClick = mapObjects[i].AddComponent<WorldMapClickListener>();
                    mapClick.mapIndex = i;
                }
            }

            current = world - 1;
            isUpdate = true;
            transform.localPosition = new Vector3(-current * 8, transform.localPosition.y, transform.localPosition.z);

            ARController.setBannerVisible(true);
        }

        void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                touchX = Input.mousePosition.x;
                isUpdate = false;
            }
            else if (Input.GetButton("Fire1"))
            {
                deltaX = Input.mousePosition.x - touchX;
                transform.localPosition += new Vector3(deltaX / 100, transform.localPosition.y, transform.localPosition.z);
                touchX = Input.mousePosition.x;

                current = currentWorldMap(transform.localPosition.x);

                offsetX = -8 * current - transform.localPosition.x;
                if (Mathf.Abs(offsetX) >= 0.3f)
                {
                    isDragged = true;
                }
                UpdateDots();
            }
            else if (Input.GetButtonUp("Fire1"))
            {
                current = currentWorldMap(transform.localPosition.x);

                offsetX = -8 * current - transform.localPosition.x;
                rx = transform.position.x + offsetX;

                isUpdate = true;
            }

            if (isUpdate)
            {
                updateMaps();
            }
        }

        private void updateMaps()
        {
            if (offsetX < 0)
            {
                if (transform.localPosition.x > rx)
                {
                    transform.localPosition += new Vector3(offsetX * 2 * Time.deltaTime, transform.localPosition.y, transform.localPosition.z);
                }
                else
                {
                    transform.localPosition = new Vector3(rx, transform.localPosition.y, transform.localPosition.z);
                    isUpdate = false;
                }
            }
            else
            {
                if (transform.localPosition.x < rx)
                {
                    transform.localPosition += new Vector3(offsetX * 2 * Time.deltaTime, transform.localPosition.y, transform.localPosition.z);
                }
                else
                {
                    transform.localPosition = new Vector3(rx, transform.localPosition.y, transform.localPosition.z);
                    isUpdate = false;
                }
            }
            UpdateDots();
        }

        private void UpdateDots()
        {
            switch (current)
            {
                case 0:
                    dots[4].transform.localPosition = new Vector3(dots[0].transform.localPosition.x, dots[0].transform.localPosition.y, dots[4].transform.localPosition.z);
                    break;
                case 1:
                    dots[4].transform.localPosition = new Vector3(dots[1].transform.localPosition.x, dots[1].transform.localPosition.y, dots[4].transform.localPosition.z);
                    break;
                case 2:
                    dots[4].transform.localPosition = new Vector3(dots[2].transform.localPosition.x, dots[2].transform.localPosition.y, dots[4].transform.localPosition.z);
                    break;
                case 3:
                    dots[4].transform.localPosition = new Vector3(dots[3].transform.localPosition.x, dots[3].transform.localPosition.y, dots[4].transform.localPosition.z);
                    break;
            }
        }

        private int currentWorldMap(float x)
        {
            if (x >= -4)
                return 0;
            else if (x < -4 && x >= -12)
                return 1;
            else if (x < -12 && x >= -20)
                return 2;
            else
                return 3;
        }

        
    }
}