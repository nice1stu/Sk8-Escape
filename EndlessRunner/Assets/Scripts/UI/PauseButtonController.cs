using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButtonController : MonoBehaviour
{
    // Start is called before the first frame update

    private bool isPaused = false;


    public void Clicked()
    {
        Debug.Log("Press!!!");
        if (isPaused == false)
        {
            isPaused = true;
            Time.timeScale = 0.0f;
        }
        else
        {
            isPaused = false;
            Time.timeScale = 1.0f;
        }
    }
    
}
