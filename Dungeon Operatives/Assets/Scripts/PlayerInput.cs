using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private GameObject player2;
    [SerializeField] private Camera player1Camera;

    private bool split = false;

    // Start is called before the first frame update
    void Start()
    {
        ChangeScreen();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonUp("P2Start"))
        {
            ChangeScreen();
        }
    }

    void ChangeScreen()
    {
        if (split == false)
        {
            player2.SetActive(false);
            player1Camera.rect = new Rect(0, 0, 1, 1);
            split = true;
        }
        else
        {
            player2.SetActive(true);
            player1Camera.rect = new Rect(0, 0.5f, 1, 0.5f);
            split = false;
        }
    }
}
