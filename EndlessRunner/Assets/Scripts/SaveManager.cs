using System;
using System.IO;
using Inventory.Scripts;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public int SaveTotalScore { get; set; }
    public int SaveTotalGems { get; set; }
    public int SaveTotalCoins { get; set; }
    public int SaveHighScore { get; set; }

    //public Countdown[] currentBoxes = new Countdown[4]; for testing
    private Countdown[] LootBoxes => LoadLootBoxData();

    private void Awake()
    {
        LoadData();
    }

    private void SaveLootBoxData(Countdown[] lootBoxes)
    {
        var data = lootBoxes;
        var json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/lootBoxes.save.json", json);
    }

    private Countdown[] LoadLootBoxData()
    {
        var path = Application.persistentDataPath + "/lootBoxes.save.json";
        if (!File.Exists(path)) return null;

        var json = File.ReadAllText(path);
        var data = JsonUtility.FromJson<Countdown[]>(json);
        return data;
    }

    public void LoadData()
    {
        var path = Application.persistentDataPath + "/stats.save.json";
        if (!File.Exists(path)) return;

        var json = File.ReadAllText(path);
        var data = JsonUtility.FromJson<GameData>(json);

        SaveTotalScore = data.totalScore;
        SaveTotalGems = data.totalGems;
        SaveTotalCoins = data.totalCoins;
        SaveHighScore = data.playerHighScore;
    }

    public void SaveGameData()
    {
        var data = new GameData
        {
            totalScore = SaveTotalScore,
            totalGems = SaveTotalGems,
            totalCoins = SaveTotalCoins,
            playerHighScore = SaveHighScore
        };

        var json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/stats.save.json", json);
    }
}

[Serializable]
public class GameData
{
    public int totalScore;
    public int totalGems;
    public int totalCoins;
    public int playerHighScore;
}