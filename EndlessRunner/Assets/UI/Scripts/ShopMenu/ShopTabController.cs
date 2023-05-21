using UnityEngine;

public class ShopTabController : MonoBehaviour
{
    public GameObject chestTab;
    public GameObject gemTab;
    public GameObject chestTabEnable;
    public GameObject gemTabEnable;
    
    // public Button chestTabButton;
    // public Button gemTabButton;

    private void Start()
    {
        // Set the initial active tab
        SelectChestTab();
    }

    public void SelectChestTab()
    {
        SetActiveTab(chestTab);
        chestTabEnable.SetActive(true);
        gemTabEnable.SetActive(false);
    }

    public void SelectGemTab()
    {
        SetActiveTab(gemTab);
        chestTabEnable.SetActive(false);
        gemTabEnable.SetActive(true);
    }

    private void SetActiveTab(GameObject activeTab)
    {
        // Deactivate all tabs
        chestTab.SetActive(false);
        gemTab.SetActive(false);

        // Activate the selected tab
        activeTab.SetActive(true);
    }
}
