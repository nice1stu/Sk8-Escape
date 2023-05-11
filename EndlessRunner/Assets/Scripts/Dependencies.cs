using Inventory;
using Inventory.Scripts;
using UnityEngine;

[CreateAssetMenu]
public class Dependencies : ScriptableObject
{
    private static Dependencies _instance;
    public static Dependencies Instance
    {
        get
        {
            if (_instance == null)
                _instance = Resources.Load<Dependencies>(nameof(Dependencies));
            return _instance;
        }
    }
    [SerializeField] private DummyInventory dummyInventory;
    private LootBoxInventory _lootBoxInventory = new LootBoxInventory();
    [SerializeField] private DummyLootBoxInventory _dummyLootBoxInventory = new();

    public ILootBoxInventory LootBoxes => _dummyLootBoxInventory;
    public IInventoryData Inventory => dummyInventory;
    public IActiveInventory Equipped => dummyInventory;
}