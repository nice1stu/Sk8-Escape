using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButtonScript : MonoBehaviour
{
    public void LoadStartScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void LoadInventoryScene()
    {
        SceneManager.LoadScene("InventoryScene");
    }
    
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }
}