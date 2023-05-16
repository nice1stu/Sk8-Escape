using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenuBackKey : MonoBehaviour
{
    public Button backbutton;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            backbutton.onClick.Invoke();
        }
    }
}
