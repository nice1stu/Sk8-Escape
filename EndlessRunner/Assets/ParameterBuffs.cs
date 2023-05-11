using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class ParameterBuffs : MonoBehaviour
{
    [Range(0,8)]
    public float vision;
    [Range(0,100)]
    public float stability;
    [Range(0.6f,2.5f)]
    public float coffinTime;
    [Range(.2f,1)]
    public float grindLeniency;
    [Range(1,3f)]
    public float scoreMultiplier;
    void Start()
    {
        Get();
        Buff();
    }

    void Get()
    {
        
    }
    void Buff()
    {
        FindObjectOfType<CameraController>().offset += new Vector3(vision, 0, 0);
        GetComponent<PlayerDeathHandler>().survivalRate += stability;
        var playerModel = GetComponent<PlayerModel>();
        playerModel.coffinTime = coffinTime;
        playerModel.interactRadius = grindLeniency;
        FindObjectOfType<PlayerScoreModel>().multiplier = scoreMultiplier;
    }
}
