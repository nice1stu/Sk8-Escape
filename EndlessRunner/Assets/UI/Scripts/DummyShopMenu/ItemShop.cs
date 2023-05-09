
using UnityEngine;
using UnityEngine.UI;
using UI.Scripts;

public class ItemShop : MonoBehaviour
{
    public ShopChestSo[] shopChestSO;
    public GameObject[] shopPanelsGO;
    public ShopTemplate[] shopPanels;
    public Button[] myPurchaseBtns;
    public UIManager uiManager;

    
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < shopChestSO.Length; i++) //looping through number of SO inside the shop
            shopPanelsGO[i].SetActive(true);
        LoadPanel();
        CheckPurchaseable();
    }
    
    public void CheckPurchaseable()
    {
        for (int i = 0; i < shopChestSO.Length; i++)
        {
            if (uiManager.GetCoins() >= shopChestSO[i].coinCost) //if coins is enough
            {
                myPurchaseBtns[i].interactable = true;
            }
            else
                myPurchaseBtns[i].interactable = false;
        }
    }
    
    public void PurchaseItem(int btnNo)
    {
        if (uiManager.GetCoins() < shopChestSO[btnNo].coinCost) return;
        uiManager.SpendCoins(shopChestSO[btnNo].coinCost);
        CheckPurchaseable();
        UnlockChest();
    }
    
    public void LoadPanel()
    {
        for (int i = 0; i < shopChestSO.Length; i++)
        {
            shopPanels[i].titleTxt.text = shopChestSO[i].title;
            // shopPanels[i].desriptionTxt.text = shopChestSO[i].description;
            shopPanels[i].coinsCostText.text = "Coins: " + shopChestSO[i].coinCost;
            shopPanels[i].Sprite = shopChestSO[i].Sprite;
        }
    }

    public void UnlockChest()
    {
        Debug.Log("Chest Unlock!");
    }
}
