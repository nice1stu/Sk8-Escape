using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Item;
using Lean.Localization;
using UnityEngine;
using UnityEngine.UI;

public class ItemPreview : MonoBehaviour
{
    public Image frame;

    public Image backGround;

    public Image item;
    // Start is called before the first frame update
    public Button equipButton;
    public LeanLocalizedBehaviour equipLabel;
    private IItemData itemData;
    private Action<IItemData> onDetailClicked;

    public void Setup(IItemData itemData, Action<IItemData> onDetailClicked)
    {
        this.itemData = itemData;
        this.onDetailClicked = onDetailClicked;
        item.sprite = itemData.ItemConfig.ItemIcon;
        var equipped = Dependencies.Instance.Equipped.EquippedItems.Any(item => item == itemData);
        if (equipped)
        {
            OnEquip();
        }
        else
        {
            OnUnEquip();
        }
        Dependencies.Instance.Equipped.ItemEquipped += EquippedOnItemEquipped;
        Dependencies.Instance.Equipped.ItemUnequipped += EquippedOnItemUnequipped;
    }

    public void OnDetailClicked()
    {
        onDetailClicked?.Invoke(itemData);
    }

    private void OnDestroy()
    {
        Dependencies.Instance.Equipped.ItemEquipped -= EquippedOnItemEquipped;
        Dependencies.Instance.Equipped.ItemUnequipped -= EquippedOnItemUnequipped;
    }

    private void EquippedOnItemUnequipped(IItemData obj)
    {
        if (obj == itemData)
        {
            OnUnEquip();
        }
    }

    private void EquippedOnItemEquipped(IItemData obj)
    {
        if (obj == itemData)
        {
            OnEquip();
        }
    }

    private void OnUnEquip()
    {
        if (equipButton == null) return;
            equipButton.interactable = true;
        equipLabel.TranslationName = "equip";
    }

    private void OnEquip()
    {
        if (equipButton == null) return;
        equipButton.interactable = false;
        equipLabel.TranslationName = "equipped";
    }

    public void Equip()
    {
        Dependencies.Instance.Equipped.Equip(itemData);
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
