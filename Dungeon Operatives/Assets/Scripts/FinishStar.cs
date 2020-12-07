using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishStar : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        SpawnStar();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SpawnStar()
    {
        if (GameObject.FindGameObjectsWithTag("RangeEnemy").Length == 0)
        {
            gameObject.SetActive(true);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            
        }
    }
}
