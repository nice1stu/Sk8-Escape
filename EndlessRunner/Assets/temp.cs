using UnityEngine;
using UnityEngine.SceneManagement;

public class temp : MonoBehaviour
{
    public void LoadStartScene()
    {
        if (SaveManager.DoneLoading) SceneManager.LoadScene("MainScene");
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
