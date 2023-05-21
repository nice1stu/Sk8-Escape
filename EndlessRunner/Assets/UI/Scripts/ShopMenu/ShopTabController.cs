using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopTabController : MonoBehaviour
{
    public GameObject chestTab;
    public GameObject gemTab;

    // public Button chestTabButton;
    // public Button gemTabButton;

    private void Start()
    {
        // Set the initial active tab
        SetActiveTab(chestTab);
    }

    public void SelectChestTab()
    {
        SetActiveTab(chestTab);
    }

    public void SelectGemTab()
    {
        SetActiveTab(gemTab);
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
