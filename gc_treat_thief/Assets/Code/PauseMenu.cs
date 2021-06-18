using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    private GameManager manager;

    void Start()
    {
        manager = FindObjectOfType<GameManager>();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void Pause()
    {
        Debug.Log("Pause");
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        Debug.Log("Resume");
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Restart()
    {
        Debug.Log("Restart");
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;

        manager.messageTimer = 0f;
        manager.gameTimer = manager.gameLength;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToMenu()
    {
        Debug.Log("Back to menu");
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
