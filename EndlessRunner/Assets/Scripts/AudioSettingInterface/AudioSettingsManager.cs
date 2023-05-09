using System.IO;
using UnityEngine;

namespace AudioSettingInterface
{
    public class AudioSettings : IAudioSettings
    {
        public IAudioChannelSettings Music { get; set; }
        public IAudioChannelSettings SFX { get; set; }

        public AudioSettings()
        {
            Music = new AudioChannelSettings { Volume = 1, Muted = false };
            SFX = new AudioChannelSettings { Volume = 1, Muted = false };
        }
    }

    public class AudioSettingsManager : AudioSettings
    {
        private const string SettingsFilePath = "settings.save.json";

        public AudioSettingsManager()
        {
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
                // If there's an error loading settings, just use the defaults
            }
        }
    }

    public class AudioChannelSettings : IAudioChannelSettings
    {
        public float Volume { get; set; }
        public bool Muted { get; set; }
    }
}