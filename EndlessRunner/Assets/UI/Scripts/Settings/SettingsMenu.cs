using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

// [RequireComponent(
//     typeof(SettingsMenuView), 
//     typeof(SettingsMenuController),
//     typeof(SettingsMenuModel)
//     )]
public class SettingsMenu : MonoBehaviour
{
    // TODO: Bind pattern?
    // Startup scene is responsible for loading the StartMenu scene
    public static SettingsMenu MenuInstance;
    
    
    
    // Placeholder till interwoven with other scenes

    
    // [SerializeField] public Button changeLanguageButton;
    // [SerializeField] private TMP_Text changeLanguageButtonText;

    // [SerializeField] private SaveSettings persistentSettingsManager;
    public static bool StartHidden = false;
    private static bool _isActive = !StartHidden;
    
    public static void ReturnToStartupScene() => SceneManager.LoadScene("StartMenu");
    

    public static void UnloadAdditiveScene()
    {
        var lastLoadedScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        if (SceneManager.sceneCount > 1)
        {
            SceneManager.UnloadSceneAsync(lastLoadedScene);
        }
        else
        {
            SceneManager.LoadScene("StartMenu");
        }
        
        
    } 
    
    public static void ToggleSettingsMenu()
    {
        Assert.IsNotNull(MenuInstance);
        
        _isActive = !_isActive;

        MenuInstance.gameObject.SetActive(_isActive);
    }
    
    
    void Awake()
    {
        MenuInstance ??= this;

        // Not great, interfacing with a static class would be better
        // persistentSettingsManager ??= GetComponent<SaveSettings>(); // get component if null
        
        SetUIStateFromSavedData();
        
        gameObject.SetActive(_isActive);
    }
    
    void Start()
    {

    }


    // Just to make things look a little cleaner, more consistent
    private static void SetSlider(float value, Slider slider) => slider.value = value;
    private static void SetToggle(bool isChecked, Toggle toggle) => toggle.isOn = isChecked;
    private static void SetText(string text, TMP_Text textMesh) => textMesh.SetText(text);

    private void SetUIFromState(SaveSettings state)
    {
        // SetSlider(state.SaveSfxVolume,   effectsVolumeSlider);
        // SetToggle(state.SaveSfxMute,     effectsMuteToggle);
        // SetSlider(state.SaveMusicVolume, musicVolumeSlider);
        // SetToggle(state.SaveMusicMute,   musicMuteToggle);
    }
    
    // Sets the UI to reflect backend values 
    private void SetUIStateFromSavedData()
    {
        // Blocked: Awaiting interface
        // Assert.IsNotNull(persistentSettingsManager);

        // persistentSettingsManager.LoadSettingsData();
        // SetUIFromState(persistentSettingsManager);
    }
}
