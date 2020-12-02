using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
   
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void onPlayClicked()
    {
        SceneManager.LoadScene("Experimental Scene");
    }
    public void onOptionsClicked()
    {
        SceneManager.LoadScene("Options Scene");
    }

    public void onInstructionsClicked()
    {
        SceneManager.LoadScene("Instructions Scene");
    }


}
