using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;

namespace UI.Scripts
{
public class UIManager : MonoBehaviour
{
    public SaveManager saveManager;
    public TextMeshProUGUI coinView;
    public TextMeshProUGUI gemsView;
    public static void LoadSettingsAdditively() => SceneManager.LoadScene("SettingsMenu", LoadSceneMode.Additive);
    
    void Start()
    {
        UpdateUI();
    }

    public int GetCoins()
    {
        return saveManager.SaveTotalCoins;
    }
    public void SpendCoins(int cost)
    {
        saveManager.SaveTotalCoins -= cost;
        saveManager.SaveGameData();
        coinView.text = saveManager.SaveTotalCoins.ToString();
    }

    public void SpendGems(int cost)
    {
        saveManager.SaveTotalGems -= cost;
        saveManager.SaveGameData();
        gemsView.text = saveManager.SaveTotalGems.ToString();
    }

    public void OpenSettingsMenu()
    {
        SceneManager.LoadScene("SettingsMenu");
    }

    public void BackButton()
    {
        SceneManager.LoadScene("StartMenu");
    }
    
    public int GetGems()
    {
        return saveManager.SaveTotalGems;
    }

    public void ShowLeaderboard()
    {
        Social.ShowLeaderboardUI();
    }

    public void UpdateUI()
    {
        coinView.text = saveManager.SaveTotalCoins.ToString();
        gemsView.text = saveManager.SaveTotalGems.ToString();
    }
    
}
}
