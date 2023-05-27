using Item;
using UnityEngine;
using Player;
using Stat;

public class ParameterBuffs : MonoBehaviour
{
    [Range(7,14)]
    public float vision;
    [Range(0,25)]
    public float survivalRate;
    [Range(0.6f,2.5f)]
    public float coffinTime;
    [Range(.2f,1)]
    public float grindLeniency;
    [Range(1,2f)]
    public float scoreMultiplier;

    private IStats equippedSkateboard;
    void Start()
    {
        Get();
        Buff();
    }

    void Get()
    {
        foreach (var item in Dependencies.Instance.Equipped.EquippedItems)
            if (item.ItemConfig.ItemType == ItemType.SkateBoard)
            {
                equippedSkateboard = item.TotalStats;
                break;
            }
        vision += (0.7f * equippedSkateboard.Vision);                   // 7 is middle so 7-14 = 7
        survivalRate += (0.25f * equippedSkateboard.Stability);         // survival think that's max 25% maybe to high even = 0.25
        coffinTime += (0.19f * equippedSkateboard.CoffinTimeAdded);     // rather useless feature but good for stat sink(to not get it) 0.6-2.5  // 1.9 / 10 = 0.19
        grindLeniency += (0.08f * equippedSkateboard.GrindLeniency);    // radius bigger for each point (0.2 pinpoint on rail - 1 pretty lenient) (10 points to max) 0.8 / 10 = 0.08
        scoreMultiplier += (0.1f * equippedSkateboard.ScoreMultiplier); //should maybe be max 2 to max score score 0.1 each point (10 points to max) = 0.1

    }
    void Buff()
    {
        FindObjectOfType<CameraController>().offset += new Vector3(vision, 0, 0);
        GetComponent<PlayerDeathHandler>().survivalRate += this.survivalRate;
        var playerModel = GetComponent<PlayerModel>();
        playerModel.coffinTime = coffinTime;
        playerModel.interactRadius = grindLeniency;
        FindObjectOfType<PlayerScoreModel>().multiplier = scoreMultiplier;
    }
}
