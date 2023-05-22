using UnityEngine;
using UI.Scripts;

public class GemShop : MonoBehaviour
{
    public ShopGemSo[] gemPackSo;
    public GameObject[] gemPanelsGo;
    public GemPackTemplate[] gemPanels;
    public UIManager uiManager;
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < gemPackSo.Length; i++) //looping through number of SO inside the shop
            gemPanelsGo[i].SetActive(true);
        LoadPanel();
    }
    
    //Update the variable text in unity
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
        //TO DO; Transaction with google in real money
        Debug.Log("You bought a "+ gemPackSo[btnNo].gemContent +" gems.");
        uiManager.SpendGems(-gemPackSo[btnNo].gemContent);
    }
}
