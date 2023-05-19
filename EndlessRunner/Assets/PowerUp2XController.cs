using System;
using System.Collections;
using System.Collections.Generic;
using static Player.RunInventoryManager;
using UnityEngine;

public class PowerUp2XController : MonoBehaviour, IPickupable
{
    public int duration = 10;

    private IEnumerator timer;

    private PlayerScoreModel hudScore;
    
    private HUDX2 HUDElement;
    

    // Start is called before the first frame update
    void Start()
    {
        hudScore = GameObject.FindWithTag("HUD").GetComponentInChildren<PlayerScoreModel>();
        HUDElement = GameObject.FindWithTag("HUD").GetComponentInChildren<HUDX2>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnDisable()
    {
        if (timer != null)
        {
            StopCoroutine(timer);
        }
    }

    public void OnPickup()
    {
        timer = PowerUpCooldown();
        //StartCoroutine(timer);
        StartCoroutine(PowerUpCooldown());
        HUDElement.SetEnabled(true);
        GetComponent<SpriteRenderer>().enabled = false;
    }

    private IEnumerator PowerUpCooldown()
    {
        multiplier *= 2;
        hudScore.multiplier *= 2;
        yield return new WaitForSeconds(duration);
        hudScore.multiplier /= 2;
        multiplier /= 2;
        if (multiplier == 1)
        {
            HUDElement.SetEnabled(false);
        }
    }
}