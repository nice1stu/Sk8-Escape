using System;
using System.Collections;
using System.Collections.Generic;
using Firebase.Database;
using UnityEngine;
using UnityEngine.UI;

public class DatabaseManager : MonoBehaviour
{
    public InputField Name;
    public InputField Gold;
    public InputField Silver;

    public Text NameText;
    public Text GoldText;
    
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
        UserData newUserData = new UserData(Name.text, int.Parse(Gold.text), int.Parse(Silver.text)); 
        // converts to json
        string json = JsonUtility.ToJson(newUserData);

        // organizing users stats in firebase and setting json variable to raw json
        datareference.Child("users").Child(userID).SetRawJsonValueAsync(json);
    }

    // retrieves user's name from Firebase database and passes it to another function as string
    public IEnumerator GetName(Action<string> onCallback)
    {
        var userNameData = datareference.Child("user").Child(userID).Child("name").GetValueAsync();
        
        // pauses the execution until database calls complete
        yield return new WaitUntil(predicate: () => userNameData.IsCompleted);
    
        if (userNameData != null)
        {
            DataSnapshot snapshot = userNameData.Result;
    
            onCallback.Invoke(snapshot.Value.ToString());
        }
    }
    
    public IEnumerator GetGold(Action<int> onCallback)
    {
        var userGoldData = datareference.Child("user").Child(userID).Child("gold").GetValueAsync();
    
        yield return new WaitUntil(predicate: () => userGoldData.IsCompleted);
    
        if (userGoldData != null)
        {
            DataSnapshot snapshot = userGoldData.Result;
    
            onCallback.Invoke(int.Parse(snapshot.Value.ToString()));
        }
    }
    
    // should retrieves name and gold and display's information ( UI )
    public void GetUserInfo()
    {
        StartCoroutine(GetName((Username) =>
        {
            NameText.text = "Name: " + Username;
        }));
        StartCoroutine(GetGold((gold) =>
        {
            GoldText.text = "Gold: " + gold;
        }));
    }

    public void UpdateName()
    {
        datareference.Child("users").Child(userID).Child("name").SetValueAsync(Name.text);
    }
    public void UpdateGold()
    {
        datareference.Child("users").Child(userID).Child("gold").SetValueAsync(Gold.text);
    }
    public void UpdateSilver()
    {
        datareference.Child("users").Child(userID).Child("silver").SetValueAsync(Silver.text);
    }
}
