using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData
{
    // data we want to store to firebase
    public string username;
    public int score;   //
    public int gems;    //
    public int coins;//
    public int highscore;
    

    public UserData(string username, int score, int gems, int coins, int highscore) {
        this.username = username;
        this.score = score;
        this.gems = gems;
        this.coins = coins;
        this.highscore = highscore;

    }
}
