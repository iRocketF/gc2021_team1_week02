using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHUD : MonoBehaviour
{
    public TextMeshProUGUI treats;
    public TextMeshProUGUI treatsStored;

    public PlayerInventory pInv;
    public Nest pNest;

    // Update is called once per frame
    void Update()
    {
        treats.text = "Treats collected: " + pInv.treatsCollected.ToString();
        treatsStored.text = "Treats stored: " + pNest.storedTreats.ToString();
        
    }
}
