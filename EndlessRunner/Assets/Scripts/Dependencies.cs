using System;
using System;
using Inventory;
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
    
    private InventorySerializer inventorySerializer;

    
    public IInventoryData Inventory => dummyInventory;
    public IActiveInventory Equipped => dummyInventory;

    private void OnEnable()
    {
        //Move to constructor when not scriptableObject anymore
        inventorySerializer = new InventorySerializer(dummyInventory);
        dummyInventory.CreateDummyItem();
        Load();
    }
    void Load()
    {
        dummyInventory.Load();
    }
    public Dependencies(){
        
    }
}