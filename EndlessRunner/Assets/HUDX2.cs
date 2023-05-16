using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDX2 : MonoBehaviour
{
    // Start is called before the first frame update
    private Image visual;
    private TextMeshProUGUI text;
    void Start()
    {
        visual = GetComponentInChildren<Image>();
        text = GetComponentInChildren<TextMeshProUGUI>();
        text.enabled = false;
        visual.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void SetEnabled(bool newState)
    {
        visual.enabled = newState;
        text.enabled = newState;
    }
}
