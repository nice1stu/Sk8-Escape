using System;
using System.IO;
using UnityEngine;

namespace AudioSettings
{
    public class AudioSettingsLocalSaver : MonoBehaviour, IDisposable
    {
        private const string SettingsFilePath = "settings.save.json";

        public AudioSettings _audioSettings = new AudioSettings();

        public AudioSettingsLocalSaver()
        {
            _audioSettings.Music.VolumeAndMutedChanged += SaveSettings;
        }
        
        private void Awake()
        {
            LoadSettings();
        }

        public void SaveSettings(Tuple<float, bool> other)
        {
            _audioSettings.IsMuted = _audioSettings.Music.Muted && _audioSettings.Sfx.Muted;
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
                }
            }

            // Set defaults if necessary
            if (_audioSettings.Music.Volume == 0)
                _audioSettings.Music.Volume = 1f;

            if (_audioSettings.Sfx.Volume == 0)
                _audioSettings.Sfx.Volume = 1f;

            if (_audioSettings.IsMuted)
            {
                _audioSettings.Music.Muted = true;
                _audioSettings.Sfx.Muted = true;
            }
        }

        
        ~AudioSettingsLocalSaver()
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