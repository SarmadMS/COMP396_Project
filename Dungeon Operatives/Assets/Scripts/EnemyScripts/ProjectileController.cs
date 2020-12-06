using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public Rigidbody rb;
    private GameObject player;
    public int damage;
    public int damage1;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        damage = Random.Range(15, 25);
        if(collision.gameObject == player)
        {
            collision.transform.gameObject.GetComponentInChildren<PlayerController>().health -= damage;
            Debug.Log("Ranged Enemy dealt " + damage + " damage");
        }
        //if (collision.gameObject == player && collision.transform.gameObject.GetComponentInParent<RangeEnemyController>().attackRange == 3)
        //{
        //    collision.transform.gameObject.GetComponentInChildren<PlayerController>().health -= damage;
        //    Debug.Log("Chasing Enemy dealt " + damage);
        //}
        if(collision.transform.gameObject.GetComponentInChildren<PlayerController>().health <= 0)
        {
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
