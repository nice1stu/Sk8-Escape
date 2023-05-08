using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Item;
using Lean.Localization;
using UnityEngine;
using UnityEngine.UI;

public class ItemDetailPopup : MonoBehaviour
{
    public Image frame;

    public Image backGround;

    public Image item;
    // Start is called before the first frame update
    public Button equipButton;
    public LeanLocalizedBehaviour equipLabel;
    private IItemData itemData;

    public void Setup(IItemData itemData)
    {
        this.itemData = itemData;
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
        equipButton.interactable = true;
        equipLabel.TranslationName = "equip";
    }

    private void OnEquip()
    {
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