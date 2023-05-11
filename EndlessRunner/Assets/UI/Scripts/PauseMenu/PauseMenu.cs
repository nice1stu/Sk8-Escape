using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Experimental
    public static void LoadSettingsAdditively() => SceneManager.LoadScene("SettingsMenu", LoadSceneMode.Additive);
    public static void LoadStartMenuScene()     => SceneManager.LoadScene("StartMenu");

    public bool StartActive = true;
    private bool _pauseMenuActive;
    
    private static float previousTimescale;
    private static bool IsPaused() => (Time.timeScale == 0f);

    private void Start()
    {
        _pauseMenuActive = StartActive;
        gameObject.SetActive(_pauseMenuActive);
    }

    public void TogglePauseMenuActive()
    {
        _pauseMenuActive = !_pauseMenuActive;
        gameObject.SetActive(_pauseMenuActive);
    }

    public static void ToggleGameplayPaused()
    {
        if (IsPaused())
            Time.timeScale = previousTimescale;
        else
            previousTimescale = Time.timeScale;
    }
}
