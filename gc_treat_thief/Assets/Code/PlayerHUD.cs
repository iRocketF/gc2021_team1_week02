using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private float effectTimer;
    [SerializeField] private float effectTime;
    [SerializeField] private float floatUpSpeed;

    [SerializeField] private TextMeshProUGUI timer;
    [SerializeField] private TextMeshProUGUI treats;
    [SerializeField] private TextMeshProUGUI treatsStored;
    [SerializeField] private TextMeshProUGUI timeAdded;
    [SerializeField] private TextMeshProUGUI gameStatus;

    public GameManager manager;
    public PlayerInventory pInv;
    public Nest pNest;

    public bool isEffectActive;

    private void Start()
    {
        manager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        UpdateGameStatus();
    }

    void UpdateGameStatus()
    {
        timer.text = Mathf.RoundToInt(manager.gameTimer).ToString();
        treats.text = "Treats collected: " + pInv.treatsCollected.ToString();
        treatsStored.text = "Treats stored: " + pNest.storedTreats.ToString();

        if (manager.isGameActive && manager.messageTimer < manager.messageTime)
        {
            manager.messageTimer += Time.deltaTime;
            gameStatus.text = "COLLECT TREATS \n AND BRING THEM  \n TO YOUR NEST";
        }
        else if (manager.isGameActive && manager.gameTimer < (manager.gameLength - manager.messageTime))
            gameStatus.text = " ";
        else if (!manager.isGameActive)
            gameStatus.text = "GAME OVER \n \n R TO RESTART";
    }
 

    public void SpawnEffect()
    {
        TextMeshProUGUI hudTextClone;

        hudTextClone = Instantiate(timeAdded, timeAdded.transform.position, timeAdded.transform.rotation, transform);

        hudTextClone.gameObject.active = true;
        hudTextClone.text = timeAdded.text;
        hudTextClone.gameObject.AddComponent<CleanUp>();

        // hudTextClone.transform.Translate(Vector3.up * Time.deltaTime * floatUpSpeed);

        effectTimer += Time.deltaTime;
    }
}
