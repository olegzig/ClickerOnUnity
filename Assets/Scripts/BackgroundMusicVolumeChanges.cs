using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System;

public class BackgroundMusicVolumeChanges : MonoBehaviour
{
    public Slider volume;
    public AudioSource[] Music;
    public void VolChange()
    {
        for(int i = 0; i < Music.Length; i++)
        {
            Music[i].volume = volume.value;
        }
    }
}
