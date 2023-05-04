using System.IO;
using UnityEngine;


public class SaveManager : MonoBehaviour
{
    public int SaveTotalScore { get; set; }
    public int SaveTotalGems { get; set; }
    public int SaveTotalCoins { get; set; }
    public int SaveHighScore { get; set; }

    private MonoBehaviour[] LootBoxes => LoadLootBoxData();

    private void SaveLootBoxData(MonoBehaviour[] lootBoxes)
    {
        MonoBehaviour[] data = lootBoxes;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/lootBoxes.save.json", json);
    }

    private MonoBehaviour[] LoadLootBoxData()
    {
        string path = Application.persistentDataPath + "/lootBoxes.save.json";
        if (!File.Exists(path)) return null;

        string json = File.ReadAllText(path);
        MonoBehaviour[] data = JsonUtility.FromJson<MonoBehaviour[]>(json);
        return data;
    }
    private void Awake()
    {
        LoadData();
    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + "/stats.save.json";
        if (!File.Exists(path)) return;

        string json = File.ReadAllText(path);
        GameData data = JsonUtility.FromJson<GameData>(json);

        SaveTotalScore = data.totalScore;
        SaveTotalGems = data.totalGems;
        SaveTotalCoins = data.totalCoins;
        SaveHighScore = data.playerHighScore;
    }

    public void SaveGameData()
    {
        GameData data = new GameData
        {
            totalScore = SaveTotalScore,
            totalGems = SaveTotalGems,
            totalCoins = SaveTotalCoins,
            playerHighScore = SaveHighScore,
            
        };

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/stats.save.json", json);
    }
}

[System.Serializable]
public class GameData
{
    public int totalScore;
    public int totalGems;
    public int totalCoins;
    public int playerHighScore;
}
