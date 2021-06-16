using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Nest : MonoBehaviour
{
    public float storedTreats;
    public float timeAddedOnStore;

    public GameManager manager;
    public PlayerHUD pHud;

    private AudioSource storeSound;

    private void Start()
    {
        manager = FindObjectOfType<GameManager>();
        pHud = FindObjectOfType<PlayerHUD>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory pInv = other.GetComponent<PlayerInventory>();

            if (pInv.treatsCollected > 0)
            {
                manager.gameTimer = manager.gameTimer + (timeAddedOnStore * pInv.treatsCollected);
                pHud.SpawnEffect();
                storedTreats = storedTreats + pInv.treatsCollected;
                pInv.treatsCollected = pInv.treatsCollected - pInv.treatsCollected;
            }

        }
    }
}
