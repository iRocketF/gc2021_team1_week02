using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource mainMusic;
    [SerializeField]
    private AudioSource chaseMusic;

    public bool isGameActive;

    public bool pIsChased;
    public bool isChaseOn;
    public bool isMusicFading;

    public float gameTimer;
    public float gameLength;
    public float messageTime;
    public float messageTimer;

    // Start is called before the first frame update
    void Start()
    {
        gameTimer = gameLength;

        isGameActive = false;
        isMusicFading = false;
        isChaseOn = false;

        GameObject[] managers = GameObject.FindGameObjectsWithTag("GameController");

        if (managers.Length > 1)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(2))
            isGameActive = true;

        if (isGameActive)
            gameTimer -= Time.deltaTime;

        if (gameTimer <= 0)
            isGameActive = false;

        if (!isGameActive)
            if (Input.GetAxis("Reload") >= 0.5f) 
            {
                SceneManager.LoadScene(2);
                gameTimer = gameLength;
            }
                
                
        UpdateMusic();
    }

    void UpdateMusic()
    {
        if (pIsChased && !isChaseOn && !isMusicFading)
        {
            StartCoroutine(FadeMusic.StartFade(mainMusic, 2f, 0f));
            StartCoroutine(FadeMusic.StartFade(chaseMusic, 2f, 0.5f));
            isChaseOn = true;
        }
        else if (!pIsChased && isChaseOn && !isMusicFading)
        {
            StartCoroutine(FadeMusic.StartFade(chaseMusic, 2f, 0f));
            StartCoroutine(FadeMusic.StartFade(mainMusic, 2f, 0.5f));
            isChaseOn = false;
        }
    }



}
