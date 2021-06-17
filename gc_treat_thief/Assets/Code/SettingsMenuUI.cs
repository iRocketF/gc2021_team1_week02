using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenuUI : MonoBehaviour
{
    private const int gameScene = 1;

    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Back()
    {
        Debug.Log("Back pressed, go to main menu");
        SceneManager.LoadScene(0);
    }


}
