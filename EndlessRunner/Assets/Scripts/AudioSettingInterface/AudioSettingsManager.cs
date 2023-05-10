using System;
using System.IO;
using UnityEngine;

namespace AudioSettingInterface
{
    public class AudioSettingsManager : MonoBehaviour, IDisposable
    {
        private const string SettingsFilePath = "settings.save.json";

        public AudioSettings _audioSettings = new AudioSettings();

        public AudioSettingsManager()
        {
            _audioSettings.Music.VolumeAndMutedChanged += SaveSettings;
        }
        
        private void Awake()
        {
            LoadSettings();
        }

        public void SaveSettings(Tuple<float, bool> other)
        {
            string json = JsonUtility.ToJson(_audioSettings);
            File.WriteAllText(Application.persistentDataPath + "/" + SettingsFilePath, json);
        }
        
        private void LoadSettings()
        {
            string filePath = Path.Combine(Application.persistentDataPath, SettingsFilePath);
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                if (!string.IsNullOrEmpty(json))
                {
                    _audioSettings = JsonUtility.FromJson<AudioSettings>(json);
                    return;
                }
            }
            // If the file doesn't exist or is empty, use defaults
            _audioSettings.Music.Volume = 1f;
        }
        
        ~AudioSettingsManager()
        {
            ReleaseUnmanagedResources();
        }
        private void ReleaseUnmanagedResources()
        {
            // TODO release unmanaged resources here
            _audioSettings.Music.VolumeAndMutedChanged -= SaveSettings;
        }

        public void Dispose()
        {
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }
    }
}