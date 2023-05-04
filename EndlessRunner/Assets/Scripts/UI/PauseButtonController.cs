using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButtonController : MonoBehaviour
{
    // Start is called before the first frame update

    private bool isPaused = false;
    private float oldTimeScale = 0;


    public void Clicked()
    {
        if (isPaused == false)
        {
            isPaused = true;
            oldTimeScale = Time.timeScale;
            Time.timeScale = 0.0f;
        }
        else
        {
            isPaused = false;
            Time.timeScale = oldTimeScale;
        }
    }
    
}
