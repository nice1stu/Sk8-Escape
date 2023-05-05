using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
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

    // UI Element References
    [SerializeField] public Slider effectsVolumeSlider;
    [SerializeField] public Toggle effectsMuteToggle;
    
    [SerializeField] public Slider musicVolumeSlider;
    [SerializeField] public Toggle musicMuteToggle;

    [SerializeField] public Button changeLanguageButton;
    [SerializeField] private TMP_Text changeLanguageButtonText;

    [SerializeField] public Button backButton;

    [SerializeField] private SaveSettings persistentSettingsManager;
    public static bool StartHidden = false;
    private static bool _isActive = !StartHidden;
    
    void Awake()
    {
        MenuInstance ??= this;

        // Not great, interfacing with a static class would be better
        persistentSettingsManager ??= GetComponent<SaveSettings>(); // get component if null
        
        // Language Button's text will change to the currently selected language
        // Get and cache the component here
        changeLanguageButtonText = changeLanguageButton.GetComponentInChildren<TMP_Text>(true);
        
        SetUIStateFromSavedData();
        
        gameObject.SetActive(_isActive);
    }
    
    // TODO: Refactor abhorrent eldritch abomination
    void Start()
    {
        
        effectsVolumeSlider.onValueChanged.AddListener(delegate
        {
            persistentSettingsManager.SaveSfxVolume = effectsVolumeSlider.value;
            persistentSettingsManager.SaveSettingsData();
            
        } );
        musicVolumeSlider.onValueChanged.AddListener(delegate
        {
            persistentSettingsManager.SaveMusicVolume = musicVolumeSlider.value;
            persistentSettingsManager.SaveSettingsData();
            
        } );

        effectsMuteToggle.onValueChanged.AddListener(delegate
        {
            persistentSettingsManager.SaveSfxMute = effectsMuteToggle.isOn;
            persistentSettingsManager.SaveSettingsData();
            
        } );
        
        musicMuteToggle.onValueChanged.AddListener(delegate
        {
            persistentSettingsManager.SaveMusicMute = musicMuteToggle.isOn;
            persistentSettingsManager.SaveSettingsData();
        } );
        
        changeLanguageButton.onClick.AddListener(delegate
        {
            Debug.Log("Switch Language");
            persistentSettingsManager.SaveSettingsData();
        } );
        
        backButton.onClick.AddListener(ToggleSettingsMenu);
    }

    public static void ToggleSettingsMenu()
    {
        Assert.IsNotNull(MenuInstance);
        
        _isActive = !_isActive;

        MenuInstance.gameObject.SetActive(_isActive);
    }
    
    // Just to make things look a little cleaner, more consistent
    private static void SetSlider(float value, Slider slider) => slider.value = value;
    private static void SetToggle(bool isChecked, Toggle toggle) => toggle.isOn = isChecked;
    private static void SetText(string text, TMP_Text textMesh) => textMesh.SetText(text);

    private void SetUIFromState(SaveSettings state)
    {
        SetSlider(state.SaveSfxVolume,   effectsVolumeSlider);
        SetToggle(state.SaveSfxMute,     effectsMuteToggle);
        SetSlider(state.SaveMusicVolume, musicVolumeSlider);
        SetToggle(state.SaveMusicMute,   musicMuteToggle);
          SetText(state.SaveLanguageSetting, changeLanguageButtonText);
    }
    
    // Sets the UI to reflect backend values 
    private void SetUIStateFromSavedData()
    {
        Assert.IsNotNull(persistentSettingsManager);
        Assert.IsNotNull(changeLanguageButtonText);  // checking this specifically since it's not a simple reference
        
        persistentSettingsManager.LoadSettingsData();
        SetUIFromState(persistentSettingsManager);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
