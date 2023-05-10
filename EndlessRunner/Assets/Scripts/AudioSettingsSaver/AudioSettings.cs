namespace AudioSettingsSaver
{
    public class AudioSettings : IAudioSettings
    {
        private readonly IAudioChannelSettings _music;
        private readonly AudioChannelSettings _sfx;

        public AudioSettings()
        {
            _music = new AudioChannelSettings();
            _sfx = new AudioChannelSettings();

            // Load default values
            _music.Volume = 1f;
            _sfx.Volume = 1f;
        }

        public IAudioChannelSettings Music => _music;
        public IAudioChannelSettings Sfx => _sfx;
    }
}