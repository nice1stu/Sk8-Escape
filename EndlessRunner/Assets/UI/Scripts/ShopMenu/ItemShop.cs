
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
    public GameObject[] disablePanel;
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
            shopPanels[i].coinCostText.text = "" + shopChestSo[i].coinCost;
            shopPanels[i].gemCostText.text = "" + shopChestSo[i].gemCost;
         
            DisablePanel();
        }
    }

    public void CheckPurchase()
    {
        if (uiManager.GetCoins() < shopChestSo[test].coinCost || uiManager.GetGems() < shopChestSo[test].gemCost)
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
        uiManager.SpendGems(shopChestSo[test].gemCost);
        Dependencies.Instance.LootBoxes.AddLootBox(new LootBoxData(shopChestSo[test].lootBox, DateTime.UtcNow));
    }

    public void DisablePanel()
    {
        for (int i = 0; i < shopChestSo.Length; i++)
        {
            //if coin is not enough disable pop up
            if (uiManager.GetCoins() < shopChestSo[i].coinCost || uiManager.GetGems() < shopChestSo[i].gemCost)
            {
                disablePanel[i].SetActive(true);
            }
            else
            {
                disablePanel[i].SetActive(false);
            }
        }
    }

    public void GemGererator()
    {
        uiManager.SpendGems(-5);
        LoadPanel();
    }
}
