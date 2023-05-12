using System;
using AudioSettingsSaver;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace UI.Scripts.Settings
{
    public class VolumeWidget : MonoBehaviour
    {
        // TODO: Replace before shipping
        #region DUMMY_SAVE_BACKEND
        // -------------------------------------------------------------

        public static float DummyInitialEffectsVolume = 70f; 
        public static bool DummyInitialEffectsMuted = false;
        
        public static float DummyInitialMusicVolume = 50f; 
        public static bool DummyInitialMusicMuted = false; 

        public IAudioChannelSettings DummyEffectsSettings = new AudioChannelSettings(DummyInitialEffectsVolume, DummyInitialEffectsMuted);
        public IAudioChannelSettings DummyMusicSettings = new AudioChannelSettings(DummyInitialEffectsVolume, DummyInitialEffectsMuted);

        // -------------------------------------------------------------
        #endregion
        
        [SerializeField] public Slider volumeSlider;
        [SerializeField] public Toggle muteToggle;
        
        public IAudioSettings AudioEffectsSettings;
        public IAudioSettings AudioMusicSettings;

        public static AudioSettingsIO AudioEffectsInstance;
        
        private void Awake()
        {
            Assert.IsNotNull(volumeSlider,"Volume widget has volume slider assigned");
            Assert.IsNotNull(muteToggle, "Volume widget has no mute toggle assigned");
        }

        private void Start()
        {
            AudioEffectsInstance = new AudioSettingsIO();
        }

        private void InitFromSaved(IAudioChannelSettings channelSettings)
        {
            Debug.Log($"Initing settings volume from backend, Volume: {channelSettings.Volume} Muted: {channelSettings.Muted}");
            volumeSlider.value = channelSettings.Volume;
               muteToggle.isOn = channelSettings.Muted;
        }

        // TODO: Replace with actual saving backend
        // Refactor: Inheritance would probably be cleaner
        public void SaveSliderToEffects()
        {
            Debug.Log($"Mock saving effects volume: {volumeSlider.value}");
            DummyEffectsSettings.Volume = volumeSlider.value;
        }

        public void SaveSliderToMusic()
        {
            Debug.Log($"Mock saving music volume: {volumeSlider.value}");
            DummyMusicSettings.Volume = volumeSlider.value;
        }

        public void SaveToggleToEffects()
        {
            Debug.Log($"Mock saving effect mute: {muteToggle.isOn}");
            DummyEffectsSettings.Muted = muteToggle.isOn;
        }

        public void SaveToggleToMusic()
        {
            Debug.Log($"Mock saving sound mute: {muteToggle.isOn}");
            DummyMusicSettings.Muted = muteToggle.isOn;
        }

        // TODO: Replace with actual backend saved settings
        public void InitFromSavedEffects()
        {
            Debug.Log("Mock initing from saved effects settings");
            InitFromSaved(DummyEffectsSettings);
        }

        public void InitFromSavedMusic()
        {
            Debug.Log("Mock initing from saved music settings");
            InitFromSaved(DummyMusicSettings);
        }
    }
}
