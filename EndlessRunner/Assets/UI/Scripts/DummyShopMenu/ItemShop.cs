
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemShop : MonoBehaviour
{
    public int coins;
    public TMP_Text coinUI;
    public ItemShopSo[] itemShopSO;
    public GameObject[] shopPanelsGO;
    public ShopTemplate[] shopPanels;
    public Button[] myPurchaseBtns;
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < itemShopSO.Length; i++) //looping through number of SO inside the shop
            shopPanelsGO[i].SetActive(true);
        coinUI.text = "Coins: " + coins;
        LoadPanel();
        CheckPurchaseable();
    }
    
    public void AddCoins() //Simple Script to add/generate coins
    {
        coins+=5;
        coinUI.text = "Coins: " + coins;
        CheckPurchaseable();
    }
    
    public void CheckPurchaseable()
    {
        for (int i = 0; i < itemShopSO.Length; i++)
        {
            if (!itemShopSO[i].purchased && coins >= itemShopSO[i].coinCost) //if coins is enough
            {
                myPurchaseBtns[i].interactable = true;
            }
            else
                myPurchaseBtns[i].interactable = false;
        }
    }
    
    public void PurchaseItem(int btnNo)
    {
        if (coins < itemShopSO[btnNo].coinCost) return;
        coins -= itemShopSO[btnNo].coinCost;
        coinUI.text = "Coins: " + coins;
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
