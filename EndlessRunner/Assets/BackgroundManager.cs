using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    public bool first;
    public BackgroundManager otherBackground;

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == 10)
        {
            Debug.Log("triggerExit");
            otherBackground.transform.parent = transform.parent;
            
            // Set the first background as the first background again
            Vector3 startPosition = (otherBackground.transform.position + Vector3.right * otherBackground.GetComponent<SpriteRenderer>().bounds.size.x);
            transform.position = startPosition;
            
            transform.parent = otherBackground.transform;
            
            first = false;
            gameObject.GetComponent<BackgroundView>().enabled=false;
            otherBackground.first = true;
            otherBackground.gameObject.GetComponent<BackgroundView>().enabled=true;
        }
    }

    void Update()
    {
        
    }
}