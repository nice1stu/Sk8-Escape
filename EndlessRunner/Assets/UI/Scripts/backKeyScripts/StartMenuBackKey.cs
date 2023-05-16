using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuBackKey : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Debug.Log("exit button pressed, game should exit if not in editor");
            Application.Quit();
        }

    }
}
