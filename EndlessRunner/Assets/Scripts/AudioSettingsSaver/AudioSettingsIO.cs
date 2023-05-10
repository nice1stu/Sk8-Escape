using UnityEngine;
using System.IO;

namespace AudioSettingsSaver
{
    public class AudioSettingsManager
    {
        private readonly string _settingsFilePath;

        public AudioSettingsManager(string settingsFilePath)
        {
            _settingsFilePath = settingsFilePath;
        }

        public void SaveSettings(IAudioSettings settings)
        {
            var serializedSettings = JsonUtility.ToJson(settings);
            File.WriteAllText(_settingsFilePath, serializedSettings);
        }

        public AudioSettings LoadSettings()
        {
            try
            {
                var serializedSettings = File.ReadAllText(_settingsFilePath);
                var settings = JsonUtility.FromJson<AudioSettings>(serializedSettings);
                return settings ?? new AudioSettings();
            }
            catch (FileNotFoundException)
            {
                return new AudioSettings();
            }
        }
    }
}