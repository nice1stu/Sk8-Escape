using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParameterBuffs : MonoBehaviour
{
    [Range(0,8)]
    public float Vision;
    [Range(0,50)]
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
        
    }
}
