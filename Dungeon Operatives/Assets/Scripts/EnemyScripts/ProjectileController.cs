using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public Rigidbody rb;
    private GameObject player;
    private GameObject player2;
    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player");
        player2 = GameObject.FindWithTag("Player2");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        damage = Random.Range(15, 25);
        if(collision.gameObject == player)
        {
            collision.transform.gameObject.GetComponentInChildren<PlayerController>().health -= damage;
            Destroy(gameObject);
            Debug.Log("Enemy dealt " + damage + " damage to Player 1");
        }
        else if (collision.gameObject == player2)
        {
            collision.transform.gameObject.GetComponentInChildren<Player2Controller>().health -= damage;
            Destroy(gameObject);
            Debug.Log("Enemy dealt " + damage + " damage to Player 2");
        }
        if (collision.transform.gameObject.GetComponentInChildren<Player2Controller>().health <= 0)
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
        else if(collision.transform.gameObject.GetComponentInChildren<PlayerController>().health <= 0)
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
        //|| collision.transform.gameObject.GetComponentInChildren<Player2Controller>().health <= 0
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Metal")
        {
            Destroy(gameObject);
        }
    }
}
