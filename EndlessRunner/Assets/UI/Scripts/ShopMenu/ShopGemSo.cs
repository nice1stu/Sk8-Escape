using UnityEngine;
[CreateAssetMenu(fileName = "shopMenu", menuName = "Scriptable Objects/ New Gem Pack", order = 3)]
public class ShopGemSo : ScriptableObject
{
    public string title;
    public int gemContent;
    public double gemCostInDollar;
}
