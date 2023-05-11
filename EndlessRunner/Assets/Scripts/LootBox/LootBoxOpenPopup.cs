using System.Collections;
using System.Collections.Generic;
using Inventory.Scripts;
using Item;
using UnityEngine;
using UnityEngine.UI;

public class LootBoxOpenPopup : MonoBehaviour
{
    public GameObject Background;

    public GameObject OpenButton;

    public GameObject CloseButton;

    public Image LootBoxIcon;

    private ILootBoxData lootBox;

    public GameObject openFx;

    public ItemPreview rewardItemPrefab;

    private List<ItemPreview> rewardItems = new();

    public Transform rewardItemParent;

    // Start is called before the first frame update
    void Start()
    {
        Dependencies.Instance.LootBoxes.LootBoxOpened += LootBoxesOnLootBoxOpened;
    }

    private void LootBoxesOnLootBoxOpened(ILootBoxData arg1, IItemData[] arg2)
    {
        openFx.SetActive(false);
        Background.SetActive(true);
        OpenButton.SetActive(true);
        CloseButton.SetActive(false);
        LootBoxIcon.sprite = arg1.Config.Icon;
        LootBoxIcon.SetNativeSize();
        this.lootBox = arg1;
        
        foreach (var itemPreview in rewardItems)
        {
            Destroy(itemPreview.gameObject);
        }

        rewardItems.Clear();
        
        foreach (var itemData in arg2)
        {
            var rewardItem = Instantiate(rewardItemPrefab, rewardItemParent);
            rewardItem.Setup(itemData, null);
            rewardItems.Add(rewardItem);
        }

        rewardItemParent.gameObject.SetActive(false);
        
    }

    // Update is called once per frame
    void OnDestroy()
    {
        Dependencies.Instance.LootBoxes.LootBoxOpened -= LootBoxesOnLootBoxOpened;
    }

    public void TriggerDummyLootBoxOpen()
    {
        (Dependencies.Instance.LootBoxes as DummyLootBoxInventory).TriggerDummyLotBoxOpened();
    }

    public void OnLootBoxClicked()
    {
        LootBoxIcon.sprite = lootBox.Config.BoxOpen;
        LootBoxIcon.SetNativeSize();
        CloseButton.SetActive(true);
        openFx.SetActive(true);
        rewardItemParent.gameObject.SetActive(true);
    }
    
}
