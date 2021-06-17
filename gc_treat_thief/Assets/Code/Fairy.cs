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

    private GameManager manager;

    void Start()
    {
        manager = FindObjectOfType<GameManager>();
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
                ThirdPersonMovement pController = hitCollider.gameObject.GetComponent<ThirdPersonMovement>();

                if (!pController.isHidden)
                {
                    manager.pIsChased = true;
                    player = hitCollider.transform;
                    FollowPlayer(player);
                }
                else
                {
                    player = null;
                }

                break;
            }
        }

        if (player == null)
        {
            followPlayer = false;

            manager.pIsChased = false;
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
        float minimum = transform.position.y;

        float y = Mathf.PingPong(Time.time * speed, pingPongHeight) + minimum;
        enemyObject.transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }
}
