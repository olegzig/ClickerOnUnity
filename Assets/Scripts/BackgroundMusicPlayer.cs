using System.Linq;
using UnityEngine;

public class BackgroundMusicPlayer : MonoBehaviour
{
    public AudioSource[] myAudio;

    // Use this for initialization
    void Start()
    {
        if (!myAudio[0].playOnAwake)
        {
            myAudio[Random.Range(0, myAudio.Length)].Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (myAudio.All(x => x.isPlaying == false))
        {
            myAudio[Random.Range(0, myAudio.Length)].Play();
        }
    }
}