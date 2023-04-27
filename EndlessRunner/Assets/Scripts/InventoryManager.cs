using System.Collections.Generic;
using UnityEngine;

// Just test inventory system for testing save function is working

public class InventoryManager : MonoBehaviour
{
    public List<InventoryItem> inventory = new List<InventoryItem>();

    public void AddItem(InventoryItem item)
    {
        inventory.Add(item);
    }

    public void RemoveItem(InventoryItem item)
    {
        inventory.Remove(item);
    }

    public void DisplayInventory()
    {
        // TODO: Implement inventory display logic
    }
    
    public void ClearInventory()
    {
        inventory.Clear();
    }
}

[System.Serializable]
public class InventoryItem
{
    public string name;
    public int quantity;

    public InventoryItem(string name, int quantity)
    {
        this.name = name;
        this.quantity = quantity;
    }
}

