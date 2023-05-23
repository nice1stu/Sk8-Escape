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

    void Start()
    {
        // TODO: remove temporary hack that fixes audio settings
        Dependencies.Instance.Audio.Music.Muted = Dependencies.Instance.Audio.Music.Muted;
        Dependencies.Instance.Audio.Sfx.Muted = Dependencies.Instance.Audio.Sfx.Muted;
    }
}
