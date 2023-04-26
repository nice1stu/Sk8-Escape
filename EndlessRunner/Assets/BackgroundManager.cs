using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BackgroundManager : MonoBehaviour
{
    public bool first;
    public BackgroundView otherParallax;
    private BackgroundView parallax;
    private SpriteRenderer spriteRenderer;
    public Sprite[] possibleBackgroundSprites;
    
    private void Start()
    {
        parallax = GetComponent<BackgroundView>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == 10&&first)
        {
            spriteRenderer.sprite = possibleBackgroundSprites[Random.Range(0, possibleBackgroundSprites.Length)];
            
            parallax.enabled=false;
            
            Debug.Log("camera triggerExit from "+gameObject+" the object exiting is "+ other);
            var otherParallaxTransform = otherParallax.transform;
            var myTransform = transform;
            otherParallaxTransform.parent = myTransform.parent;


            var otherParallaxTransformPosition = otherParallaxTransform.position;
            otherParallax.progressiveModifier += otherParallaxTransformPosition-myTransform.position;
            Vector3 startPosition = (otherParallaxTransformPosition - Vector3.left * (otherParallax.GetComponent<SpriteRenderer>().bounds.size.x));
            transform.position = startPosition;
            
            
            myTransform.parent = otherParallax.transform;
            first = false;
            otherParallax.GetComponent<BackgroundManager>().first = true;
            otherParallax.enabled = true;
             //StartCoroutine(CO_LateFirstSwitch());
            
        }
    }
}