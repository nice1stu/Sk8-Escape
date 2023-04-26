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
        Debug.Log("triggerExit");
        if (other.gameObject.layer == 10)
        {
            // Move the first background to the end of the other background
            Vector3 endPosition = otherBackground.transform.position + Vector3.right * otherBackground.GetComponent<SpriteRenderer>().bounds.size.x;
            transform.position = endPosition;

            // Make the first background a child of the other background
            transform.parent = otherBackground.transform;

            // Set the first background as the first background again
            Vector3 startPosition = otherBackground.transform.position - Vector3.right * otherBackground.GetComponent<SpriteRenderer>().bounds.size.x;
            transform.position = startPosition;

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