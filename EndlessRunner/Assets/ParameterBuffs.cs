using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParameterBuffs : MonoBehaviour
{
    [Range(0,8)]
    public float Vision;
    [Range(0,100)]
    public float SurviveRate;
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
        FindObjectOfType<CameraController>().offset += new Vector3(Vision, 0, 0);
        FindObjectOfType<collision>().survivalRate += SurviveRate;
    }
}
