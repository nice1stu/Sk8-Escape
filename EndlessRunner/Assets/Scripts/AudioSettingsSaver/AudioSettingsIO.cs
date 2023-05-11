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
                // Create a new instance of AudioSettings with default values
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
}