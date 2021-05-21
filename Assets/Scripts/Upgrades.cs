using System;
using System.Linq;

using UnityEngine;
using UnityEngine.UI;

public class Upgrades : MonoBehaviour
{
    //����� ������� ������, � ����� ������ �� |ln(x)|. ��������� ����� |ln(x*2)|
    public static int[] treeCostBuf = new int[] { 1, 1, 1, 1, 1, 1, 1 };//��������� ������� ������
    public static int[] growTimeBuf = new int[] { 1, 1, 1, 1, 1, 1, 1 };//����� ����� �����
    public static int[] seedCostBuf = new int[] { 1, 1, 1, 1, 1, 1, 1 };//��������� �������

    public Button[] senders0;//������� ����
    public Button[] senders1;//������ ����
    public Button[] senders2;//������ ����

    public Text[] cost0;
    public Text[] cost1;
    public Text[] cost2;

    public void BuyUpgrade(Button sender)
    {
        Upgrade(sender);
        ShowPrice();
        Save();
    }

    private void ShowPrice()//���������� ������� ���� �����
    {
        for (int i = 0; i < treeCostBuf.Length; i++)
        {
            cost0[i].text = Convert.ToInt32(Math.Abs(Math.Log(treeCostBuf[i] * 2, Math.E))).ToString() + "G";
            cost1[i].text = Convert.ToInt32(Math.Abs(Math.Log(growTimeBuf[i] * 2, Math.E))).ToString() + "G";
            cost2[i].text = Convert.ToInt32(Math.Abs(Math.Log(seedCostBuf[i] * 2, Math.E))).ToString() + "G";
        }
    }

    private void Upgrade(Button sender)//������ ��� ��������� ����
    {
        //������ ����� ������ �������� ������ ���� ����� ������������� �����
        for (int i = 0; i < treeCostBuf.Length; i++)
        {
            if (sender == senders0[i])
            {
                if (GameProgress.gold - Math.Abs(Math.Log(treeCostBuf[i] * 2, Math.E)) >= 0) // ��������� ����� ����� ������� ����� | ln(x * 2) |
                {
                    treeCostBuf[i]++;
                    GameProgress.gold -= Convert.ToInt32(Math.Abs(Math.Log(treeCostBuf[i] * 2, Math.E)));
                }
                return;
            }
        }
        for (int i = 0; i < growTimeBuf.Length; i++)
        {
            if (sender == senders1[i])
            {
                if (GameProgress.gold - Math.Abs(Math.Log(growTimeBuf[i] * 2, Math.E)) >= 0) // ��������� ����� ����� ������� ����� | ln(x * 2) |
                {
                    growTimeBuf[i]++;
                    GameProgress.gold -= Convert.ToInt32(Math.Abs(Math.Log(growTimeBuf[i] * 2, Math.E)));
                }
                return;
            }
        }
        for (int i = 0; i < seedCostBuf.Length; i++)
        {
            if (sender == senders2[i])
            {
                if (GameProgress.gold - Math.Abs(Math.Log(seedCostBuf[i] * 2, Math.E)) >= 0) // ��������� ����� ����� ������� ����� | ln(x * 2) |
                {
                    seedCostBuf[i]++;
                    GameProgress.gold -= Convert.ToInt32(Math.Abs(Math.Log(seedCostBuf[i] * 2, Math.E)));
                }
                return;
            }
        }
    }

    private void Start()
    {
        Load();
        ShowPrice();
    }

    private void Load()
    {
        string txt = PlayerPrefs.GetString("treeCostBuf");
        if (txt != "")
            treeCostBuf = GetIntArrFromFormatString(txt);

        txt = PlayerPrefs.GetString("growTimeBuf");
        if (txt != "")
            growTimeBuf = GetIntArrFromFormatString(txt);

        txt = PlayerPrefs.GetString("seedCostBuf");
        if (txt != "")
            seedCostBuf = GetIntArrFromFormatString(txt);
    }

    private int[] GetIntArrFromFormatString(string txt)//�������� ������ �� ������ ����� ��������
    {
        return txt.Split(' ').Select(Int32.Parse).ToArray();
    }

    private void Save()
    {
        string txt = GetFormatStringFromArr(treeCostBuf);
        PlayerPrefs.SetString("treeCostBuf", txt);

        txt = GetFormatStringFromArr(growTimeBuf);
        PlayerPrefs.SetString("growTimeBuf", txt);

        txt = GetFormatStringFromArr(seedCostBuf);
        PlayerPrefs.SetString("seedCostBuf", txt);

        PlayerPrefs.Save();
    }

    private string GetFormatStringFromArr(int[] arr)//���������� �������������� ������ �� ������� ����� ��� �����
    {
        string txt = null;
        for (int i = 0; i < arr.Length; i++)
        {
            txt += arr[i] + " ";
        }
        txt = txt.Remove(txt.Length - 1);//���������
        return txt;
    }
}