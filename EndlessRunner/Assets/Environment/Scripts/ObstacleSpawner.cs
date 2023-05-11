using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    public GameObject[] powerUpPrefabs;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == 8 )
        {
            SpawnObstacle();
            SpawnPowerUp();
        }
    }

    public void SpawnObstacle()
    {
        
        transform.position = transform.position + new Vector3(20, 0, 0);
        Instantiate(obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)], transform.position,
            transform.rotation);
    }

    public void SpawnPowerUp()
    {
        if (Random.Range(1, 100) <= 5) // 5% chance
        {
        transform.position = transform.position + new Vector3(20, 0, 0);
        Instantiate(powerUpPrefabs[Random.Range(0, powerUpPrefabs.Length)], transform.position,
            transform.rotation);
        }
    }
    
}
