using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Fairy : MonoBehaviour
{
    public NavMeshAgent agent;

    [SerializeField] private float minDistance = 10f;
    [SerializeField] private float maxDistance = 20f;
    [SerializeField] private float targetThreshold = 0.1f;

    [SerializeField] private GameObject enemyObject;
    [SerializeField] private float pingPongHeight = 1.5f;
    private float speed = 1f;

    private Vector3 currentTarget;
    private Vector3 previousTarget;

    [SerializeField] private float sphereRadius = 5f;
    [SerializeField] private LayerMask layerMask;

    private bool followPlayer = false;

    void Start()
    {
        currentTarget = transform.position;
        GetNewDestination();
    }

    
    void Update()
    {
        if (Vector3.Distance(transform.position, currentTarget) < targetThreshold)
        {
            if (!followPlayer)
            {
                GetNewDestination();
            }
        }

        LookForThePlayer();
        PingPong();
    }

    void GetNewDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * Random.Range(minDistance, maxDistance);
        randomDirection += transform.position;

        NavMeshHit hit;

        bool destinationOK = false;

        for (int i = 0; i < 10; i++)
        {
            if (NavMesh.SamplePosition(randomDirection, out hit, maxDistance, 1))
            {
                previousTarget = currentTarget;
                currentTarget = hit.position;
                agent.SetDestination(currentTarget);
                destinationOK = true;
                break;
            }
            else
            {
                randomDirection = Random.insideUnitSphere * Random.Range(minDistance, maxDistance);
                randomDirection += transform.position;
            }
        }

        if (!destinationOK)
        {
            Vector3 temporarySave = currentTarget;
            currentTarget = previousTarget;
            previousTarget = temporarySave;
            agent.SetDestination(currentTarget);
        }
    }

    void LookForThePlayer()
    {
        Vector3 enemyOrigin = transform.position;
        Transform player = null;

        Collider[] hitColliders = Physics.OverlapSphere(enemyOrigin, sphereRadius, layerMask);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag("Player"))
            {
                player = hitCollider.transform;
                FollowPlayer(player);
                break;
            }
        }

        if (player == null)
        {
            followPlayer = false;
        }
    }

    void FollowPlayer(Transform target)
    {
        transform.LookAt(target);

        previousTarget = currentTarget;
        currentTarget = target.position;

        agent.SetDestination(currentTarget);
    }

    void PingPong()
    {
        float mininum = 2f;

        float y = Mathf.PingPong(Time.time * speed, pingPongHeight) + mininum;
        enemyObject.transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }
}
