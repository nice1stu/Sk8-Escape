using System;
using System.IO;
using UnityEngine;

namespace AudioSettingsSaver
{
    public class AudioSettings : IAudioSettings
    {
        private AudioChannelSettings _music;
        private AudioChannelSettings _sfx;

        private const string SettingsFilePath = "audio_settings.json";

        public AudioSettings()
        {
            Load();
        }

        public IAudioChannelSettings Music => _music;
        public IAudioChannelSettings Sfx => _sfx;

        public void Save()
        {
            // Serialize audio settings to JSON
            AudioSettingsData data = new AudioSettingsData
            {
                musicVolume = _music.Volume,
                musicMuted = _music.Muted,
                sfxVolume = _sfx.Volume,
                sfxMuted = _sfx.Muted
            };
            string json = JsonUtility.ToJson(data, true);

            // Write JSON to file
            File.WriteAllText(Path.Combine(Application.persistentDataPath, SettingsFilePath), json);
        }

        private void Load()
        {
            // Load audio settings from JSON file, or use default values if file doesn't exist
            string filePath = Path.Combine(Application.persistentDataPath, SettingsFilePath);
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                AudioSettingsData data = JsonUtility.FromJson<AudioSettingsData>(json);
                _music = new AudioChannelSettings { Volume = data.musicVolume, Muted = data.musicMuted };
                _sfx = new AudioChannelSettings { Volume = data.sfxVolume, Muted = data.sfxMuted };
            }
            else
            {
                _music = new AudioChannelSettings { Volume = 1f, Muted = false };
                _sfx = new AudioChannelSettings { Volume = 1f, Muted = false };
            }
        }

        [Serializable]
        private class AudioSettingsData
        {
            public float musicVolume;
            public bool musicMuted;
            public float sfxVolume;
            public bool sfxMuted;
        }
    }
}