using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class temp : MonoBehaviour
{
    public void LoadStartScene()
    {
        Destroy(MenuMusic.instance.gameObject);
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
