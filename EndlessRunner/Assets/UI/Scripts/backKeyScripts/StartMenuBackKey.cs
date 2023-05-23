using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class StartMenuBackKey : MonoBehaviour
{
    public GameObject ConfirmExitPopUp;
    public GameObject TutorialPrompt;

    public Button ExitGameButton;

    public Button CloseTutorial;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!TutorialPrompt.gameObject.activeInHierarchy)
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
            else
            {
                CloseTutorial.onClick.Invoke();
            }
        }

    }

    void Start()
    {
        // TODO: remove temporary hack that fixes audio settings
        Dependencies.Instance.Audio.Music.Muted = Dependencies.Instance.Audio.Music.Muted;
        Dependencies.Instance.Audio.Sfx.Muted = Dependencies.Instance.Audio.Sfx.Muted;
    }
}
