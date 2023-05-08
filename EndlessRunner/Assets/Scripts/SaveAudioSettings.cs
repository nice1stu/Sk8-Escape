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
    public bool musicMuted;
    public bool sfxMuted;

    private float SaveMusicVolume { get; set; }
    private bool SaveMusicMute { get; set; }
    private float SaveSfxVolume { get; set; }
    private bool SaveSfxMute { get; set; }
    private string SaveLanguageSetting { get; set; }

    private void Awake()
    {
        LoadSettingsData();
    }

    private void LoadSettingsData()
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
            musicMixer.SetFloat(musicMixerVolumeParameter, musicMuted ? -80f : Mathf.Log10(SaveMusicVolume) * 20);
        }

        if (sfxMixer != null)
        {
            sfxMixer.SetFloat(sfxMixerVolumeParameter, sfxMuted ? -80f : Mathf.Log10(SaveSfxVolume) * 20);
        }
    }
}
