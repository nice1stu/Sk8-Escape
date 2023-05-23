using System;
using System.Collections;
using System.IO;
using Inventory.Scripts;
using UnityEngine;
using Firebase.Database;
using GooglePlayGames;
using UI.Scripts;
using UnityEngine.Serialization;

public class SaveManager : MonoBehaviour
{
    public int SaveTotalScore { get; set; }
    public int SaveTotalGems { get; set; }
    public int SaveTotalCoins { get; set; }
    public int SaveHighScore { get; set; }

    
    //public Countdown[] currentBoxes = new Countdown[4]; for testing
    private Countdown[] LootBoxes => LoadLootBoxData();
    
    private long onlineTimeStamp;
    private long localTimeStamp;
    private GameData onlineData;
    private void Awake()
    {
        FirebaseDatabase.DefaultInstance.SetPersistenceEnabled(false);
        StartCoroutine(GetStats());
        LoadData();
    }

    private void SaveLootBoxData(Countdown[] lootBoxes)
    {
        var data = lootBoxes;
        var json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/lootBoxes.save.json", json);
        FirebaseDatabase.DefaultInstance.RootReference.Child("lootBoxes").Child("player").SetRawJsonValueAsync(json);
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
       
        if (File.Exists(path))
        {
            var json = File.ReadAllText(path);
            var localData = JsonUtility.FromJson<GameData>(json);
            localTimeStamp = localData.timeStamp;

            //get the most up to date data stats
            if (localTimeStamp > onlineTimeStamp)
            {
                LoadSaveFile(localData);
            }
            else
            {
                LoadSaveFile(onlineData);
                UpdateUIAndSave();
            }
        }
    }

    private void LoadSaveFile(GameData data)
    {
        SaveTotalScore = data.totalScore;
        SaveTotalGems = data.totalGems;
        SaveTotalCoins = data.totalCoins;
        SaveHighScore = data.playerHighScore;
    }

    private void UpdateUIAndSave()
    {
        UIManager uiManager = FindObjectOfType<UIManager>();
        if (uiManager) uiManager.UpdateUI();

        //save the online data on device to keep it up to date
        SaveGameData();
    }

    public void SaveGameData()
    {
        GameData data = new GameData
        {
            username = PlayGamesPlatform.Instance.localUser.userName,
            totalScore = SaveTotalScore,
            totalGems = SaveTotalGems,
            totalCoins = SaveTotalCoins,
            playerHighScore = SaveHighScore,
            timeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
        };

        var json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/stats.save.json", json);
        if(data.username != String.Empty) FirebaseDatabase.DefaultInstance.RootReference.Child("users").Child(data.username).SetRawJsonValueAsync(json);

    }
    
    //get online stats
    public IEnumerator GetStats()
    {
        yield return new WaitForSeconds(1);
        var userData = FirebaseDatabase.DefaultInstance.RootReference.Child("users").Child(PlayGamesPlatform.Instance.localUser.userName).GetValueAsync();
        yield return new WaitUntil(predicate: () => userData.IsCompleted);
        
        DataSnapshot snapshot = userData.Result;
        if (snapshot != null && snapshot.Exists)
        {
            onlineData = JsonUtility.FromJson<GameData>(snapshot.GetRawJsonValue());
            onlineTimeStamp = onlineData.timeStamp;
            //if the local data doesnt exist load online data
            if (localTimeStamp == 0)
            {
                LoadSaveFile(onlineData);
                UpdateUIAndSave();
            }
            else LoadData();
        }
    }
}

[Serializable]
public class GameData
{
    public string username;
    public int totalScore;
    public int totalGems;
    public int totalCoins;
    public int playerHighScore;
    public long timeStamp;
}