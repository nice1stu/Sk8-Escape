using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibilityPowerUpController : MonoBehaviour, IPickupable
{
    private GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPickup()
    {
        player.GetComponent<collision>().invicibilityTokens++;
        Destroy(gameObject);
    }
}
