using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenuBackKey : MonoBehaviour
{
    public GameObject ConfirmExitPopUp;

    public Button ExitGameButton;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!ConfirmExitPopUp.gameObject.activeInHierarchy)
            {
                ConfirmExitPopUp.gameObject.SetActive(true);
            }
            else
            {
                ExitGameButton.onClick.Invoke();
            }
        }

    }
}
