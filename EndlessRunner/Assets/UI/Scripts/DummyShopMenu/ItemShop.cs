
using System;
using Inventory.Scripts;
using UnityEngine;
using UnityEngine.UI;
using UI.Scripts;

public class ItemShop : MonoBehaviour
{
    public ShopChestSo[] shopChestSo;
    public GameObject[] shopPanelsGo;
    public ShopTemplate[] shopPanels;
    public Button[] myPurchaseBtns;
    public UIManager uiManager;

    public PopupWindow popupWarning;
    public PopupWindow popupConfirmation;
    public bool purchaseSuccess = true;
    private int test;
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < shopChestSo.Length; i++) //looping through number of SO inside the shop
            shopPanelsGo[i].SetActive(true);
        LoadPanel();
    }
    
    //this method is will check you have enough coins to purchase the item.

    //when you purchase the item this item will be called
    public void PurchaseItem(int btnNo)
    {
        popupConfirmation.ShowPopupConfirmation("Purchase Item?");
        test = btnNo;
    }
    
    //this method is to load every details in unity.
    public void LoadPanel()
    {
        for (int i = 0; i < shopChestSo.Length; i++)
        {
            shopPanels[i].titleTxt.text = shopChestSo[i].title;
            shopPanels[i].coinsCostText.text = "Coins: " + shopChestSo[i].coinCost;
        }
    }

    public void CheckPurchase()
    {
        if (uiManager.GetCoins() < shopChestSo[test].coinCost)
        {
            popupWarning.ShowPopupMessage("Not enough coins");
            return;
        }

        if (Dependencies.Instance.LootBoxes.IsFull)
        {
            popupWarning.ShowPopupMessage("LootBoxSlot is full");
            return;
        }
        uiManager.SpendCoins(shopChestSo[test].coinCost);
        Dependencies.Instance.LootBoxes.AddLootBox(new LootBoxData(shopChestSo[test].lootBox, DateTime.UtcNow));
    }
}
