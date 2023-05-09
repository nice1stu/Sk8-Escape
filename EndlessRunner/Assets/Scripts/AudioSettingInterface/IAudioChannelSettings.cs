namespace AudioSettingInterface
{
    public interface IAudioChannelSettings
    {
        public float Volume{get;set;}
        public bool Muted{get;set;}
    }

    public abstract class AudioSettings
    {
        protected IAudioChannelSettings music;
        protected IAudioChannelSettings sfx;
    }
}