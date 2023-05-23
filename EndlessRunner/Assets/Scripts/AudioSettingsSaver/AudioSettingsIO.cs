using System.IO;
using UnityEngine;
using UnityEngine.Audio;

namespace AudioSettingsSaver
{
    public class AudioSettings : IAudioSettings
    {
        public IAudioChannelSettings Music { get; set; }
        public IAudioChannelSettings Sfx { get; set; }

        public AudioSettings(AudioMixerGroup musicGroup, AudioMixerGroup sfxGroup)
        {
            Music = new AudioChannelSettings("Music", musicGroup); // Default music settings
            Sfx = new AudioChannelSettings("SFX", sfxGroup); // Default SFX settings
        }
    }

    public class AudioChannelSettings : IAudioChannelSettings
    {
        private readonly string _channelName;
        private readonly AudioMixerGroup _audioMixerGroup;
        private float _volume;
        private bool _muted;

        public float Volume
        {
            get => _volume;
            set
            {
                value = Mathf.Clamp01(value);
                _volume = value;
                PlayerPrefs.SetFloat(_channelName+"_Volume", value);
                UpdateMixerVolume();
            }
        }
        
        void UpdateMixerVolume() => _audioMixerGroup.audioMixer.SetFloat(_channelName+"Volume", ActualVolumeDb);

        private float ActualVolume => _muted ? 0f : _volume;
        private float ActualVolumeDb => ActualVolume == 0f ? -80f : Mathf.Log10(ActualVolume) * 20;

        public bool Muted
        {
            get => _muted;
            set
            {
                _muted = value;
                PlayerPrefs.SetInt(_channelName+"_Muted", value ? 1 : 0);
                UpdateMixerVolume();
            }
        }

        public AudioChannelSettings(string channelName, AudioMixerGroup audioMixerGroup)
        {
            _channelName = channelName;
            _audioMixerGroup = audioMixerGroup;
            Volume = PlayerPrefs.GetFloat(channelName+"_Volume", 1f);
            Muted = PlayerPrefs.GetInt(channelName+"_Muted", 0) == 1;
        }
    }

    public class AudioSettingsIO
    {
        private const string SettingsFileName = "audio_settings.json";

        // The current audio settings
        public AudioSettings currentSettings;

        public AudioSettingsIO()
        {
            // Load the audio settings from the JSON file
            LoadSettings();
        }

        public void LoadSettings()
        {
            string filePath = Path.Combine(Application.persistentDataPath, SettingsFileName);
            if (File.Exists(filePath))
            {
                // Read the JSON file and deserialize it into an instance of AudioSettings
                string json = File.ReadAllText(filePath);
                currentSettings = JsonUtility.FromJson<AudioSettings>(json);
            }
            else
            {
                // Create a new instance of AudioSettings with default values
                currentSettings = new AudioSettings(null, null);
                SaveSettings(); // Save the default settings to a JSON file
            }
        }

        public void SaveSettings()
        {
            // Serialize the current settings to JSON
            string json = JsonUtility.ToJson(currentSettings, true);

            // Write the JSON to a file in the persistent data path
            string filePath = Path.Combine(Application.persistentDataPath, SettingsFileName);
            File.WriteAllText(filePath, json);
        }
    }
}