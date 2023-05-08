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
        UserData newUserData = new UserData(Name.text, Int32.Parse(Gold.text)); 
        // converts to json
        string json = JsonUtility.ToJson(newUserData);

        // organizing users stats in firebase
        datareference.Child("user").Child(userID).SetRawJsonValueAsync(json);
    }
    
}
