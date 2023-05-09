using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpDespawner : MonoBehaviour
{
    // Start is called before the first frame update
    public int lifeSpan;
    void Start()
    {
        StartCoroutine(DespawnTimer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DespawnTimer()
    {
        yield return new WaitForSeconds(lifeSpan);
        Destroy(gameObject);
    }
}
