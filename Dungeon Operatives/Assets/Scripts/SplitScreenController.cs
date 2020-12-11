using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitScreenController : MonoBehaviour
{
    [SerializeField] Camera playerCam1, playerCam2;
    [SerializeField] GameObject player1, player2;
    [SerializeField] bool isSolo = true;
    [SerializeField] bool isHorizontal = true;

    [SerializeField] Transform playerSpawnPoint;

    // Start is called before the first frame update
    void Start()
    {        
        playerCam1.rect = new Rect(0f, 0f, 1f, 1f);
        playerCam2.rect = new Rect(0f, 0f, 0f, 0f);
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

    public void RespawnPlayer1()
    {
        player1 = Instantiate(player1, playerSpawnPoint.transform.position, playerSpawnPoint.rotation);
        player1.name = "Dungeon Operative";
    }

    public void RespawnPlayer2()
    {
        player2 = Instantiate(player2, playerSpawnPoint.transform.position, playerSpawnPoint.rotation);
        player1.name = "Dungeon Operative 2";
    }
}
