using UnityEngine;

[CreateAssetMenu(fileName = "shopMenu", menuName = "Scriptable Objects/ New Shop Chest", order = 2)]
public class ShopChestSo : ScriptableObject
{
    public string title;
    public string description;
    public int coinCost;
    public Sprite Sprite;
}
