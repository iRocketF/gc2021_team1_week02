using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nest : MonoBehaviour
{
    public float storedTreats;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory pInv = other.GetComponent<PlayerInventory>();

            storedTreats = storedTreats + pInv.treatsCollected;
            pInv.treatsCollected = pInv.treatsCollected - pInv.treatsCollected;
        }
    }
}
