namespace AudioSettingInterface
{
    public interface IAudioChannelSettings
    {
        public float Volume { get; set; }
        public bool Muted { get; set; }
    }

    public interface IAudioSettings
    {
        IAudioChannelSettings Music { get; }
        IAudioChannelSettings SFX { get; }
    }
}