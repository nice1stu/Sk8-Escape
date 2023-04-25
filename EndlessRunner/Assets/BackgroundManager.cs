using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    public bool first;

    private void OnTriggerExit2D(Collider2D other)
    {
        first = false;
    }

    void Update()
    {
        
    }
}
