using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HUDSlowmo : MonoBehaviour
{
    // Start is called before the first frame update
    private Image visual;
    void Start()
    {
        visual = GetComponent<Image>();
        visual.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetEnabled(bool newState)
    {
        visual.enabled = newState;
    }
    
    
}
