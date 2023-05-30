using UnityEngine;

public class TutorialShownOnce : MonoBehaviour
{
    void Start()
    {
        // Calls setActive true(popUp tutorial when opening the game) and only shown once when installed then never again (gets saved with playerPrefs)
        if (PlayerPrefs.HasKey("tutorialShown")) return;
        gameObject.SetActive(true);
        PlayerPrefs.SetString("tutorialShown", "true");
    }
}
