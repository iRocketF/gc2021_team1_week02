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

    void Start()
    {
        currentTarget = transform.position;
        GetNewDestination();
    }

    
    void Update()
    {
        if (Vector3.Distance(transform.position, currentTarget) < targetThreshold)
        {
            Debug.Log("Choosing a new direction");
            GetNewDestination();
        }

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
                Debug.Log("New destination OK");
                previousTarget = currentTarget;
                currentTarget = hit.position;
                agent.SetDestination(currentTarget);
                destinationOK = true;
                break;
            }
            else
            {
                Debug.Log("New destination not OK");
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

    void PingPong()
    {
        float mininum = 2f;

        float y = Mathf.PingPong(Time.time * speed, pingPongHeight) + mininum;
        enemyObject.transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }
}
