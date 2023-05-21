using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    public GameObject[] powerUpPrefabs;

    private GameObject currObstacleWidth;
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
        currObstacleWidth = Instantiate(obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)], transform.position,
            transform.rotation);
        Debug.Log(GetObstacleWidth(currObstacleWidth) +" is the bounds size");
        currObstacleWidth.transform.position += new Vector3(GetObstacleWidth(currObstacleWidth), 0, 0);
        transform.position += new Vector3(GetObstacleWidth(currObstacleWidth)+8, 0, 0);
    }

    public void SpawnPowerUp()
    {
        if (Random.Range(1, 100) <= 5) // 5% chance
        {
            Debug.Log("im spawning now!");
            transform.position = transform.position + new Vector3(GetObstacleWidth(currObstacleWidth), 0, 0);
            Instantiate(powerUpPrefabs[Random.Range(0, powerUpPrefabs.Length)], transform.position,
            transform.rotation);
        }
    }
    
}
