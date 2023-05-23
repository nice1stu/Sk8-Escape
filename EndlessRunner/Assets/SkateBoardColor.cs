using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkateBoardColor : MonoBehaviour
{

    public Color[] colors;
    public int colorIndex;
    void Start()
    {
        GetComponent<SpriteRenderer>().color = colors[colorIndex];
    }

    void Update()
    {
        
    }
}
