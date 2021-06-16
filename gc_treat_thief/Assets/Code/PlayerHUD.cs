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
        timer.text = Mathf.RoundToInt(manager.gameTimer).ToString();
        treats.text = "Treats collected: " + pInv.treatsCollected.ToString();
        treatsStored.text = "Treats stored: " + pNest.storedTreats.ToString();
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
