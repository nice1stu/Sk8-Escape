using System.IO;
using UnityEngine;

public class SaveSettings : MonoBehaviour
{
    public float SaveMusicVolume { get; set; }
    public bool SaveMusicMute { get; set; }
    public float SaveSfxVolume { get; set; }
    public bool SaveSfxMute { get; set; }
    
    private void Awake()
    {
        LoadSettingsData();
    }

    public void LoadSettingsData()
    {
        string path = Application.persistentDataPath + "/settings.save.json";
        if (!File.Exists(path)) return;

        string json = File.ReadAllText(path);
        PlayerSettingsData settingsData = JsonUtility.FromJson<PlayerSettingsData>(json);

        SaveMusicVolume = settingsData.musicVolume;
        SaveMusicMute = settingsData.musicMute;
        SaveSfxVolume = settingsData.sfxVolume;
        SaveSfxMute = settingsData.sfxMute;

        UpdateAudioSettings();
    }

    public void SaveSettingsData()
    {
        PlayerSettingsData settingsData = new PlayerSettingsData
        {
            musicVolume = SaveMusicVolume,
            musicMute = SaveMusicMute,
            sfxVolume = SaveSfxVolume,
            sfxMute = SaveSfxMute,
        };

        string json = JsonUtility.ToJson(settingsData);
        File.WriteAllText(Application.persistentDataPath + "/settings.save.json", json);
    }

    private void UpdateAudioSettings()
    {
        AudioListener.volume = (SaveMusicMute) ? 0f : SaveMusicVolume;
        AudioListener.pause = SaveMusicMute;
    }
}

[System.Serializable]
public class PlayerSettingsData
{
    public float musicVolume;
    public bool musicMute;
    public float sfxVolume;
    public bool sfxMute;
}
