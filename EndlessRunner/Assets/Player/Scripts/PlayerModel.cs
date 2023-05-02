using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    [Header("General Movement")]
    public float movementSpeed = 5f;
    [Header("Tricks")]
    public float ollieJumpForce = 5f;
    public float kickflipJumpForce = 3f;
    public float shuvitJumpForce = 2f;
    public float coffinTime = 1f;
    [Header("State Conditions")] 
    public float initialFallingVelocity = 0.05f;
    [Header("Collisions")] 
    public float maxGroundAngle = 45f;
    public LayerMask groundLayers;
    [Header("Other")]
    public bool isAlive;
}
