using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.UI;

//нужно сделать так, чтобы небыло приколов с двойной посадкой

public class Tree_stat : MonoBehaviour
{
    public Text howManyToGrowthText;
    public GameObject howManyToGrowthObject;

    public Image[] treeImage;
    public Image plantedTree;
    public Image defaultTree;

    public enum Trees
    {
        вишня, пальма, яблоня, дуб, ива, ясень, липа, NaN
    };

    private int[] howManyToPayForPlant = new int[] { 0, 2, 4, 6, 8, 10, 12 };//тут мы определяем сколько за ПоСаДоЧкУ платим по дефолту
    public Text[] howManyPayForPlantText;

    private Trees whatGrow;

    public Button[] senders;

    private List<TimeSpan> timeOfGrowth;
    private DateTime whenGrowed;
    public static bool isGrown;

    private void Start()
    {
        #region заполняем время роста деревьев

        timeOfGrowth = new List<TimeSpan>
        {
            new TimeSpan(0, 0, 30),//0 = вишня
            new TimeSpan(0, 5, 0),//1 = пальма
            new TimeSpan(0, 10, 0),//2 = яблоня
            new TimeSpan(0, 15, 0),//3 = дуб
            new TimeSpan(0, 20, 0),//4 = ива
            new TimeSpan(0, 25, 0),//5 = ясень
            new TimeSpan(0, 30, 0)//6 = липа
        };

        #endregion заполняем время роста деревьев

        isGrown = Convert.ToBoolean(PlayerPrefs.GetString("isGrown"));
        whenGrowed = Convert.ToDateTime(PlayerPrefs.GetString("whenGrow"));
        LoadWhatGrowing();
        LoadPlantCost();
    }
    
    private void LoadWhatGrowing()//получаем что растёт и грузим пикчу этого вместо дефолтной
    {
        string whatGrowText = PlayerPrefs.GetString("whatGrow");
        switch (whatGrowText)
        {
            case "вишня":
                whatGrow = Trees.вишня;
                plantedTree.sprite = treeImage[0].sprite;
                break;

            case "пальма":
                whatGrow = Trees.пальма;
                plantedTree.sprite = treeImage[1].sprite;
                break;

            case "яблоня":
                whatGrow = Trees.яблоня;
                plantedTree.sprite = treeImage[2].sprite;
                break;

            case "дуб":
                whatGrow = Trees.дуб;
                plantedTree.sprite = treeImage[3].sprite;
                break;

            case "ива":
                whatGrow = Trees.ива;
                plantedTree.sprite = treeImage[4].sprite;
                break;

            case "ясень":
                whatGrow = Trees.ясень;
                plantedTree.sprite = treeImage[5].sprite;
                break;

            case "липа":
                whatGrow = Trees.липа;
                plantedTree.sprite = treeImage[6].sprite;
                break;
            default:
                plantedTree.sprite = defaultTree.sprite;
                break;
        }
    }
    private void LoadPlantCost()//показывает ценники на плент
    {
        //сплитим в сколько стоит

        string txt = null;
        txt = PlayerPrefs.GetString("howManyToPayForPlant");
        if (txt != "")
        {
            howManyToPayForPlant = txt.Split(' ').Select(Int32.Parse).ToArray();
        }

        //отображаем сколько стоит
        for (int i = 0; i < howManyPayForPlantText.Length; i++)
        {
            howManyPayForPlantText[i].text = howManyToPayForPlant[i].ToString() + "G";
        }
    }
    private TimeSpan GetTimeOfGrowth(Trees tree)//возвращает время роста Trees tree
    {
        switch (tree)
        {
            case Trees.вишня:
                return TimeSpan.FromTicks(timeOfGrowth[0].Ticks / Upgrades.growTimeBuf[0]);

            case Trees.дуб:
                return TimeSpan.FromTicks(timeOfGrowth[1].Ticks / Upgrades.growTimeBuf[1]);

            case Trees.ива:
                return TimeSpan.FromTicks(timeOfGrowth[2].Ticks / Upgrades.growTimeBuf[2]);

            case Trees.липа:
                return TimeSpan.FromTicks(timeOfGrowth[3].Ticks / Upgrades.growTimeBuf[3]);

            case Trees.пальма:
                return TimeSpan.FromTicks(timeOfGrowth[4].Ticks / Upgrades.growTimeBuf[4]);

            case Trees.яблоня:
                return TimeSpan.FromTicks(timeOfGrowth[5].Ticks / Upgrades.growTimeBuf[5]);

            case Trees.ясень:
                return TimeSpan.FromTicks(timeOfGrowth[6].Ticks / Upgrades.growTimeBuf[6]);

            default:
                Debug.LogError("Что ты сюда шлёшь И-Д-И-О-Т!?");
                return new TimeSpan(0, 0, 0);
        }
    }

    private TimeSpan UpdateGrowTime(Trees tree)
    {
        TimeSpan time = GetTimeOfGrowth(tree);
        howManyToGrowthText.text = time.ToString();//выводим время роста

        isGrown = true;//говорим что дерево растёт
        whatGrow = tree;//запоминаем что растёт
        return time;
    }

    //сравнивать время "когда вырастет" с текущим, и отнимать время "когда вырастет" от текущего чтобы быть в курсе сколько осталось
    private void Update()
    {
        //тут нужно менять
        TimeSpan time = whenGrowed.Subtract(DateTime.Now);
        howManyToGrowthText.text = string.Format("{0:D2}:{1:D2}:{2:D2}", time.Hours, time.Minutes, time.Seconds);

        //эта тема затевалась для того, чтобы скрывать текст времени и выдавать награду за рост
        if (time <= TimeSpan.Zero && isGrown)
        {
            isGrown = false;
            Debug.Log("Дерево должно было вырасти");

            #region тут мы даём деньги за выросшее дерево

            switch (whatGrow)
            {
                case Trees.вишня:
                    GameProgress.gold += Convert.ToInt32(Math.Log(Upgrades.treeCostBuf[0]+.2)*10);
                    break;

                case Trees.дуб:
                    GameProgress.gold += Convert.ToInt32(Math.Log(Upgrades.treeCostBuf[1] + .2) * 15);
                    break;

                case Trees.ива:
                    GameProgress.gold += Convert.ToInt32(Math.Log(Upgrades.treeCostBuf[2] + .2) * 20);
                    break;

                case Trees.липа:
                    GameProgress.gold += Convert.ToInt32(Math.Log(Upgrades.treeCostBuf[3] + .2) * 25);
                    break;

                case Trees.пальма:
                    GameProgress.gold += Convert.ToInt32(Math.Log(Upgrades.treeCostBuf[4] + .2) * 30);
                    break;

                case Trees.яблоня:
                    GameProgress.gold += Convert.ToInt32(Math.Log(Upgrades.treeCostBuf[5] + .2) * 35);
                    break;

                case Trees.ясень:
                    GameProgress.gold += Convert.ToInt32(Math.Log(Upgrades.treeCostBuf[6] + .2) * 40);
                    break;

                default:
                    Debug.LogError("Ты как это сделал чертила?");
                    break;
            }

            #endregion тут мы даём деньги за выросшее дерево

            plantedTree.sprite = defaultTree.sprite;
            whatGrow = Trees.NaN;//может всё крашнуть
            howManyToGrowthObject.SetActive(false);
        }
        if (time > TimeSpan.Zero)
        {
            howManyToGrowthObject.SetActive(true);
        }
        Save();
    }

    public void PlantTree(Button sender)
    {
        #region тут мы получаем время когда вырастет определённое дерево (when growed) и клеим пикчу этого дерева на img. А так же снимаем деньги

        if (sender == senders[0] && GameProgress.gold - Math.Abs(howManyToPayForPlant[0] / Upgrades.seedCostBuf[0]) >= 0)
        {
            whenGrowed = DateTime.Now.Add(UpdateGrowTime(Trees.вишня));
            GameProgress.gold -= Math.Abs(howManyToPayForPlant[0] / Upgrades.seedCostBuf[0]);
            plantedTree.sprite = treeImage[0].sprite;
        }
        else if (sender == senders[1] && GameProgress.gold - Math.Abs(howManyToPayForPlant[1] / Upgrades.seedCostBuf[1]) >=0)
        {
            whenGrowed = DateTime.Now.Add(UpdateGrowTime(Trees.пальма));
            GameProgress.gold -= Math.Abs(howManyToPayForPlant[1] / Upgrades.seedCostBuf[1]);
            plantedTree.sprite = treeImage[1].sprite;
        }
        else if (sender == senders[2] && GameProgress.gold - Math.Abs(howManyToPayForPlant[2] / Upgrades.seedCostBuf[2]) >= 0)
        {
            whenGrowed = DateTime.Now.Add(UpdateGrowTime(Trees.яблоня));
            GameProgress.gold -= Math.Abs(howManyToPayForPlant[2] / Upgrades.seedCostBuf[2]);
            plantedTree.sprite = treeImage[2].sprite;
        }
        else if (sender == senders[3] && GameProgress.gold - Math.Abs(howManyToPayForPlant[3] / Upgrades.seedCostBuf[3]) >= 0)
        {
            whenGrowed = DateTime.Now.Add(UpdateGrowTime(Trees.дуб));
            GameProgress.gold -= Math.Abs(howManyToPayForPlant[3] / Upgrades.seedCostBuf[3]);
            plantedTree.sprite = treeImage[3].sprite;
        }
        else if (sender == senders[4] && GameProgress.gold - Math.Abs(howManyToPayForPlant[4] / Upgrades.seedCostBuf[4]) >= 0)
        {
            whenGrowed = DateTime.Now.Add(UpdateGrowTime(Trees.ясень));
            GameProgress.gold -= Math.Abs(howManyToPayForPlant[4] / Upgrades.seedCostBuf[4]);
            plantedTree.sprite = treeImage[4].sprite;
        }
        else if (sender == senders[5] && GameProgress.gold - Math.Abs(howManyToPayForPlant[5] / Upgrades.seedCostBuf[5]) >= 0)
        {
            whenGrowed = DateTime.Now.Add(UpdateGrowTime(Trees.липа));
            GameProgress.gold -= Math.Abs(howManyToPayForPlant[5] / Upgrades.seedCostBuf[5]);
            plantedTree.sprite = treeImage[5].sprite;
        }
        else if (sender == senders[6] && GameProgress.gold - Math.Abs(howManyToPayForPlant[6] / Upgrades.seedCostBuf[6]) >= 0)
        {
            whenGrowed = DateTime.Now.Add(UpdateGrowTime(Trees.ива));
            GameProgress.gold -= Math.Abs(howManyToPayForPlant[6] / Upgrades.seedCostBuf[6]);
            plantedTree.sprite = treeImage[6].sprite;
        }
        else
        {
            Debug.LogError("Опять вы сделали невозможное. Как можно нажать на кнопку которой нет?");
        }

        #endregion тут мы получаем время когда вырастет определённое дерево (when growed) и клеим пикчу этого дерева на img. А так же снимаем деньги

        Save();
    }

    private void Save()
    {
        PlayerPrefs.SetString("isGrown", isGrown.ToString());
        PlayerPrefs.SetString("whatGrow", whatGrow.ToString());
        PlayerPrefs.SetString("whenGrow", whenGrowed.ToString());

        string txt = null;
        for (int i = 0; i < howManyToPayForPlant.Length; i++)
        {
            txt += howManyToPayForPlant[i] + " ";
        }
        txt = txt.Remove(txt.Length - 1, 1);
        PlayerPrefs.SetString("howManyToPayForPlant", txt);

        PlayerPrefs.Save();
    }
}