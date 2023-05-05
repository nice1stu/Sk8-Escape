
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UI.Scripts;

public class ItemShop : MonoBehaviour
{
    public ItemShopSo[] itemShopSO;
    public GameObject[] shopPanelsGO;
    public ShopTemplate[] shopPanels;
    public Button[] myPurchaseBtns;
    public UIManager uiManager;
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < itemShopSO.Length; i++) //looping through number of SO inside the shop
            shopPanelsGO[i].SetActive(true);
        LoadPanel();
        CheckPurchaseable();
    }
    
    public void CheckPurchaseable()
    {
        for (int i = 0; i < itemShopSO.Length; i++)
        {
            if (!itemShopSO[i].purchased && uiManager.GetCoins() >= itemShopSO[i].coinCost) //if coins is enough
            {
                myPurchaseBtns[i].interactable = true;
            }
            else
                myPurchaseBtns[i].interactable = false;
        }
    }
    
    public void PurchaseItem(int btnNo)
    {
        if (uiManager.GetCoins() < itemShopSO[btnNo].coinCost) return;
        uiManager.SpendCoins(itemShopSO[btnNo].coinCost);
        itemShopSO[btnNo].purchased = true;
        CheckPurchaseable();
        //Unlock Item
    }
    
    public void LoadPanel()
    {
        for (int i = 0; i < itemShopSO.Length; i++)
        {
            shopPanels[i].titleTxt.text = itemShopSO[i].title;
            shopPanels[i].desriptionTxt.text = itemShopSO[i].description;
            if (itemShopSO[i].purchased)
                shopPanels[i].coinsCostText.text = "Purchased";
            else
                shopPanels[i].coinsCostText.text = "Coins: " + itemShopSO[i].coinCost;
        }
    }
}
