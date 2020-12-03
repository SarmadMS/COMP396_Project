using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemyHeadController : MonoBehaviour
{
    public RangeEnemyController health;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {

        if(health.health <= 0)
        {
            Destroy(collision.gameObject);
        }
    }
}
