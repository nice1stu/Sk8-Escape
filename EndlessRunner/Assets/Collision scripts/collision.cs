using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Player;
using UnityEngine;
using UnityEngine.UI;

public class collision : MonoBehaviour
{
    
    public CameraShake cameraShake;
    public PlayerModel life;
    

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.gameObject.layer == LayerMask.NameToLayer("WallObstacles"))
        {
            life.isAlive = false;
           StartCoroutine(cameraShake.Shake(.13f,0.6f));
           StartCoroutine(DelayCoroutine(1.0f));
           
           
        }
    }

    void AfterDeath()
    {
        


    }

   
    
    IEnumerator DelayCoroutine(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        AfterDeath();
    }
    
}
