using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ButtonFx : MonoBehaviour
{

    public AudioSource source;
    public AudioClip hoverFx;
    public AudioClip clickFx;

    public void onHoverPlay()
    {
        source.PlayOneShot(hoverFx);
    }

    public void onClickPlay()
    {
        source.PlayOneShot(clickFx);
    }

}
