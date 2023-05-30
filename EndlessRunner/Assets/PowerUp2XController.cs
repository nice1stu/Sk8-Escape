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
    
    private void OnDisable()
    {
        if (timer != null)
        {
            StopCoroutine(timer);
        }
        // Need this!. Because if we die before timer runs out and exit fast it's always on x2. 
        hudScore.multiplier = 1;
        multiplier = 1;
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
        //if we want multiple x2 use this
        //multiplier *= 2;
        //hudScore.multiplier *= 2;
        
        //But we just want the x2 and if we get it again resets the timer and keeps x2 going instead of increasing it to x4
        multiplier = 2;
        hudScore.multiplier = 2;
        yield return new WaitForSeconds(duration);
        //hudScore.multiplier /= 2;
        //multiplier /= 2;
        
        //same as above
        hudScore.multiplier = 1;
        multiplier = 1;
        if (multiplier == 1)
        {
            HUDElement.SetEnabled(false);
        }
    }
}