using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class collision : MonoBehaviour
{
    
    public CameraShake cameraShake;
    public PlayerModel life;
    public float survivalRate;
    public bool invincible;
    public float defaultDukeTime = 1f;

    public int invicibilityTokens = 0;
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        
        if (col.collider.gameObject.layer == LayerMask.NameToLayer("WallObstacles") &&!invincible)
        {
            float surviveFloat = Random.Range(0, 100f);
            if (invicibilityTokens > 0)
            {
                surviveFloat = -1;
                invicibilityTokens--;
            }
            if (surviveFloat<survivalRate)
            {
                Debug.Log("survived");
                Rigidbody2D rb2d = life.gameObject.GetComponent<Rigidbody2D>();
                StartCoroutine(CO_invincibilityFrames(defaultDukeTime));
                rb2d.velocity = new Vector2(rb2d.velocity.x, 20);
            }
            else
            {
                Debug.Log("dead");
                life.isAlive = false;
                StartCoroutine(cameraShake.Shake(.13f,0.6f));
                StartCoroutine(DelayCoroutine(1.0f));
            }

        }
    }

    public IEnumerator CO_invincibilityFrames(float time)
    {
        invincible = true;
        yield return new WaitForSeconds(time);
        invincible = false;
    }
    
    void AfterDeath()
    {
        SceneManager.LoadScene("GameplayResults");


    }

   
    
    IEnumerator DelayCoroutine(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        AfterDeath();
    }
    
}
