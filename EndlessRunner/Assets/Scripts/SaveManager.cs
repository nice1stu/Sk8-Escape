using System.IO;
using UnityEngine;
<<<<<<< Updated upstream
=======
using System.Collections.Generic;

>>>>>>> Stashed changes

public class SaveManager : MonoBehaviour
{
    public int SaveTotalScore { get; set; }
    public int SaveTotalGems { get; set; }
    public int SaveTotalCoins { get; set; }
    public int SaveHighScore { get; set; }

<<<<<<< Updated upstream
    public float MusicVolume { get; set; }
    public bool MusicMute { get; set; }
    public float SfxVolume { get; set; }
    public bool SfxMute { get; set; }

    private void Awake()
    {
=======
    public float SaveMusicVolume { get; set; }
    public bool SaveMusicMute { get; set; }
    public float SaveSfxVolume { get; set; }
    public bool SaveSfxMute { get; set; }
    
    public Dictionary<int, float> SaveLootBoxCooldown
    {
        get => lootboxCooldowns;
        set => lootboxCooldowns = value;
    }

    private Dictionary<int, float> lootboxCooldowns = new Dictionary<int, float>();

    private void Awake()
    {
        // Load saved lootbox cooldowns
        if (PlayerPrefs.HasKey("lootboxCooldowns"))
        {
            string[] cooldownData = PlayerPrefs.GetString("lootboxCooldowns").Split(',');
            for (int i = 0; i < cooldownData.Length; i += 2)
            {
                int index = int.Parse(cooldownData[i]);
                float time = float.Parse(cooldownData[i + 1]);
                lootboxCooldowns[index] = time;
            }
        }
>>>>>>> Stashed changes
        LoadData();
    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + "/save.json";
        if (!File.Exists(path)) return;

        string json = File.ReadAllText(path);
        GameData data = JsonUtility.FromJson<GameData>(json);

<<<<<<< Updated upstream
        SaveTotalScore = data.playerScore;
        SaveTotalGems = data.playerGems;
        SaveTotalCoins = data.playerCoins;
        SaveHighScore = data.playerHighScore;

        MusicVolume = data.musicVolume;
        MusicMute = data.musicMute;
        SfxVolume = data.sfxVolume;
        SfxMute = data.sfxMute;
=======
        SaveTotalScore = data.totalScore;
        SaveTotalGems = data.totalGems;
        SaveTotalCoins = data.totalCoins;
        SaveHighScore = data.playerHighScore;

        SaveMusicVolume = data.musicVolume;
        SaveMusicMute = data.musicMute;
        SaveSfxVolume = data.sfxVolume;
        SaveSfxMute = data.sfxMute;
>>>>>>> Stashed changes

        UpdateAudioSettings();
    }

    public void SaveGameData()
    {
        GameData data = new GameData
        {
<<<<<<< Updated upstream
            playerScore = SaveTotalScore,
            playerGems = SaveTotalGems,
            playerCoins = SaveTotalCoins,
            playerHighScore = SaveHighScore,
            musicVolume = MusicVolume,
            musicMute = MusicMute,
            sfxVolume = SfxVolume,
            sfxMute = SfxMute,
=======
            totalScore = SaveTotalScore,
            totalGems = SaveTotalGems,
            totalCoins = SaveTotalCoins,
            playerHighScore = SaveHighScore,
            musicVolume = SaveMusicVolume,
            musicMute = SaveMusicMute,
            sfxVolume = SaveSfxVolume,
            sfxMute = SaveSfxMute,
>>>>>>> Stashed changes
        };

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/save.json", json);
    }

    private void UpdateAudioSettings()
    {
<<<<<<< Updated upstream
        AudioListener.volume = (MusicMute) ? 0f : MusicVolume;
        AudioListener.pause = MusicMute;
=======
        AudioListener.volume = (SaveMusicMute) ? 0f : SaveMusicVolume;
        AudioListener.pause = SaveMusicMute;
>>>>>>> Stashed changes
    }
}

[System.Serializable]
public class GameData
{
    public int totalScore;
    public int totalGems;
    public int totalCoins;
    public int playerHighScore;
    public float musicVolume;
    public bool musicMute;
    public float sfxVolume;
    public bool sfxMute;
<<<<<<< Updated upstream
=======

    private Dictionary<int, float> lootBoxCooldowns = new Dictionary<int, float>();

    public float GetLootBoxCooldown(int lootBoxId)
    {
        float cooldown;
        return lootBoxCooldowns.TryGetValue(lootBoxId, out cooldown) ? cooldown : 0f;
    }

    public void SetLootBoxCooldown(int lootBoxId, float cooldown)
    {
        lootBoxCooldowns[lootBoxId] = cooldown;
    }
>>>>>>> Stashed changes
}
