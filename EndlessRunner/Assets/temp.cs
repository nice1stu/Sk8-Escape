using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class temp : MonoBehaviour
{
    public static bool delayOnce = true;
    public void LoadStartScene() => StartCoroutine(StartDelay());

    public IEnumerator StartDelay()
    {
        if (delayOnce) yield return new WaitForSeconds(2);
        delayOnce = false;
        SceneManager.LoadScene("MainScene");
    }

    public void LoadShopScene()
    {
        SceneManager.LoadScene("DummyShopMenu");
    }
    
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
