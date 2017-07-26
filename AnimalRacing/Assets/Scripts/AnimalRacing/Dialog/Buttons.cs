using UnityEngine;
using System.Collections;

namespace Dialog
{
    public class Buttons : MonoBehaviour
    {
        public GameObject buttonOk;
        public GameObject buttonClose;

        public GameObject buttonX;

        public void setButtonNumber(int numberButton)
        {
            if (numberButton == 1)
            {
                buttonOk.SetActive(false);
                buttonClose.SetActive(false);
                buttonX.SetActive(true);
            }
            else if(numberButton == 2)
            {
                buttonOk.SetActive(true);
                buttonClose.SetActive(true);
                buttonX.SetActive(false);
            }
        }
    }
}