using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibilityPowerUpController : MonoBehaviour, IPickupable
{
    private GameObject player;

    private HUDInvincibility hudLogic;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        hudLogic = GameObject.FindWithTag("HUD").GetComponentInChildren<HUDInvincibility>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPickup()
    {
        player.GetComponent<PlayerDeathHandler>().invicibilityTokens++;
        hudLogic.SetEnabled(true);
        Destroy(gameObject);
    }
}
