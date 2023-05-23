using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScoreModel : MonoBehaviour
{
    private static double score;

    public float multiplier = 1;

    private static int coins;

    //private int invincibilityPoints = 0;
    
    private int powerUps;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetScore(double newScore)
    {
        score = newScore;
    }

    public void AddToScore(double toAdd)
    {
        score += toAdd;
    }
    
    public void AddToScore(float toAdd)
    {
        score += (toAdd * multiplier);
    }

    public double GetScore()
    {
        return score;
    }

    public void SetCoins(int newCoins)
    {
        coins = newCoins;
    }

    public void AddCoins(int toAdd)
    {
        coins += toAdd;
    }

    public int GetCoins()
    {
        return coins;
    }

    public void AddPowerUp()
    {
        powerUps++;
    }

    public bool TryToUsePowerUp()
    {
        if (powerUps > 0)
        {
            powerUps--;
            if (powerUps == 0)
            {
                HUDSlowmo hudLogic = GameObject.FindWithTag("HUD").GetComponentInChildren<HUDSlowmo>();
                hudLogic.SetEnabled(false);
            }
            return true;
        }
        else
        {
            return false;
        }
    }
}
