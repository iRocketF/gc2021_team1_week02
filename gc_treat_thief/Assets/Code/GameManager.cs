using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField]  private AudioSource mainMusic;
    [SerializeField]  private AudioSource chaseMusic;

    public GameObject[] treats;

    public bool isGameActive;
    public bool pIsChased;
    public bool isChaseOn;
    public bool isMusicFading;
    public bool isAmountSet;

    public float gameTimer;
    public float gameLength;
    public float messageTime;
    public float messageTimer;

    public float treatsAmount;
    public float treatsLeft;

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
        if (gameTimer > 0f)
        {
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(2))
            {
                treats = GameObject.FindGameObjectsWithTag("Treat");
                treatsLeft = treats.Length;

                isGameActive = true;

                if (!isAmountSet)
                {
                    isAmountSet = true;
                    treatsAmount = treatsLeft;
                }
            }

            else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(0))
            {
                gameTimer = gameLength;
                isGameActive = false;
            }
                
        }

        if (isGameActive)
            gameTimer -= Time.deltaTime;

        if (gameTimer <= 0f)
            isGameActive = false;

        if (!isGameActive)
            if (Input.GetAxis("Reload") >= 0.5f) 
            {
                SceneManager.LoadScene(2);
                gameTimer = gameLength;
                messageTime = 0f;
            }
                
                
        UpdateMusic();
    }

    void UpdateMusic()
    {
        if (pIsChased && !isChaseOn && !isMusicFading)
        {
            Debug.Log("music change yes???");
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
