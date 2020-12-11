using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public Rigidbody rb;
    private GameObject player;
    private GameObject player2;
    public int damage;

    public SplitScreenController splitScreenController;

    public PlayerController playerDamage;
    public Player2Controller player2Damage;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerDamage = GameObject.Find("Dungeon Operative/Camera").GetComponent<PlayerController>();
        player2Damage = GameObject.Find("Dungeon Operative 2/Camera").GetComponent<Player2Controller>();
        //player = GameObject.FindWithTag("Player");
        //player2 = GameObject.FindWithTag("Player2");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        damage = Random.Range(15, 25);
        if(collision.gameObject.tag == "Metal")
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Player")
        {
            playerDamage.DamagePlayer(damage);
            //collision.gameObject.GetComponentInChildren<PlayerController>().health -= damage;
            Destroy(gameObject);
            Debug.Log("Enemy dealt " + damage + " damage to Player 1");
        }
        else if (collision.gameObject.tag == "Player2")
        {
            player2Damage.DamagePlayer(damage);
            //collision.gameObject.GetComponentInChildren<Player2Controller>().health -= damage;
            Destroy(gameObject);
            Debug.Log("Enemy dealt " + damage + " damage to Player 2");
        }
        //if (collision.gameObject.GetComponentInChildren<PlayerController>().health <= 0)
        //{
        //    Destroy(gameObject);
        //    Destroy(collision.gameObject);
        //    Invoke(nameof(splitScreenController.RespawnPlayer1), 3f);
        //}
        //else if (collision.transform.gameObject.GetComponentInChildren<Player2Controller>().health <= 0)
        //{
        //    Destroy(gameObject);
        //    Destroy(collision.gameObject);
        //    Invoke(nameof(splitScreenController.RespawnPlayer2), 3f);
        //}
    }
}
