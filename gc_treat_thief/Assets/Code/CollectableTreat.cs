using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableTreat : MonoBehaviour
{
    public GameObject particles;

    public Vector3 startPos;
    public Quaternion rotPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        rotPos = transform.rotation;
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory pInv = other.GetComponentInChildren<PlayerInventory>();

            if (pInv.treatsCollected < pInv.maxTreats)
            {
                pInv.treatsCollected++;

                GameObject particleClone = Instantiate(particles, startPos, rotPos);
                TreatAudio treatSound = particleClone.GetComponent<TreatAudio>();

                treatSound.SetSound(gameObject.name);

                particleClone.AddComponent<CleanUp>();

                Destroy(gameObject);
            }
        }
    }
}
