using UnityEngine;
using UnityEngine.Serialization;

namespace AudioSettingsSaver
{
    public class AudioSettingsTest : MonoBehaviour
    {
        private AudioSettings _audioSettings;
        [FormerlySerializedAs("_musicVolume")] [SerializeField] private float musicVolume = 1f;
        [FormerlySerializedAs("_sfxVolume")] [SerializeField] private float sfxVolume = 1f;
        [FormerlySerializedAs("_musicMuted")] [SerializeField] private bool musicMuted;
        [FormerlySerializedAs("_sfxMuted")] [SerializeField] private bool sfxMuted;

        private void Start()
        {
            // Load settings on start
            musicVolume = _audioSettings.Music.Volume;
            musicMuted = _audioSettings.Music.Muted;
            sfxVolume = _audioSettings.Sfx.Volume;
            sfxMuted = _audioSettings.Sfx.Muted;
        }

        private void Update()
        {
            // Update settings on change
            if (_audioSettings.Music.Volume != musicVolume)
            {
                musicVolume = _audioSettings.Music.Volume;
                Debug.Log("Music volume changed to: " + musicVolume);
            }
            if (_audioSettings.Music.Muted != musicMuted)
            {
                musicMuted = _audioSettings.Music.Muted;
                Debug.Log("Music muted status changed to: " + musicMuted);
            }
            if (_audioSettings.Sfx.Volume != sfxVolume)
            {
                sfxVolume = _audioSettings.Sfx.Volume;
                Debug.Log("SFX volume changed to: " + sfxVolume);
            }
            if (_audioSettings.Sfx.Muted != sfxMuted)
            {
                sfxMuted = _audioSettings.Sfx.Muted;
                Debug.Log("SFX muted status changed to: " + sfxMuted);
            }

            // Update settings from inspector values
            _audioSettings.Music.Volume = musicVolume;
            _audioSettings.Music.Muted = musicMuted;
            _audioSettings.Sfx.Volume = sfxVolume;
            _audioSettings.Sfx.Muted = sfxMuted;

            // Save settings on change
            _audioSettings.Save();
        }
    }
}