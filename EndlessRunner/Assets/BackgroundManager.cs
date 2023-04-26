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
        if (other.gameObject.layer == 10&&first)
        {
            gameObject.GetComponent<BackgroundView>().enabled=false;
            
            Debug.Log("camera triggerExit from "+gameObject);
            otherBackground.transform.parent = transform.parent;
            
            
            otherBackground.gameObject.GetComponent<BackgroundView>().progressiveModifier = otherBackground.transform.position-transform.position;
            Vector3 startPosition = (otherBackground.transform.position - Vector3.left * (otherBackground.GetComponent<SpriteRenderer>().bounds.size.x));
            transform.position = startPosition;
            
            
            transform.parent = otherBackground.transform;
            first = false;
            otherBackground.first = true;
            otherBackground.gameObject.GetComponent<BackgroundView>().enabled = true;
             //StartCoroutine(CO_LateFirstSwitch());

        }
    }

    private IEnumerator CO_LateFirstSwitch()
    {
        yield return 1;
        first = false;
        otherBackground.first = true;
    }

    void Update()
    {
        
    }
}