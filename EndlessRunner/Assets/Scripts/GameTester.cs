using UnityEngine;
using UnityEngine.Serialization;

//This script is just for testing the SaveManager script
public class GameTester : MonoBehaviour
{
    public int totalPlayerScore;
    public int totalPlayerGems;
    public int totalPlayerCoins;
    public int currentRunScore;
    [SerializeField] private int highScore;
    public InventoryManager inventoryManager;
    private SaveManager saveManager;

    private void Start()
    {
        // Get the SaveManager component
        saveManager = GetComponent<SaveManager>();

        // Load player data
        LoadPlayerData();
    }
    
    void UpdateHighScore()
    {
        if (currentRunScore <= saveManager.SavedHighScore) return;
        saveManager.SavedHighScore = currentRunScore;
    }

    private void SavePlayerData()
    {
        if (saveManager == null) return;

        // Update the saved player points and coins in the SaveManager
        saveManager.SavedPlayerScore = totalPlayerScore;
        saveManager.SavedPlayerGems = totalPlayerGems;
        saveManager.SavedPlayerCoins = totalPlayerCoins;
        
        // Save the player data in the SaveManager
        saveManager.SaveGameData();
    }

    private void LoadPlayerData()
    {
        // Load the player data from the SaveManager
        saveManager.LoadData();

        // Update the player points and coins from the saved data
        totalPlayerScore = saveManager.SavedPlayerScore;
        totalPlayerGems = saveManager.SavedPlayerGems;
        totalPlayerCoins = saveManager.SavedPlayerCoins;
        highScore = saveManager.SavedHighScore;

        // Update the inventory from the saved data
        if (inventoryManager == null || saveManager.inventoryManager == null) return;
        inventoryManager.inventory = saveManager.inventoryManager.inventory;
    }
    
    [ContextMenu("Add Points")]
    private void AddGPoints()
    {
        totalPlayerScore += 10;
    }
    
    [ContextMenu("Add Gems")]
    private void AddGems()
    {
        totalPlayerGems += 1;
    }

    [ContextMenu("Add Coins")]
    private void AddCoins()
    {
        totalPlayerCoins += 5;
    }

    [ContextMenu("Add Items")]
    private void AddItems()
    {
        inventoryManager.AddItem(new InventoryItem("Board", 1));
        inventoryManager.AddItem(new InventoryItem("Wheel", 4));
    }
    
    [ContextMenu("End Run")]
    private void EndRun()
    {
        UpdateHighScore();
        SavePlayerData();
        LoadPlayerData();
        currentRunScore = 0;
    }

    [ContextMenu("Reset Player Data")]
    private void ResetPlayerData()
    {
        totalPlayerScore = totalPlayerGems = totalPlayerCoins = currentRunScore = highScore = 0;
        inventoryManager.ClearInventory();
        saveManager.SavedHighScore = 0;
        SavePlayerData();
    }
}