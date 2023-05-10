using System;

namespace AudioSettingsSaver
{
    public class AudioChannelSettings : IAudioChannelSettings
    {
        private float _volume;
        private bool _muted;

        public float Volume
        {
            get => _volume;
            set
            {
                _volume = value;
                VolumeAndMutedChanged?.Invoke(new Tuple<float, bool>(_volume, _muted));
            }
        }

        public bool Muted
        {
            get => _muted;
            set
            {
                _muted = value;
                VolumeAndMutedChanged?.Invoke(new Tuple<float, bool>(_volume, _muted));
            }
        }

        public event Action<Tuple<float, bool>> VolumeAndMutedChanged;
    }
}