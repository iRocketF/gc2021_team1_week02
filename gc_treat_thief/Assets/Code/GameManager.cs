using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource mainMusic;
    [SerializeField]
    private AudioSource chaseMusic;

    public bool isGameActive;

    public bool pIsChased;
    public bool isChaseOn;

    public float gameTimer;

    // Start is called before the first frame update
    void Start()
    {
        isGameActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameActive)
            gameTimer -= Time.deltaTime;

        if (gameTimer <= 0)
            isGameActive = false;


        UpdateMusic();
    }

    void UpdateMusic()
    {
        if (pIsChased && !isChaseOn)
        {
            StartCoroutine(FadeMusic.StartFade(mainMusic, 2f, 0f));
            StartCoroutine(FadeMusic.StartFade(chaseMusic, 2f, 0.5f));
            isChaseOn = true;
        }
        else if (!pIsChased && isChaseOn)
        {
            StartCoroutine(FadeMusic.StartFade(chaseMusic, 2f, 0f));
            StartCoroutine(FadeMusic.StartFade(mainMusic, 2f, 0.5f));
            isChaseOn = false;
        }
    }



}
