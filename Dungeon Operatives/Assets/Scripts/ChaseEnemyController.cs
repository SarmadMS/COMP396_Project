using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseEnemyController : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            navMeshAgent.SetDestination(target.position);
        }
    }
}
