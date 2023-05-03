using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public int SaveTotalScore { get; set; }
    public int SaveTotalGems { get; set; }
    public int SaveTotalCoins { get; set; }
    public int SaveHighScore { get; set; }

    public float MusicVolume { get; set; }
    public bool MusicMute { get; set; }
    public float SfxVolume { get; set; }
    public bool SfxMute { get; set; }

    private void Awake()
    {
        LoadData();
    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + "/save.json";
        if (!File.Exists(path)) return;

        string json = File.ReadAllText(path);
        GameData data = JsonUtility.FromJson<GameData>(json);

        SaveTotalScore = data.playerScore;
        SaveTotalGems = data.playerGems;
        SaveTotalCoins = data.playerCoins;
        SaveHighScore = data.playerHighScore;

        MusicVolume = data.musicVolume;
        MusicMute = data.musicMute;
        SfxVolume = data.sfxVolume;
        SfxMute = data.sfxMute;

        UpdateAudioSettings();
    }

    public void SaveGameData()
    {
        GameData data = new GameData
        {
            playerScore = SaveTotalScore,
            playerGems = SaveTotalGems,
            playerCoins = SaveTotalCoins,
            playerHighScore = SaveHighScore,
            musicVolume = MusicVolume,
            musicMute = MusicMute,
            sfxVolume = SfxVolume,
            sfxMute = SfxMute,
        };

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/save.json", json);
    }

    private void UpdateAudioSettings()
    {
        AudioListener.volume = (MusicMute) ? 0f : MusicVolume;
        AudioListener.pause = MusicMute;
    }
}

[System.Serializable]
public class GameData
{
    public int playerScore;
    public int playerGems;
    public int playerCoins;
    public int playerHighScore;
    public float musicVolume;
    public bool musicMute;
    public float sfxVolume;
    public bool sfxMute;
}
