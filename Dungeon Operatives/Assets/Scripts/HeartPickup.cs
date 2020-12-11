using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPickup : MonoBehaviour
{
    public int healthAmount;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        healthAmount = Random.Range(15, 20);
        if (other.tag == "Player")
        {
            PlayerController health = other.gameObject.GetComponentInChildren<PlayerController>();
            health.health += healthAmount;
            Destroy(gameObject);
            Debug.Log("Player gained " + healthAmount + " health");
        }
        if (other.tag == "Player2")
        {
            Player2Controller health2 = other.gameObject.GetComponentInChildren<Player2Controller>();
            health2.health += healthAmount;
            Destroy(gameObject);
            Debug.Log("Player 2 gained " + healthAmount + " health");
        }
    }
}
