using System.IO;
using UnityEngine;

namespace AudioSettingInterface
{
    public class AudioSettingsManager : MonoBehaviour
    {
        private const string SettingsFilePath = "settings.save.json";

        private AudioSettings _audioSettings;

        private void Awake()
        {
            _audioSettings = new AudioSettings();

            LoadSettings();
        }

        public void SaveSettings()
        {
            string json = JsonUtility.ToJson(_audioSettings);
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
                    JsonUtility.FromJsonOverwrite(json, _audioSettings);
                }
            }
            catch (IOException)
            {
                // Use defaults if there are any errors loading
            }
            _audioSettings.Music.Volume = PlayerPrefs.GetFloat("MusicVolume", 1);
            _audioSettings.Music.Muted = PlayerPrefs.GetInt("MusicMuted", 0) == 1;
        }
    }
}