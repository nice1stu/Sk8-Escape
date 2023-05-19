using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopMenuBackKey : MonoBehaviour
{
    public GameObject confirmWindow;
    public Button confirmCloseButton;
    public GameObject successWindow;
    public Button successCloseButton;
    public Button shopExitButton;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (confirmWindow.gameObject.activeInHierarchy)
            {
                confirmCloseButton.onClick.Invoke();
            }
            else if (successWindow.gameObject.activeInHierarchy)
            {
                successCloseButton.onClick.Invoke();
            }
            else
            {
                shopExitButton.onClick.Invoke();
            }
            
        }

    }
}
