using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleBg : MonoBehaviour
{

    public Toggle audioToggle;
    public AudioSource audioSource;
    public bool musicOn;
    public bool musicOff;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = AudioManager.instance.audioSource;
        musicOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onMusicToggle(bool toggle)
    {
        if (musicOn)
        {
            audioSource.Stop();
            musicOff = true;
            musicOn = false;
        }
        else if (musicOff)
        {
            audioSource.Play();
            musicOn = true;
            musicOff = false;
        }


    }
}
