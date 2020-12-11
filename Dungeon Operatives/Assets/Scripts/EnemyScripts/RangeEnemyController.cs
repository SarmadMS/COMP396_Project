using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangeEnemyController : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform player;
    public Transform player2;
    public LayerMask whatIsGround, whatIsPlayer;
    public float rhealth = 100;
    public int damage;
    public GameObject healthDrop;
    public bool dropsHealth;

    //Patrol Variables
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attack Variable
    public float projectileSpeed;
    public float attackDelay;
    bool attackActive;
    public GameObject enemyProjectile;

    //State Variables
    public float sightRange, attackRange;
    public bool inSightRange, inAttackRange;

    // Start is called before the first frame update
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    private void Update()
    {
        //player = GameObject.FindGameObjectWithTag("Player2").transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;

        //Checks for sight and attack range from player
        inSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        inAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!inSightRange && !inAttackRange) Patrol();
        if (inSightRange && !inAttackRange) Chase();
        if (inSightRange && inAttackRange) Attack();
    }

    //Moves to a set position after a certain distance
    private void Patrol()
    {
        Debug.Log("Patrolling....");
        if (!walkPointSet) SearchWalkPoint();
        if (walkPointSet)
            navMeshAgent.SetDestination(walkPoint);

        Vector3 walkPointDistance = transform.position - walkPoint;

        //Walkpoint range limit
        if (walkPointDistance.magnitude < 1f)
            walkPointSet = false;
    }

    //Sets the enemy set point position.
    private void SearchWalkPoint()
    {
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    //Chase Player Function - When player is in a certain range
    private void Chase()
    {
        Debug.Log("Chasing Player");
        navMeshAgent.SetDestination(player.position);
        if(player = null)
        {
            navMeshAgent.SetDestination(player2.position);
        }

        //navMeshAgent.SetDestination(player2.position);
    }

    //Attacks Player Function
    private void Attack()
    {
        //Debug.Log("Attacking Player");
        navMeshAgent.SetDestination(transform.position);
        transform.LookAt(player);
        if (player = null)
        {
            navMeshAgent.SetDestination(player2.position);
        }

        if (!attackActive)
        {
            //Instantiate Enemy projectile
            Rigidbody rb = Instantiate(enemyProjectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * projectileSpeed, ForceMode.Impulse);
            //rb.AddForce(transform.up * 8f, ForceMode.Impulse);

            attackActive = true;
            Invoke(nameof(ResetAttack), attackDelay);
        }
    }

    private void ResetAttack()
    {
        attackActive = false;
    }
    
    public void Enemydead()
    {
        if (rhealth <= 0) HealthDrop();
    }
    void HealthDrop()
    {
        if (dropsHealth) Instantiate(healthDrop, transform.position, transform.rotation);
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //collision.transform.gameObject.GetComponentInChildren<PlayerController>().health -= damage;
            Invoke(nameof(DamagePlayer), attackDelay);
        }
    }

    public void DamagePlayer(Collision collision)
    {
        collision.transform.gameObject.GetComponentInChildren<PlayerController>().health -= damage;
        Destroy(gameObject);
    }

    
}
