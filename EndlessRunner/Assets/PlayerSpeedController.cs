using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class PlayerSpeedController : MonoBehaviour
{
    public int speedUpFactor = 5;
    private PlayerModel stats;
    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<PlayerModel>();
    }

    // Update is called once per frame
    void Update()
    {
        if (stats.isAlive) stats.movementSpeed += speedUpFactor * Time.deltaTime;
    }
}
