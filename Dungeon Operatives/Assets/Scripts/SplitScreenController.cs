using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitScreenController : MonoBehaviour
{
    public Camera playerCam1, playerCam2;
    public bool isHorizontal;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonUp("P1 Start"))
        {
            isHorizontal = !isHorizontal;
            SplitScreen();
        }
    }

    public void SplitScreen()
    {
        if (isHorizontal)
        {
            playerCam1.rect = new Rect(0f, 0.5f, 1f, 0.5f);
            playerCam2.rect = new Rect(0f, 0f, 1f, 0.5f);
        }
        else
        {
            playerCam1.rect = new Rect(0f, 0f, 0.5f, 1f);
            playerCam2.rect = new Rect(0.5f, 0f, 0.5f, 1f);
        }
    }
}
