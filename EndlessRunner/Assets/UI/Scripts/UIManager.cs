using TMPro;
using UnityEngine;

namespace UI.Scripts
{
public class UIManager : MonoBehaviour
{
    public SaveManager saveManager;
    public TextMeshProUGUI coinView;
    public TextMeshProUGUI gemsView;
    
    void Start()
    {
        coinView.text = saveManager.SaveTotalCoins.ToString();
        gemsView.text = saveManager.SaveTotalGems.ToString();
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
}
}
