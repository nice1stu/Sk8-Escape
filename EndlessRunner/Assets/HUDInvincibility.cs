using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDInvincibility : MonoBehaviour
{
    private Image visual;
    // Start is called before the first frame update
    void Start()
    {
        visual = GetComponentInChildren<Image>();
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
