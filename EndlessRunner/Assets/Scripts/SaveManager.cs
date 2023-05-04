using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Serialization;

public class SaveManager : MonoBehaviour
{
    private int savedPlayerScore;
    private int savedPlayerGems;
    private int savedPlayerCoins;
    public InventoryManager inventoryManager;

    public int SavedPlayerScore { get; set; }
    public int SavedPlayerGems { get; set; }
    public int SavedPlayerCoins { get; set; }
    public int SavedHighScore { get; set; }

    private void Awake() => LoadData();

    public void LoadData()
    {
        string path = Application.persistentDataPath + "/save.json";
        if (!File.Exists(path)) return;
        string json = File.ReadAllText(path);
        SaveData saveData = JsonUtility.FromJson<SaveData>(json);
        SavedPlayerScore = saveData.playerScore;
        SavedPlayerGems = saveData.playerGems;
        SavedPlayerCoins = saveData.playerCoins;
        SavedHighScore = saveData.playerHighScore;
        inventoryManager.inventory = saveData.inventory;
    }

    private void SaveData()
    {
        SaveData saveData = new SaveData
        {
            playerScore = SavedPlayerScore,
            playerGems = SavedPlayerGems,
            playerCoins = SavedPlayerCoins,
            playerHighScore = SavedHighScore,
            inventory = inventoryManager.inventory,
        };

        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(Application.persistentDataPath + "/save.json", json);
    }

    public void SaveGameData()
    {
        SaveData();
    }
}

[System.Serializable]
public class SaveData
{
    public int playerScore;
    public int playerGems;
    public int playerCoins;
    public int playerHighScore;
    public List<InventoryItem> inventory;
}