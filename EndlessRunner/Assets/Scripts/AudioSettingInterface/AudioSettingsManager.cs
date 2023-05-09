using System.IO;
using UnityEngine;

namespace AudioSettingInterface
{
    public class AudioSettingsManager : AudioSettings
    {
        private const string SettingsFilePath = "settings.save.json";

        public AudioSettingsManager()
        {
            // Set default values for music and SFX
            music = new AudioChannelSettings { Volume = 1, Muted = false };
            sfx = new AudioChannelSettings { Volume = 1, Muted = false };

            // Load saved settings from file
            LoadSettings();
        }

        public void SaveSettings()
        {
            string json = JsonUtility.ToJson(this);
            File.WriteAllText(Application.persistentDataPath + "/" + SettingsFilePath, json);
        }

        private void LoadSettings()
        {
            try
            {
                string filePath = Application.persistentDataPath + "/" + SettingsFilePath;
                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    JsonUtility.FromJsonOverwrite(json, this);
                }
            }
            catch (IOException)
            {
                // Use default if there is error in loading saved settings
            }
        }
    }

    public class AudioChannelSettings : IAudioChannelSettings
    {
        public float Volume { get; set; }
        public bool Muted { get; set; }
    }
}