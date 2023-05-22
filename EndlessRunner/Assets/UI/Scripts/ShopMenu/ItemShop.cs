
using System;
using Inventory.Scripts;
using UnityEngine;
using UI.Scripts;

public class ItemShop : MonoBehaviour
{
    public ShopChestSo[] shopChestSo;
    public GameObject[] shopPanelsGo;
    public ShopTemplate[] shopPanels;
    public GameObject[] disablePanel;
    public UIManager uiManager;

    public PopupWindow popupWarning;
    public PopupWindow popupConfirmation;
    private int _test;
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < shopChestSo.Length; i++) //looping through number of SO inside the shop
            shopPanelsGo[i].SetActive(true);
        LoadPanel();
    }
    
    //this method is to load every details in unity.
    public void LoadPanel()
    {
        for (int i = 0; i < shopChestSo.Length; i++)
        {
            shopPanels[i].titleTxt.text = shopChestSo[i].title;
            shopPanels[i].coinsCostText.text = "" + shopChestSo[i].coinCost;
            shopPanels[i].gemCostText.text = "" + shopChestSo[i].gemCost;
            DisablePanel();
        }
    }
    
    //when you purchase the item this item will be called
    public void PurchaseItem(int btnNo)
    {
        popupConfirmation.ShowPopupConfirmation("Purchase Item?");
        _test = btnNo;
    }
    

    public void CheckPurchase()
    {
        if (uiManager.GetCoins() < shopChestSo[_test].coinCost)
        {
            popupWarning.ShowPopupMessage("Not enough coins");
            return;
        }
        
        if (uiManager.GetGems() < shopChestSo[_test].gemCost)
        {
            popupWarning.ShowPopupMessage("Not enough gems");
            return;
        }

        if (Dependencies.Instance.LootBoxes.IsFull)
        {
            popupWarning.ShowPopupMessage("LootBoxSlot is full");
            return;
        }
        uiManager.SpendCoins(shopChestSo[_test].coinCost);
        uiManager.SpendGems(shopChestSo[_test].gemCost);
        Dependencies.Instance.LootBoxes.AddLootBox(new LootBoxData(shopChestSo[_test].lootBox, DateTime.UtcNow));
    }

    public void DisablePanel()
    {
        for (int i = 0; i < shopChestSo.Length; i++)
        {
            //if coin/gem is not enough DisablePanel pop up
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
}
