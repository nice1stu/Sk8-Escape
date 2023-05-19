using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerDeathHandler : MonoBehaviour
{
    public CameraShake cameraShake;
    public PlayerModel life;
    public float survivalRate;
    public bool invincible;
    public float defaultDukeTime = 1f;
    public GameObject gamePlayResults;
    public GameObject pauseButton;
    public int invicibilityTokens = 0;

    private bool ranRecently = false;

    private HUDInvincibility hudLogic;

    private void Start()
    {
        hudLogic = GameObject.FindWithTag("HUD").GetComponentInChildren<HUDInvincibility>();
    }

    public bool OnDeath()
    {
        if (ranRecently == true)
        {
            return false;
        }

        ranRecently = true;
        
        StartCoroutine(Cooldown());
        
        
        Debug.Log("Did we run?");
        
        float surviveFloat = Random.Range(0, 100f);
        if (invicibilityTokens > 0)
        {
            surviveFloat = -1;
            invicibilityTokens--;
            if (invicibilityTokens == 0)
            {
                hudLogic.SetEnabled(false);
            }
        }

        if (surviveFloat < survivalRate)
        {
            Debug.Log("survived");
            Rigidbody2D rb2d = life.gameObject.GetComponent<Rigidbody2D>();
            StartCoroutine(CO_invincibilityFrames(defaultDukeTime));
            rb2d.velocity = new Vector2(rb2d.velocity.x, 20);
            return false;
        }
        else
        {
            Debug.Log("dead");
            life.isAlive = false;
            if (cameraShake != null)
                StartCoroutine(cameraShake.Shake(.13f, 0.6f));
            StartCoroutine(DelayCoroutine(1.0f));
            GetComponent<PlayerController>().CancelSlowmo();
            return true;
        }
    }

    public IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(1);
        ranRecently = false;
    }

    public IEnumerator CO_invincibilityFrames(float time)
    {
        Physics.IgnoreLayerCollision(8, 7);
        Physics.IgnoreLayerCollision(7, 8);
        invincible = true;
        yield return new WaitForSeconds(time);
        invincible = false;
    }

    void AfterDeath()
    {
        gamePlayResults.SetActive(true);
        pauseButton.SetActive(false);
    }


    IEnumerator DelayCoroutine(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        AfterDeath();
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Did we actually run this collision check?");
        if (collision.gameObject.layer == 8)
        {
            Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), gameObject.GetComponent<Collider>());
        }
    }
}