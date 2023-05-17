using System;
using System.Collections;
using System.Collections.Generic;
using Firebase.Database;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class DatabaseManager : MonoBehaviour
{
    public InputField Name;
    public InputField ScoreInput;
    public InputField GemsInput;
    public InputField Coins;
    public InputField HighscoreInput;

    public Text NameText;
    public Text ScoreText;
    public Text GemsText;
    public Text CoinText;
    public Text HighscoreText;
    
    private string userID;  // userID is gonna show in firebase
    private DatabaseReference datareference;
    // Start is called before the first frame update
    void Start()
    {
        // identifying device
        userID = SystemInfo.deviceUniqueIdentifier;
        // Get the root reference location of the database
        datareference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void CreateUser()
    {
        // creates new name and gold
        UserData newUserData = new UserData(
            Name.text,
            int.Parse(ScoreInput.text), 
            int.Parse(GemsInput.text), 
            int.Parse(Coins.text), 
            int.Parse(HighscoreInput.text)
        ); 
        // converts to json
        string json = JsonUtility.ToJson(newUserData);

        // organizing users stats in firebase and setting json variable to raw json
        datareference.Child("users").Child(userID).SetRawJsonValueAsync(json);
    }

    // retrieves user's name from Firebase database and passes it to another function as string
    public IEnumerator GetName(Action<string> onCallback)
    {
        var userNameData = datareference.Child("users").Child(userID).Child("username").GetValueAsync();
        
        // pauses the execution until database calls complete
        yield return new WaitUntil(predicate: () => userNameData.IsCompleted);

        if (userNameData.Exception != null)
        {
            Debug.LogError($"Failed to retrieve name data {userNameData.Exception}");
        }
        
        DataSnapshot snapshot = userNameData.Result;

        if (snapshot != null && snapshot.Exists)
        {
            onCallback.Invoke(snapshot.Value.ToString());
        }
        else
        {
            Debug.LogError("Name data does not exist.");
        }
    }
    
    public IEnumerator GetScore(Action<int> onCallback)
    {
        var userScoreData = datareference.Child("users").Child(userID).Child("score").GetValueAsync();
    
        yield return new WaitUntil(predicate: () => userScoreData.IsCompleted);

        if (userScoreData.Exception != null)
        {
            Debug.LogError($"Failed to retrieve score data: {userScoreData.Exception}");
        }

        DataSnapshot snapshot = userScoreData.Result;
        if (snapshot != null && snapshot.Exists)
        {
            onCallback.Invoke(int.Parse(snapshot.Value.ToString()));
        }
        else
        {
            Debug.LogError("Score data does not exist.");
        }
    }
    public IEnumerator GetGems(Action<int> onCallback)
    {
        var userGemsData = datareference.Child("users").Child(userID).Child("gems").GetValueAsync();
    
        yield return new WaitUntil(predicate: () => userGemsData.IsCompleted);

        if (userGemsData.Exception != null)
        {
            Debug.LogError($"Failed to retrieve gems data: {userGemsData.Exception}");
        }

        DataSnapshot snapshot = userGemsData.Result;
        if (snapshot != null && snapshot.Exists)
        {
            onCallback.Invoke(int.Parse(snapshot.Value.ToString()));
        }
        else
        {
            Debug.LogError("Gem data does not exist.");
        }
    }
    public IEnumerator GetCoins(Action<int> onCallback)
    {
        var userCoinsData = datareference.Child("users").Child(userID).Child("coins").GetValueAsync();
    
        yield return new WaitUntil(predicate: () => userCoinsData.IsCompleted);

        if (userCoinsData.Exception != null)
        {
            Debug.LogError($"Failed to retrieve gold data: {userCoinsData.Exception}");
        }

        DataSnapshot snapshot = userCoinsData.Result;
        if (snapshot != null && snapshot.Exists)
        {
            onCallback.Invoke(int.Parse(snapshot.Value.ToString()));
        }
        else
        {
            Debug.LogError("Coin data does not exist.");
        }
    }
    public IEnumerator GetHighScore(Action<int> onCallback)
    {
        var userCoinsData = datareference.Child("users").Child(userID).Child("highscore").GetValueAsync();
    
        yield return new WaitUntil(predicate: () => userCoinsData.IsCompleted);

        if (userCoinsData.Exception != null)
        {
            Debug.LogError($"Failed to retrieve gold data: {userCoinsData.Exception}");
        }

        DataSnapshot snapshot = userCoinsData.Result;
        if (snapshot != null && snapshot.Exists)
        {
            onCallback.Invoke(int.Parse(snapshot.Value.ToString()));
        }
        else
        {
            Debug.LogError("Coin data does not exist.");
        }
    }
    
    // should retrieves name and gold and display's information ( UI )
    public void GetUserInfo()
    {
        StartCoroutine(GetName((string name) =>
        {
            NameText.text = "Name: " + name.ToString();
        }));
        StartCoroutine(GetScore((int score) =>
        {
            ScoreText.text = "Score: " + score.ToString();
        }));
        StartCoroutine(GetCoins((int coins) =>
        {
            CoinText.text = "Coins: " + coins.ToString();
        }));
        StartCoroutine(GetGems((int gems) =>
        {
            GemsText.text = "Gems: " + gems.ToString();
        }));
        StartCoroutine(GetHighScore((int highscore) =>
        {
            HighscoreText.text = "Highscore: " + highscore.ToString();
        }));
    }

    // updating the data
    public void UpdateName()
    {
        datareference.Child("users").Child(userID).Child("username").SetValueAsync(Name.text);
    }
    public void UpdateScore()
    {
        datareference.Child("users").Child(userID).Child("score").SetValueAsync(ScoreInput.text);
    }
    public void UpdateGems()
    {
        datareference.Child("users").Child(userID).Child("gems").SetValueAsync(GemsInput.text);
    }
    public void UpdateCoins()
    {
        datareference.Child("users").Child(userID).Child("coins").SetValueAsync(Coins.text);
    }
    public void UpdateHighScore()
    {
        datareference.Child("users").Child(userID).Child("highscore").SetValueAsync(HighscoreInput.text);
    }
}
