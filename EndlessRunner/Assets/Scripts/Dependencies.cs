using System;
using Inventory;
using UnityEngine;

[CreateAssetMenu]
public class Dependencies : ScriptableObject
{
    public static Dependencies Instance;
    [SerializeField] private DummyInventory dummyInventory;
    
    public IInventoryData Inventory => dummyInventory;
    public IActiveInventory Equipped { get; } = null;

    private void Awake()
    {
        Instance = this;
    }
}