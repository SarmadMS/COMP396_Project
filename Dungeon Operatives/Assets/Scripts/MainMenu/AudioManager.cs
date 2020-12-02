using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    

    public static AudioManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else 
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
      
    }

  
    void Update()
    {

    }

   
}
