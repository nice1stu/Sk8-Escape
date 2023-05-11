using System.IO;
using UnityEngine;

namespace AudioSettingsSaver
{
    public class AudioSettingsIO
    {
        private const string SettingsFileName = "audio_settings.json";

        // The current audio settings
        private AudioSettings _currentSettings;

        public AudioSettingsIO()
        {
            // Load the audio settings from the JSON file
            LoadSettings();
        }

        private void LoadSettings()
        {
            string filePath = Path.Combine(Application.persistentDataPath, SettingsFileName);
            if (File.Exists(filePath))
            {
                // Read the JSON file and deserialize it into an instance of AudioSettings
                string json = File.ReadAllText(filePath);
                _currentSettings = JsonUtility.FromJson<AudioSettings>(json);
            }
            else
            {
                // Use default settings if the JSON file doesn't exist or if there's an error loading the settings
                _currentSettings = new AudioSettings();
            }
        }

        private void SaveSettings()
        {
            // Serialize the current settings to JSON
            string json = JsonUtility.ToJson(_currentSettings, true);

            // Write the JSON to a file in the persistent data path
            string filePath = Path.Combine(Application.persistentDataPath, SettingsFileName);
            File.WriteAllText(filePath, json);
        }
    }
    
    public class DefaultAudioSettings : IAudioSettings
    {
        public DefaultAudioSettings()
        {
            Music = new DefaultAudioChannelSettings();
            Sfx = new DefaultAudioChannelSettings();
        }

        public IAudioChannelSettings Music { get; }
        public IAudioChannelSettings Sfx { get; }
    }

    public class DefaultAudioChannelSettings : IAudioChannelSettings
    {
        public DefaultAudioChannelSettings()
        {
            Volume = 1f;
            Muted = false;
        }

        public float Volume { get; set; }
        public bool Muted { get; set; }
    }
}