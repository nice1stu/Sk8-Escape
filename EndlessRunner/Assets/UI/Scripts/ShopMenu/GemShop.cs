using UnityEngine;
using UI.Scripts;
using UnityEngine.UI;

public class GemShop : MonoBehaviour
{
    public ShopGemSo[] gemPackSo;
    public GameObject[] gemPanelsGo;
    public GemPackTemplate[] gemPanels;
    public UIManager uiManager;

    public PopupWindow popupWarning;
    public PopupWindow gemWindowConfirmation;
    private int test;
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < gemPackSo.Length; i++) //looping through number of SO inside the shop
            gemPanelsGo[i].SetActive(true);
        LoadPanel();
    }

    public void LoadPanel()
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
        // gemWindowConfirmation.GemPopupConfirmation("Purchase Item?");
        test = btnNo;
        //TO DO; Transaction
        Debug.Log("You bought a "+ gemPackSo[btnNo].gemContent +" gems.");
    }
    
    //TO DO; Transaction
    public void CheckPurchase()
    {
        // if ()
        // {
        //     popupWarning.ShowPopupMessage("Insufficient funds, Please try again!");
        //     return;
        // }
        uiManager.SpendGems(gemPackSo[test].gemContent);
    }
}
