using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BottomMenuSwitcher : MonoBehaviour
{
    public GameObject obj1;
    public GameObject obj2;
    public GameObject obj3;

    public Button b1;
    public Button b2;
    public Button b3;

    public void Click(Button sender)
    {
        if (sender == b1)
        {
            obj1.SetActive(true);
            obj2.SetActive(false);
            obj3.SetActive(false);
        }else if(b2 == sender)
        {
            obj1.SetActive(false);
            obj2.SetActive(true);
            obj3.SetActive(false);
        }else if (b3 == sender)
        {
            obj1.SetActive(false);
            obj2.SetActive(false);
            obj3.SetActive(true);
        }
        else
        {
            Debug.LogError("ТЫ как это сделал чёрт?");
        }
    }
}
