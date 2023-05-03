using System.IO;
using UnityEngine;
using System.Collections.Generic;


public class SaveManager : MonoBehaviour
{
    public int SaveTotalScore { get; set; }
    public int SaveTotalGems { get; set; }
    public int SaveTotalCoins { get; set; }
    public int SaveHighScore { get; set; }

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
        LoadData();
    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + "/save.json";
        if (!File.Exists(path)) return;

        string json = File.ReadAllText(path);
        GameData data = JsonUtility.FromJson<GameData>(json);

        SaveTotalScore = data.totalScore;
        SaveTotalGems = data.totalGems;
        SaveTotalCoins = data.totalCoins;
        SaveHighScore = data.playerHighScore;

        SaveMusicVolume = data.musicVolume;
        SaveMusicMute = data.musicMute;
        SaveSfxVolume = data.sfxVolume;
        SaveSfxMute = data.sfxMute;

        UpdateAudioSettings();
    }

    public void SaveGameData()
    {
        GameData data = new GameData
        {
            totalScore = SaveTotalScore,
            totalGems = SaveTotalGems,
            totalCoins = SaveTotalCoins,
            playerHighScore = SaveHighScore,
            musicVolume = SaveMusicVolume,
            musicMute = SaveMusicMute,
            sfxVolume = SaveSfxVolume,
            sfxMute = SaveSfxMute,
        };

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/save.json", json);
    }

    private void UpdateAudioSettings()
    {
        AudioListener.volume = (SaveMusicMute) ? 0f : SaveMusicVolume;
        AudioListener.pause = SaveMusicMute;
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
}
