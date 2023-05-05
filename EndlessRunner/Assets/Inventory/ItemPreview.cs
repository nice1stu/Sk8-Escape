using System.Collections;
using System.Collections.Generic;
using Item;
using UnityEngine;
using UnityEngine.UI;

public class ItemPreview : MonoBehaviour
{
    public Image frame;

    public Image backGround;

    public Image item;
    // Start is called before the first frame update
    public void Setup(IItemData itemData)
    {
        item.sprite = itemData.ItemConfig.ItemIcon;
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
