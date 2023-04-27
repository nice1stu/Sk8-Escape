using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

public class collision : MonoBehaviour
{
    public GameObject spawnPoint;
    public CameraShake cameraShake;
    public Image darkBackground;

    private void Start()
    {
        darkBackground.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.gameObject.layer == LayerMask.NameToLayer("WallObstacles"))
        {
            darkBackground.enabled = true;
            //gameObject.GetComponent<MeshRenderer>().enabled = false;
           StartCoroutine(cameraShake.Shake(.13f,0.6f));
           StartCoroutine(DelayCoroutine(1.0f));
           
        }
    }

    void Death()
    {
       // gameObject.GetComponent<MeshRenderer>().enabled = true;
       darkBackground.enabled = false;
        transform.position = spawnPoint.transform.position;
        
    }
    
    IEnumerator DelayCoroutine(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        Death();
        
    }
    
}
