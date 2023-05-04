using UnityEngine;

// This script is just for testing the SaveManager script - XXX Delete in Main Game XXX
public class GameTester : MonoBehaviour
{
    public float adjustMusicVolume;
    public bool toggleMusicMute = true;
    public float adjustSfxVolume;
    public bool toggleSfxMute = true;
    
    public int totalPlayerScore;
    public int totalPlayerGems;
    public int totalPlayerCoins;
    public int currentRunScore;
    [SerializeField] private int highScore;
    
    private SaveManager _saveManager;
    private SaveSettings _saveSettings;

    private void Start()
    {
        // Get the SaveManager component
        _saveManager = GetComponent<SaveManager>();
        _saveSettings = GetComponent<SaveSettings>();

        // Load player data
        LoadPlayerData();
        _saveSettings.LoadSettingsData();
    }

    void UpdateHighScore()
    {
        if (currentRunScore <= _saveManager.SaveHighScore) return;
        _saveManager.SaveHighScore = currentRunScore;
    }

    private void SavePlayerData()
    {
        if (_saveManager == null) return;

        // Update the saved player points and coins in the SaveManager
        _saveManager.SaveTotalScore = totalPlayerScore;
        _saveManager.SaveTotalGems = totalPlayerGems;
        _saveManager.SaveTotalCoins = totalPlayerCoins;

        _saveSettings.SaveMusicVolume = adjustMusicVolume;
        _saveSettings.SaveMusicMute = toggleMusicMute;
        _saveSettings.SaveSfxVolume = adjustSfxVolume;
        _saveSettings.SaveSfxMute = toggleSfxMute;
        
        // Save the player data in the SaveManager
        _saveManager.SaveGameData();
        _saveSettings.SaveSettingsData();
    }

    private void LoadPlayerData()
    {
        // Load the player data from the SaveManager
        _saveManager.LoadData();

        // Update the player points and coins from the saved data
        totalPlayerScore = _saveManager.SaveTotalScore;
        totalPlayerGems = _saveManager.SaveTotalGems;
        totalPlayerCoins = _saveManager.SaveTotalCoins;
        highScore = _saveManager.SaveHighScore;
        
        adjustMusicVolume = _saveSettings.SaveMusicVolume;
        toggleMusicMute = _saveSettings.SaveMusicMute;
        adjustSfxVolume = _saveSettings.SaveSfxVolume;
        toggleSfxMute= _saveSettings.SaveSfxMute;
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
        _saveManager.SaveHighScore = 0;
        adjustMusicVolume = 0.5f;
        toggleMusicMute = true;
        adjustSfxVolume = 0.5f;
        toggleSfxMute= true;
        SavePlayerData();
    }

    // Context menus for adjusting music and sound effect settings
    [ContextMenu("Set Music Volume 50%")]
    private void SetMusicVolume50()
    {
        adjustMusicVolume += 0.3f;
    }

    [ContextMenu("Set Music Volume 0% (Mute)")]
    private void SetMusicMute()
    {
        toggleMusicMute = false;
    }

    [ContextMenu("Set Music Volume 100%")]
    private void SetMusicVolume100()
    {
        adjustSfxVolume += 0.03f;
    }

    [ContextMenu("Set Music Volume 0% (Mute)")]
    private void SetSfxMute()
    {
        toggleSfxMute = false;
    }
}