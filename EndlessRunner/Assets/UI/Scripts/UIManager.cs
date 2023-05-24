using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Scripts
{
public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI coinView;
    public TextMeshProUGUI gemsView;
    public static void LoadSettingsAdditively() => SceneManager.LoadScene("SettingsMenu", LoadSceneMode.Additive);
    
    void Start()
    {
        UpdateUI();
    }

    public int GetCoins()
    {
        return SaveManager.SaveTotalCoins;
    }
    public void SpendCoins(int cost)
    {
        SaveManager.SaveTotalCoins -= cost;
        SaveManager.SaveGameData();
        coinView.text = SaveManager.SaveTotalCoins.ToString();
    }

    public void SpendGems(int cost)
    {
        SaveManager.SaveTotalGems -= cost;
        SaveManager.SaveGameData();
        gemsView.text = SaveManager.SaveTotalGems.ToString();
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
        return SaveManager.SaveTotalGems;
    }

    public void ShowLeaderboard()
    {
        Social.ShowLeaderboardUI();
    }

    public void UpdateUI()
    {
        coinView.text = SaveManager.SaveTotalCoins.ToString();
        gemsView.text = SaveManager.SaveTotalGems.ToString();
    }
    
}
}
