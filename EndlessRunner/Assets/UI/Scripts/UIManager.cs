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
}
