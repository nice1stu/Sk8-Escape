using Item;
using UnityEngine;

public class SkateBoardColor : MonoBehaviour
{

    private Color _color;
    void Start()
    {
        foreach (var inventoryItem in Dependencies.Instance.Inventory.Items)
        {
            if (inventoryItem.ItemConfig.ItemType == ItemType.SkateBoard)
            {
                _color = inventoryItem.ItemConfig.Color;
                return;
            }
            _color = Color.white;
        }

        GetComponent<SpriteRenderer>().color = _color;
    }
}
