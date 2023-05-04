using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public SaveManager saveManager;
    public TextMeshProUGUI coinView;
    public TextMeshProUGUI gemsView;
    
    void Start()
    {
        coinView.text = saveManager.SavedPlayerCoins.ToString();
        gemsView.text = saveManager.SavedPlayerGems.ToString();
    }

    public void SpendCoins(int cost)
    {
        saveManager.SavedPlayerCoins -= cost;
        saveManager.SaveGameData();
        coinView.text = saveManager.SavedPlayerCoins.ToString();
    }

    public void SpendGems(int cost)
    {
        saveManager.SavedPlayerGems -= cost;
        saveManager.SaveGameData();
        gemsView.text = saveManager.SavedPlayerGems.ToString();
    }
}
