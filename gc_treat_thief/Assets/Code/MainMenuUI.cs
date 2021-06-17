using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    private GameManager manager;

    private const int gameScene = 1;

    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        manager = FindObjectOfType<GameManager>();
    }

    public void StartGame()
    {
        manager.gameTimer = manager.gameLength;
        manager.messageTimer = 0f;
        SceneManager.LoadScene(2);
    }

    public void Settings()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
