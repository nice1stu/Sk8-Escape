using System;
using AudioSettingsSaver;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace UI.Scripts.Settings
{
    public class VolumeWidget : MonoBehaviour
    {

        [SerializeField] public Slider volumeSlider;
        [SerializeField] public Toggle muteToggle;

        private void Awake()
        {
            Assert.IsNotNull(volumeSlider,"Volume widget has volume slider assigned");
            Assert.IsNotNull(muteToggle, "Volume widget has no mute toggle assigned");
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
            Dependencies.Instance.Audio.Sfx.Volume = volumeSlider.value;
        }

        public void SaveSliderToMusic()
        {
            Debug.Log($"Mock saving music volume: {volumeSlider.value}");
            Dependencies.Instance.Audio.Music.Volume = volumeSlider.value;
        }

        public void SaveToggleToEffects()
        {
            Debug.Log($"Mock saving effect mute: {muteToggle.isOn}");
            Dependencies.Instance.Audio.Sfx.Muted = muteToggle.isOn;
        }

        public void SaveToggleToMusic()
        {
            Debug.Log($"Mock saving sound mute: {muteToggle.isOn}");
            Dependencies.Instance.Audio.Music.Muted = muteToggle.isOn;
        }

        // TODO: Replace with actual backend saved settings
        public void InitFromSavedEffects()
        {
            Debug.Log("Mock initing from saved effects settings");
            InitFromSaved(Dependencies.Instance.Audio.Sfx);
        }

        public void InitFromSavedMusic()
        {
            Debug.Log("Mock initing from saved music settings");
            InitFromSaved(Dependencies.Instance.Audio.Music);
        }
    }
}
