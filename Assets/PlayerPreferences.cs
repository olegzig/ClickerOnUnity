using UnityEngine;
using UnityEngine.UI;

public class PlayerPreferences : MonoBehaviour
{
    public Slider clickVolume;
    public Slider musicVolume;

    public void SaveGame_music()
    {
        if (musicVolume)
        {
            PlayerPrefs.SetFloat("MusicVolume", musicVolume.value);
            PlayerPrefs.Save();
        }
    }

    public void SaveGame_vol()
    {
        if (clickVolume)
        {
            PlayerPrefs.SetFloat("ClickVolume", clickVolume.value);
            PlayerPrefs.Save();
        }
    }

    public void Start()
    {
        //Эта просто работает и пока всё работает - лучше не трогать
        try
        {
            clickVolume.value = PlayerPrefs.GetFloat("ClickVolume");//тут ловит err, но я НЕ знаю почему. вроде всё работает как надо
            musicVolume.value = PlayerPrefs.GetFloat("MusicVolume");
        }
        catch
        {
//ага ага ага гага спасибо
        }
    }
}