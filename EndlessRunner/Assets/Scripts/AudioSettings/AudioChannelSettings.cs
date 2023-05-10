using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;

namespace AudioSettings
{
    [Serializable]
    public struct AudioChannelSettings : IAudioChannelSettings
    {
        public event Action<Tuple<float, bool>> VolumeAndMutedChanged; 
        [SerializeField] private AudioMixer audioMixer;

        [FormerlySerializedAs("_muted")] [SerializeField] private bool muted;
        [FormerlySerializedAs("_volume")] [SerializeField] private float volume;

        public float Volume
        {
            get => volume;
            set
            {
                volume = value;

                // Save volume to player preferences or any persistent storage
                VolumeAndMutedChanged?.Invoke(new Tuple<float, bool>(volume,muted));

                // Set volume on mixer
                audioMixer.SetFloat("Volume", Mathf.Log10(volume) * 20);
            }
        }

        public bool Muted
        {
            get => muted;
            set
            {
                muted = value;

                // Save mute status to player preferences or any persistent storage
                VolumeAndMutedChanged?.Invoke(new Tuple<float, bool>(volume, muted));

                // Set mute status on mixer
                audioMixer.SetFloat("Volume", muted ? -80 : Mathf.Log10(volume) * 20);
            }
        }

        public void SetGlobalMute(bool isMuted)
        {
            Muted = isMuted;
        }
    }
}