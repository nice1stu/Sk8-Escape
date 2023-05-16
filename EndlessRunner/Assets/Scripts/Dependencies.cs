using System.Linq;
using Inventory;
using Item;
using Stat;
using Inventory.Scripts;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu]
public class Dependencies : ScriptableObject
{
    private ItemFactory _itemFactory;
    
    private static Dependencies _instance;
    [SerializeField] private DummyInventory dummyInventory;
    [SerializeField] private ItemConfigSO dummyItem;

    [SerializeField] private PlayerInventory playerInventory;

    private InventorySerializer inventorySerializer;
    private LootBoxSerializer lootBoxSerializer;
    [SerializeField] private ItemDataBaseSO itemDataBase;
    [SerializeField] private LootBoxDataBaseSo lootBoxDataBase;

    public static Dependencies Instance
    {
        get
        {
            if (_instance == null)
                _instance = Resources.Load<Dependencies>(nameof(Dependencies));
            return _instance;
        }
    }
    private LootBoxInventory _lootBoxInventory;

    public ILootBoxInventory LootBoxes => _lootBoxInventory;
    public IInventoryData Inventory => playerInventory;
    public IActiveInventory Equipped => playerInventory;

    private void OnEnable()
    {
        //Move to constructor when not scriptableObject anymore
        inventorySerializer = new InventorySerializer(playerInventory, itemDataBase, playerInventory);
        _itemFactory = new ItemFactory(playerInventory);
        _lootBoxInventory = new LootBoxInventory(_itemFactory);
        inventorySerializer = new InventorySerializer(playerInventory, itemDataBase);
        lootBoxSerializer = new LootBoxSerializer(_lootBoxInventory, lootBoxDataBase);
        var playerStats = new PlayerStats(Equipped);
        Load();
        playerStats.GetCurrentStats();
    }

    public void CreateItemButton()
    {
        _itemFactory.CreateItem(dummyItem);
    }

    private void Load()
    {
        var items = inventorySerializer.Load();
        playerInventory.Load(items);
        var lootBoxes = lootBoxSerializer.Load();
        _lootBoxInventory.Load(lootBoxes);
        var equipedIndices = inventorySerializer.LoadEquip();
        playerInventory.Load(items,equipedIndices);
    }
}