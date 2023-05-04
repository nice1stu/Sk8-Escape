using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibilityPowerUpController : MonoBehaviour
{
    private PlayerScoreModel scoreModel;
    
    // Start is called before the first frame update
    void Start()
    {
        scoreModel = GameObject.FindWithTag("HUD").GetComponentInChildren<PlayerScoreModel>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
