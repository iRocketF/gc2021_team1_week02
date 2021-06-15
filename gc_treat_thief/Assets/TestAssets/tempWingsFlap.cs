using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempWingsFlap : MonoBehaviour
{

    float timePassed = 0f;
    float dir = 1f;
    float curRot = 0f;
    public float animSpeed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        curRot += Time.deltaTime * animSpeed * dir;

        if(curRot < -15f && dir < 0f)
        {
            curRot = -15f;
            dir = 1f;
        }
        else if(curRot > 30f && dir > 0f)
        {
            curRot = 30f;
            dir = -1f;
        }

        transform.rotation = Quaternion.Euler(0f, 0f,curRot);

    }
}
