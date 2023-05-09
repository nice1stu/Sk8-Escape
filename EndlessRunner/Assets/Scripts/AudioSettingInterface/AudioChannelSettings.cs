using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;

namespace AudioSettingInterface
{
    [System.Serializable]
    public class AudioChannelSettings : IAudioChannelSettings
    {
        [SerializeField] private AudioMixer audioMixer;

        private bool _muted;
        private float _volume;

        public float Volume
        {
            get => _volume;
            set
            {
                _volume = value;

                // Save volume to player preferences or any persistent storage
                PlayerPrefs.SetFloat("MusicVolume", _volume);

                // Set volume on mixer
                audioMixer.SetFloat("Volume", Mathf.Log10(_volume) * 20);
            }
        }

        public bool Muted
        {
            get => _muted;
            set
            {
                _muted = value;

                // Save mute status to player preferences or any persistent storage
                PlayerPrefs.SetInt("MusicMuted", _muted ? 1 : 0);

                // Set mute status on mixer
                audioMixer.SetFloat("Volume", _muted ? -80 : Mathf.Log10(_volume) * 20);
            }
        }
    }
}