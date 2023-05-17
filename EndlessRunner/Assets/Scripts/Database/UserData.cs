using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData
{
    // data we want to store to firebase
    public string username;
    public int gold;
    public int silver;
    

    public UserData(string username, int gold, int silver) {
        this.username = username;
        this.gold = gold;
        this.silver = silver;
        
    }
}
