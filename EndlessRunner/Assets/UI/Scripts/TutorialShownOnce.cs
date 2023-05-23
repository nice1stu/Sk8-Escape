using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialShownOnce : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        // if its false it should call setactive true then change it to true and save it so its only once when installed
        if (PlayerPrefs.HasKey("tutorialShown")) return;
        Debug.Log("kem eg hingað?");
        gameObject.SetActive(true);
        PlayerPrefs.SetString("tutorialShown", "true");
    }

}
