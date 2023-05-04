using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, IPickupable
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

    public void OnPickup()
    {
        
        scoreModel.AddCoins(1);
        Destroy(gameObject);
    }
}