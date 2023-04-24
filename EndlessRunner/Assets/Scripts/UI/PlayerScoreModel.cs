using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScoreModel : MonoBehaviour
{
    private double score;
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
}
