using Inventory.Scripts;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "shopMenu", menuName = "Scriptable Objects/ New Shop Chest", order = 2)]
public class ShopChestSo : ScriptableObject
{
    public LootBoxConfigSo lootBox;
    public string title;
    public int coinCost;
}
