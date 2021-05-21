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
        //��� ������ �������� � ���� �� �������� - ����� �� �������
        try
        {
            clickVolume.value = PlayerPrefs.GetFloat("ClickVolume");//��� ����� err, �� � �� ���� ������. ����� �� �������� ��� ����
            musicVolume.value = PlayerPrefs.GetFloat("MusicVolume");
        }
        catch
        {
//��� ��� ��� ���� �������
        }
    }
}