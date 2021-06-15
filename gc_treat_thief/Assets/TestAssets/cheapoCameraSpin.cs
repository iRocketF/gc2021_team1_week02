using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cheapoCameraSpin : MonoBehaviour
{
    float rot = 0f;
    public float speed = 10f;

    // Update is called once per frame
    void Update()
    {
        rot += Time.deltaTime * speed;

        transform.rotation = Quaternion.Euler(0f, rot, 0f);
    }
}
