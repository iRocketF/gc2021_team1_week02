using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanUp : MonoBehaviour
{
    private float timer;
    private float cleanUpTimer = 3f;

    void Update()
    {
        timer = timer + Time.deltaTime;

        if (timer >= cleanUpTimer)
        {
            Destroy(gameObject);
        }
    }
}
