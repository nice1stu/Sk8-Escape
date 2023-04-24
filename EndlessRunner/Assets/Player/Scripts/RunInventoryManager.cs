using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunInventoryManager : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Coin"))
        {
            Debug.Log("Nice!!!");
        }
    }
}
