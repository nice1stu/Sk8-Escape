using System;
using UnityEngine;
using UnityEngine.Audio;

namespace AudioSettings
{
    [System.Serializable]
    public struct AudioChannelSettings : IAudioChannelSettings
    {
        public event Action<Tuple<float, bool>> VolumeAndMutedChanged; 
        [SerializeField] private AudioMixer audioMixer;

        [SerializeField]private bool _muted;
        [SerializeField] float _volume;

        public float Volume
        {
            get => _volume;
            set
            {
                _volume = value;

                // Save volume to player preferences or any persistent storage
                VolumeAndMutedChanged?.Invoke(new Tuple<float, bool>(_volume,_muted));

                // Set volume on mixer
                audioMixer.SetFloat("Volume", Mathf.Log10(_volume) * 20);
            }
        }

        [SerializeField] private bool _muted;
        public bool Muted
        {
            get => _muted;
            set
            {
                _muted = value;
                VolumeAndMutedChanged?.Invoke(new Tuple<float, bool>(_volume, _muted));
                audioMixer.SetFloat("Volume", _muted ? -80 : Mathf.Log10(_volume) * 20);
            }
        }

    }
}