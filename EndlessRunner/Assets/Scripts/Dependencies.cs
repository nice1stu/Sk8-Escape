using Inventory;
using Item;
using Stat;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu]
public class Dependencies : ScriptableObject
{
    private static Dependencies _instance;
    [SerializeField] private DummyInventory dummyInventory;

    [SerializeField] private PlayerInventory playerInventory;

    private InventorySerializer inventorySerializer;
    [SerializeField] private ItemDataBaseSO itemDataBase;

    public static Dependencies Instance
    {
        get
        {
            if (_instance == null)
                _instance = Resources.Load<Dependencies>(nameof(Dependencies));
            return _instance;
        }
    }


    public IInventoryData Inventory => playerInventory;
    public IActiveInventory Equipped => playerInventory;

    private void OnEnable()
    {
        //Move to constructor when not scriptableObject anymore
        inventorySerializer = new InventorySerializer(playerInventory, itemDataBase);
        var playerStats = new PlayerStats(Equipped);
        Load();
    }

    public void CreateItemButton()
    {
        dummyInventory.CreateDummyItem();
    }

    private void Load()
    {
        var items = inventorySerializer.Load();
        dummyInventory.Load(items);
    }
}