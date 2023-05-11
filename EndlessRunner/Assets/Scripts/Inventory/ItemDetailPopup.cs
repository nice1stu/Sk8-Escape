using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Item;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDetailPopup : MonoBehaviour
{
    public TextMeshProUGUI skateboardLabel;

    public Image skateboardIcon;

    public TextMeshProUGUI stabilityValueLabel;
    public TextMeshProUGUI speedValueLabel;
    public TextMeshProUGUI styleValueLabel;
    public TextMeshProUGUI balanceValueLabel;
    
    private IItemData itemData;

    public void Setup(IItemData itemData)
    {
        this.itemData = itemData;
        skateboardLabel.text = itemData.ItemConfig.ItemName;
        skateboardIcon.sprite = itemData.ItemConfig.ItemIcon;
        stabilityValueLabel.text = itemData.TotalStats.Stability.ToString();
        speedValueLabel.text = itemData.TotalStats.Stability.ToString();
        //TODO change to correct variables instead of vision
        balanceValueLabel.text = itemData.TotalStats.Vision.ToString();
        styleValueLabel.text = itemData.TotalStats.Vision.ToString();
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
        
    }

    private void OnEquip()
    {
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