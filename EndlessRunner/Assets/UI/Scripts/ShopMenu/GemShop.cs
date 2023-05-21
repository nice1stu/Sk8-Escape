using UnityEngine;
using UI.Scripts;

public class GemShop : MonoBehaviour
{
    public ShopGemSo[] gemPackSo;
    public GameObject[] gemPanelsGo;
    public GemPackTemplate[] gemPanels;
    public UIManager uiManager;

    public PopupWindow popupWarning;
    public PopupWindow popupConfirmation;
    private int gemShopBtn;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < gemPackSo.Length; i++) //looping through number of SO inside the shop
            gemPanelsGo[i].SetActive(true);
        LoadPanel();
    }

    private void LoadPanel()
    {
        for (int i = 0; i < gemPackSo.Length; i++)
        {
            gemPanels[i].gemTitleText.text = gemPackSo[i].title;
            gemPanels[i].gemDescriptionText.text = gemPackSo[i].gemContent + " Gems";
            gemPanels[i].gemDollarCostText.text = gemPackSo[i].gemCostInDollar.ToString();
        }
    }
    
    public void PurchaseItem(int btnNo)
    {
        popupConfirmation.ShowPopupConfirmation("Purchase Item?");
        gemShopBtn = btnNo;
    }
    
    //TO DO; Transaction
    public void CheckPurchase()
    {
        // if ( Money is not enough)
        // {
        //     popupWarning.ShowPopupMessage("Insufficient funds, Please try again!");
        //     return;
        // }
        uiManager.SpendGems(gemPackSo[gemShopBtn].gemContent);
    }
}
