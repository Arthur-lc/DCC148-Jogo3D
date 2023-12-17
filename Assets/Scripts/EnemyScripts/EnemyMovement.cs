using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] public Transform target;
    private NavMeshAgent navAgent;

    private void Start() {
        navAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        navAgent.SetDestination(target.position);   
    }

    public Transform GetTarget() { return target; }
}
