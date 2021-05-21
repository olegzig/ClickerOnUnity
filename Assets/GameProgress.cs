using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameProgress : MonoBehaviour
{
    static public int gold;
    public Text goldText;

    DateTime time;//тут кароче мы будем хранить время (текущее)

    public void Start()
    {
        time = DateTime.Now;//для получения текущего времени
        LoadGame();
    }
    public void Update()
    {
        goldText.text = gold.ToString() + "G";
        time = DateTime.Now;//для обновления времени
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
        #region Отладочка
        Debug.Log("С вход был: " + PlayerPrefs.GetString("lastVisitTime"));//временно
        Debug.Log("Сейчас: "+time);
        Debug.Log("Разница: "+ (time - Convert.ToDateTime(PlayerPrefs.GetString("lastVisitTime"))));
        #endregion Конец отладочки
        time = Convert.ToDateTime(PlayerPrefs.GetString("lastVisitTime"));
        gold = PlayerPrefs.GetInt("gold");
    }
}
