using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EngineSound : MonoBehaviour
{
    [SerializeField] private float maxPich = 2f;
    [SerializeField] private float minPich = .1f;

    private AudioSource audioSource;
    private NavMeshAgent navMeshAgent;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        audioSource.time = Random.Range(0, 10f);
    }

    void Update()
    {
        audioSource.pitch = Mathf.Lerp(minPich, maxPich, navMeshAgent.velocity.magnitude);
    }
}
