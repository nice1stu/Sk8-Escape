using UnityEngine;
using UnityEngine.Audio;

public class SaveAudioSettings : MonoBehaviour
{
    private const string MusicVolumeKey = "MusicVolume";
    private const string MusicMuteKey = "MusicMute";
    private const string SfxVolumeKey = "SfxVolume";
    private const string SfxMuteKey = "SfxMute";
    private const string LanguageSettingKey = "LanguageSetting";

    public AudioMixer musicMixer;
    public AudioMixer sfxMixer;
    public string musicMixerVolumeParameter = "MasterVolume";
    public string sfxMixerVolumeParameter = "MasterVolume";
    public bool musicMuted = false;
    public bool sfxMuted = false;

    private float SaveMusicVolume { get; set; }
    private bool SaveMusicMute { get; set; }
    public float SaveSfxVolume { get; set; }
    public bool SaveSfxMute { get; set; }
    public string SaveLanguageSetting { get; set; }

    private void Awake()
    {
        LoadSettingsData();
    }

    public void LoadSettingsData()
    {
        SaveMusicVolume = PlayerPrefs.GetFloat(MusicVolumeKey, 1.0f);
        SaveMusicMute = PlayerPrefs.GetInt(MusicMuteKey, 0) == 1;
        SaveSfxVolume = PlayerPrefs.GetFloat(SfxVolumeKey, 1.0f);
        SaveSfxMute = PlayerPrefs.GetInt(SfxMuteKey, 0) == 1;
        SaveLanguageSetting = PlayerPrefs.GetString(LanguageSettingKey, "en");

        UpdateAudioSettings();
    }

    public void SaveSettingsData()
    {
        PlayerPrefs.SetFloat(MusicVolumeKey, SaveMusicVolume);
        PlayerPrefs.SetInt(MusicMuteKey, SaveMusicMute ? 1 : 0);
        PlayerPrefs.SetFloat(SfxVolumeKey, SaveSfxVolume);
        PlayerPrefs.SetInt(SfxMuteKey, SaveSfxMute ? 1 : 0);
        PlayerPrefs.SetString(LanguageSettingKey, SaveLanguageSetting);
    }

    private void UpdateAudioSettings()
    {
        if (musicMixer != null)
        {
            if (musicMuted)
            {
                musicMixer.SetFloat(musicMixerVolumeParameter, -80f);
            }
            else
            {
                musicMixer.SetFloat(musicMixerVolumeParameter, Mathf.Log10(SaveMusicVolume) * 20);
            }
        }

        if (sfxMixer != null)
        {
            if (sfxMuted)
            {
                sfxMixer.SetFloat(sfxMixerVolumeParameter, -80f);
            }
            else
            {
                sfxMixer.SetFloat(sfxMixerVolumeParameter, Mathf.Log10(SaveSfxVolume) * 20);
            }
        }
    }
}
