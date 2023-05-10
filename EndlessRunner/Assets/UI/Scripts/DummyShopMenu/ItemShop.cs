
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
    
    public PopupWindow popupWindow;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < shopChestSo.Length; i++) //looping through number of SO inside the shop
            shopPanelsGo[i].SetActive(true);
        LoadPanel();
        CheckPurchaseable();
    }
    
    //this method is will check you have enough coins to purchase the item.
    public void CheckPurchaseable()
    {
        // for (int i = 0; i < shopChestSo.Length; i++)
        // {
        //     if (uiManager.GetCoins() >= shopChestSo[i].coinCost) //if coins is enough
        //     {
        //         myPurchaseBtns[i].interactable = true;
        //     }
        //     else
        //         myPurchaseBtns[i].interactable = false;
        // }
    }
    
    //when you purchase the item this item will be called
    public void PurchaseItem(int btnNo)
    {
        if (uiManager.GetCoins() < shopChestSo[btnNo].coinCost)
        {
            popupWindow.ShowPopup("Not enough coins");
            return;
        }

        /*if (slot.count == 4)
        {
            popupWindow.ShowPopup("LootBoxSlot is full");
            return;
        }*/
        uiManager.SpendCoins(shopChestSo[btnNo].coinCost);
        CheckPurchaseable();
        PurchasedChest();
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
    
    //when you purchase the item this method will be called
    public void PurchasedChest()
    {
        Debug.Log("Chest Unlock!");
    }
}
