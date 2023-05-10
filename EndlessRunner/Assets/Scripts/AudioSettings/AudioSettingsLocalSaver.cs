using System;
using System.IO;
using UnityEngine;

namespace AudioSettings
{
    public class AudioSettingsLocalSaver : MonoBehaviour, IDisposable
    {
        public AudioSettings audioSettings;

        private const string SettingsFilePath = "settings.save.json";
        
        public AudioSettingsLocalSaver()
        {
            audioSettings.Music.VolumeAndMutedChanged += SaveSettings;
        }
        
        private void Awake()
        {
            LoadSettings();
        }

        private void SaveSettings(Tuple<float, bool> other)
        {
            audioSettings.IsMuted = audioSettings.Music.Muted && audioSettings.Sfx.Muted;
            string json = JsonUtility.ToJson(audioSettings);
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
                    audioSettings = JsonUtility.FromJson<AudioSettings>(json);
                }
            }

            // Set defaults if necessary
            if (audioSettings.Music.Volume == 0)
                audioSettings.Music.Volume = 1f;

            if (audioSettings.Sfx.Volume == 0)
                audioSettings.Sfx.Volume = 1f;

            if (audioSettings.IsMuted)
            {
                audioSettings.Music.Muted = true;
                audioSettings.Sfx.Muted = true;
            }
        }
        
        ~AudioSettingsLocalSaver()
        {
            ReleaseUnmanagedResources();
        }
        private void ReleaseUnmanagedResources()
        {
            // TODO release unmanaged resources here
            audioSettings.Music.VolumeAndMutedChanged -= SaveSettings;
        }

        public void Dispose()
        {
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }
    }
}