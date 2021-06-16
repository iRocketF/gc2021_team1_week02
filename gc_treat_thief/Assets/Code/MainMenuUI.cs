using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    private const int gameScene = 1;

    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void StartGame()
    {
        Debug.Log("Play pressed, start the game");
        //SceneManager.LoadScene(gameScene);
    }

    public void Settings()
    {
        Debug.Log("Settings pressed, open the settings");
        // Add functionality later
    }

    public void QuitGame()
    {
        Debug.Log("Quit pressed, quit the game");
        Application.Quit();
    }
}
