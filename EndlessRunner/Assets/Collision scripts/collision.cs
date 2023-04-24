using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class collision : MonoBehaviour
{
    public GameObject spawnPoint;
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.gameObject.layer == LayerMask.NameToLayer("WallObstacles"))
        {
            Death();
        }
    }

    void Death()
    {
        transform.position = spawnPoint.transform.position;
    }
    
}
