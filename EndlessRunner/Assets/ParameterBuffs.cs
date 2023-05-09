using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class ParameterBuffs : MonoBehaviour
{
    [Range(0,8)]
    public float vision;
    [Range(0,100)]
    public float stability;
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
    }
}
