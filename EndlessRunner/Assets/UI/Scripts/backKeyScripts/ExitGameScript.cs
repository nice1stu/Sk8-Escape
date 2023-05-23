using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGameScript : MonoBehaviour
{
    // Update is called once per frame
    public void button_exit()
    {
        Debug.Log("exit game has been triggered.");
        Application.Quit();
    }
}
