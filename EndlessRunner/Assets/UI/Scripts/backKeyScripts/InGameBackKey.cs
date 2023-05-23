using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameBackKey : MonoBehaviour
{
    public GameObject PauseMenu;
    public Button PauseReturnButton;
    public Button PauseButton;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))

            if (SceneManager.sceneCount < 2)
            {
                if (PauseMenu.gameObject.activeInHierarchy)
                {
                    PauseReturnButton.onClick.Invoke();
                }
                else
                {
                    PauseButton.onClick.Invoke();
                }
            }
    }
}
