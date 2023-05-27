using UnityEngine;
using UnityEngine.SceneManagement;

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
    public AudioSource skateSfx;
    public AudioSource grindSfx;
    public bool startActive = false;
    private bool _isPaused;
    
    private static float previousTimescale;
    private static bool IsPaused;

    public bool IsItPaused => _isPaused;

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
            music.Pause();
            skateSfx.Pause();
            grindSfx.Stop();
        }
        else
        { 
            Time.timeScale = previousTimescale;
            music.UnPause();
            skateSfx.UnPause();
        }
    }
}
