using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameProgress : MonoBehaviour
{
    static public int gold;
    public Text goldText;

    DateTime time;//��� ������ �� ����� ������� ����� (�������)

    public void Start()
    {
        time = DateTime.Now;//��� ��������� �������� �������
        LoadGame();
    }
    public void Update()
    {
        goldText.text = gold.ToString() + "G";
        time = DateTime.Now;//��� ���������� �������
        SaveGame();
    }
    void SaveGame()
    {
        PlayerPrefs.SetString("lastVisitTime",time.ToString());
        PlayerPrefs.SetInt("gold", gold);
        PlayerPrefs.Save();
    }
    void LoadGame()
    {
        #region ���������
        Debug.Log("� ���� ���: " + PlayerPrefs.GetString("lastVisitTime"));//��������
        Debug.Log("������: "+time);
        Debug.Log("�������: "+ (time - Convert.ToDateTime(PlayerPrefs.GetString("lastVisitTime"))));
        #endregion ����� ���������
        time = Convert.ToDateTime(PlayerPrefs.GetString("lastVisitTime"));
        gold = PlayerPrefs.GetInt("gold");
    }
}
