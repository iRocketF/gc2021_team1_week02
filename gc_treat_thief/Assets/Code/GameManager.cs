using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource mainMusic;
    [SerializeField]
    private AudioSource chaseMusic;

    public bool pIsChased;
    public bool isChaseOn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
