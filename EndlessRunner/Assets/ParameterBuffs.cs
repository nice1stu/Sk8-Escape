using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParameterBuffs : MonoBehaviour
{
    [Range(0,8)]
    public float vision;
    [Range(0,100)]
    public float stability;
    [Range(0.6f,2.5f)]
    public float coffinTime;
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
        FindObjectOfType<collision>().survivalRate += stability;
        FindObjectOfType<PlayerModel>().coffinTime = coffinTime;
    }
}
