using System;

namespace AudioSettingsSaver
{
    public interface IAudioChannelSettings
    {
        public float Volume { get; set; }
        public bool Muted { get; set; }
        
        event Action<Tuple<float, bool>> VolumeAndMutedChanged;
    }

    public interface IAudioSettings
    {
        IAudioChannelSettings Music { get; }
        IAudioChannelSettings Sfx { get; }
    }
}