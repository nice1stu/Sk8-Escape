using System;
using UnityEngine;
using UnityEngine.Audio;

namespace AudioSettings
{
    [Serializable]
    public struct AudioChannelSettings : IAudioChannelSettings
    {
        public event Action<Tuple<float, bool>> VolumeAndMutedChanged; 
        [SerializeField] private AudioMixer audioMixer;

        [SerializeField] private bool muted;
        [SerializeField] private float volume;

        public float Volume
        {
            get => volume;
            set
            {
                volume = value;
                
                VolumeAndMutedChanged?.Invoke(new Tuple<float, bool>(volume,muted));
                
                audioMixer.SetFloat("Volume", Mathf.Log10(volume) * 20);
            }
        }

        public bool Muted
        {
            get => muted;
            set
            {
                muted = value;
                
                VolumeAndMutedChanged?.Invoke(new Tuple<float, bool>(volume, muted));
                
                audioMixer.SetFloat("Volume", muted ? -80 : Mathf.Log10(volume) * 20);
            }
        }

        public void SetGlobalMute(bool isMuted)
        {
            Muted = isMuted;
        }
    }
}