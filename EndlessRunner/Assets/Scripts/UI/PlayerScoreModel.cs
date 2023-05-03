using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScoreModel : MonoBehaviour
{
    private double score;

    private int coins;

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
        score += toAdd;
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
            return true;
        }
        else
        {
            return false;
        }
    }
}
