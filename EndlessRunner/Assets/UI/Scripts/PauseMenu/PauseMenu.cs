using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class PauseMenu : MonoBehaviour
{
    // Experimental
    public static void LoadSettingsAdditively() => SceneManager.LoadScene("SettingsMenu", LoadSceneMode.Additive);
    public static void LoadStartMenuScene()
    {
        Time.timeScale = previousTimescale;
        SceneManager.LoadScene("StartMenu");
    }

    public AudioSource music;
    public bool startActive = false;
    private bool _isPaused;
    
    private static float previousTimescale;
    private static bool IsPaused;

    private void Start()
    {
        _isPaused = startActive;
        gameObject.SetActive(_isPaused);

        previousTimescale = Time.timeScale;
    }

    public void TogglePauseState()
    {
        _isPaused = !_isPaused;
        
        SetGameplayPaused();
        SetTogglePauseMenuState();
        
        if(_isPaused)
            music.Pause();
        else
            music.UnPause();
    }
    
    private void SetTogglePauseMenuState()
    {
        gameObject.SetActive(_isPaused);
    }

    private void SetGameplayPaused()
    {
        if (_isPaused)
        {
            previousTimescale = Time.timeScale;
            Time.timeScale = 0;
        }
        else
            Time.timeScale = previousTimescale;
            // Time.timeScale = 1f;
    }
}
