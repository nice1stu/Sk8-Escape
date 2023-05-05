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
    public void Setup(IItemData itemData)
    {
        item.sprite = itemData.ItemConfig.ItemIcon;
        var equipped = Dependencies.Instance.Equipped.EquippedItems.Any(item => item == itemData);
        if (equipped)
        {
            equipButton.interactable = false;
            equipLabel.TranslationName = "equipped";
        }
        else
        {
            equipButton.interactable = true;
            equipLabel.TranslationName = "equip";
        }
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
