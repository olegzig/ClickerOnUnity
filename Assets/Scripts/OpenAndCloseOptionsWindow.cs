using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenAndCloseOptionsWindow : MonoBehaviour
{
    public GameObject obj;

    public void Close()
    {
        obj.gameObject.SetActive(false);
    }
    public void Open()
    {
        obj.gameObject.SetActive(true);
    }

    //Если дерево растёт, то ты не сможешь открыть какое-либо меню
    public void OpenWhatToPlantMenu()
    {
        if(!Tree_stat.isGrown)
        obj.gameObject.SetActive(true);
    }
}
