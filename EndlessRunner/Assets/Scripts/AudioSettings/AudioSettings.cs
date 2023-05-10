using UnityEngine;

namespace AudioSettings
{
    [System.Serializable]
    public struct AudioSettings : IAudioSettings
    {
        [SerializeField] private AudioChannelSettings music;
        [SerializeField] private AudioChannelSettings sfx;
        [SerializeField] private bool globalMuted;

        public IAudioChannelSettings Music => music;
        public IAudioChannelSettings Sfx => sfx;

        public bool Muted
        {
            get => globalMuted;
            set
            {
                globalMuted = value;
                music.SetGlobalMute(globalMuted);
                sfx.SetGlobalMute(globalMuted);
            }
        }
        public bool IsMuted { get; set; }
    }
}