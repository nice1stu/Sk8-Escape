using System;
using System.IO;
using Firebase.Auth;
using UnityEngine;
using Firebase.Database;

public class SaveManager : MonoBehaviour
{
    public static int SaveTotalScore { get; set; }
    public static int SaveTotalGems { get; set; }
    public static int SaveTotalCoins { get; set; }
    public static int SaveHighScore { get; set; }

    //public Countdown[] currentBoxes = new Countdown[4]; for testing
    // private Countdown[] LootBoxes => LoadLootBoxData();

    // private void SaveLootBoxData(Countdown[] lootBoxes)
    // {
    //     var data = lootBoxes;
    //     var json = JsonUtility.ToJson(data);
    //     File.WriteAllText(Application.persistentDataPath + "/lootBoxes.save.json", json);
    //     var username = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
    //     FirebaseDatabase.DefaultInstance.RootReference.Child("lootBoxes").Child(username).SetRawJsonValueAsync(json);
    // }
    //
    // private Countdown[] LoadLootBoxData()
    // {
    //     var path = Application.persistentDataPath + "/lootBoxes.save.json";
    //     if (!File.Exists(path)) return null;
    //
    //     var json = File.ReadAllText(path);
    //     var data = JsonUtility.FromJson<Countdown[]>(json);
    //     return data;
    // }

    public static void SaveGameData()
    {
        string _username = String.Empty;
#if UNITY_ANDROID
        _username = GooglePlayGames.PlayGamesPlatform.Instance.localUser.userName;
#endif
        if(_username == String.Empty) _username = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
        GameData data = new GameData
        {
            username = _username,
            totalScore = SaveTotalScore,
            totalGems = SaveTotalGems,
            totalCoins = SaveTotalCoins,
            playerHighScore = SaveHighScore,
            timeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
        };

        var json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/stats.save.json", json);
        //no user name means it will overwrite the entire cloud data
        if (_username == String.Empty) return; 
        FirebaseDatabase.DefaultInstance.RootReference
            .Child("users")
            .Child(_username)
            .SetRawJsonValueAsync(json);

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