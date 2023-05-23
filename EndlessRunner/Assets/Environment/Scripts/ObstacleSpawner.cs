using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    public GameObject[] powerUpPrefabs;

    private GameObject _currObstacleSpawned;
    [SerializeField] float _obstacleWidth;

    private void Start()
    {
        // spawn the first obstacle so it wont be null,  check the width and move obstacle spawner the width
        _currObstacleSpawned = Instantiate(obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)], transform.position, transform.rotation);
        _obstacleWidth = GetObstacleWidth(_currObstacleSpawned);
        transform.position += new Vector3(_obstacleWidth, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == 10 )
        {
            SpawnObstacle();
            SpawnPowerUp();
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.layer == 7)
        {
            //transform.position += new Vector3(1, 0, 0);
        }
    }

    private float GetObstacleWidth(GameObject obstacle)
    {
        float minX = Mathf.Infinity;
        float maxX = Mathf.NegativeInfinity;


        foreach (Transform child in obstacle.transform)
        {
            SpriteRenderer spriteRenderer = child.GetComponent<SpriteRenderer>();
            
            if (spriteRenderer != null)
            {
                var bounds = spriteRenderer.bounds;
                Vector3 boundsMin = bounds.min;
                Vector3 boundsMax = bounds.max;

                if (boundsMin.x < minX)
                    minX = boundsMin.x;

                if (boundsMax.x > maxX)
                    maxX = boundsMax.x;
            }
            //Yeah this is ugly but it works
            foreach (Transform youngerChild in child.transform)
            {
                SpriteRenderer youngerSpriteRenderer = youngerChild.GetComponent<SpriteRenderer>();
            
                if (youngerSpriteRenderer != null)
                {
                    var bounds = youngerSpriteRenderer.bounds;
                    Vector3 boundsMin = bounds.min;
                    Vector3 boundsMax = bounds.max;

                    if (boundsMin.x < minX)
                        minX = boundsMin.x;

                    if (boundsMax.x > maxX)
                        maxX = boundsMax.x;
                }
            }
        }

        return maxX - minX;
    }
    public void SpawnObstacle()
    {
        //checks the length once and reuses it until next obstacle spawns
        _obstacleWidth = GetObstacleWidth(_currObstacleSpawned);
        // Instantiates and saves the gameObject as currObstacle (position on obstaclespawner + 8 buffer)
        _currObstacleSpawned = Instantiate(obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)], transform.position + new Vector3(8,0,0), transform.rotation);
        //move the obstacleSpawner for obstacleWidth + 8 for buffer (else we would see it spawn in)
        transform.position += new Vector3(_obstacleWidth+8 , 0, 0);
        
    }

    public void SpawnPowerUp()
    {
        if (Random.Range(1, 100) <= 5) // 5% chance
        {
            //Same here instantiate the powerUp at obstacleSpawner ( the buffer 1.5f was between the obstacles)
            Instantiate(powerUpPrefabs[Random.Range(0, powerUpPrefabs.Length)], transform.position + new Vector3(1.5f,0,0), transform.rotation);
        }
    }
    
}
