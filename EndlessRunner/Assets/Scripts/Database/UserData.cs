using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData
{
    // data we want to store to firebase
    public string username;
    public int gold;

    public UserData(string username, int gold) {
        this.username = username;
        this.gold = gold;
    }
}
