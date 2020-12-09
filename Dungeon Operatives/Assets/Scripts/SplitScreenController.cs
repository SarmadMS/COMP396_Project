using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitScreenController : MonoBehaviour
{
    public Camera playerCam1, playerCam2;
    public bool isSolo = true;
    public bool isHorizontal = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonUp("P1 Start"))
        {
            Debug.Log("p1 start");
            SplitScreen();
        }

        if (Input.GetButtonUp("P2 Start"))
        {
            Debug.Log("p2 start");
            ChangeSplit();
        }
    }

    public void SplitScreen()
    {
        if (isSolo == true)
        {
            isSolo = false;
            playerCam1.rect = new Rect(0f, 0.5f, 1f, 0.5f);
            playerCam2.rect = new Rect(0f, 0f, 1f, 0.5f);
            isHorizontal = true;
        }
        else
        {
            isSolo = true;
            playerCam1.rect = new Rect(0f, 0f, 1f, 1f);
            playerCam2.rect = new Rect(0f, 0f, 0f, 0f);
        }
    }

    public void ChangeSplit()
    {
        if (isHorizontal == true)
        {
            playerCam1.rect = new Rect(0f, 0f, 0.5f, 1f);
            playerCam2.rect = new Rect(0.5f, 0f, 0.5f, 1f);
            isHorizontal = false;
        }
        else
        {
            playerCam1.rect = new Rect(0f, 0.5f, 1f, 0.5f);
            playerCam2.rect = new Rect(0f, 0f, 1f, 0.5f);
            isHorizontal = true;
        }
    }
}
