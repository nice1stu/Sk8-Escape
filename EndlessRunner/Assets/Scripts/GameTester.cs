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
        if (currentRunScore <= saveManager.SaveHighScore) return;
        saveManager.SaveHighScore = currentRunScore;
    }

    private void SavePlayerData()
    {
        if (saveManager == null) return;

        // Update the saved player points and coins in the SaveManager
        saveManager.SaveTotalScore = totalPlayerScore;
        saveManager.SaveTotalGems = totalPlayerGems;
        saveManager.SaveTotalCoins = totalPlayerCoins;

<<<<<<< Updated upstream
        saveManager.MusicVolume = adjustMusicVolume;
        saveManager.MusicMute = toggleMusicMute;
        saveManager.SfxVolume = adjustSfxVolume;
        saveManager.SfxMute = toggleSfxMute;
=======
        saveManager.SaveMusicVolume = adjustMusicVolume;
        saveManager.SaveMusicMute = toggleMusicMute;
        saveManager.SaveSfxVolume = adjustSfxVolume;
        saveManager.SaveSfxMute = toggleSfxMute;
>>>>>>> Stashed changes
        
        // Save the player data in the SaveManager
        saveManager.SaveGameData();
    }

    private void LoadPlayerData()
    {
        // Load the player data from the SaveManager
        saveManager.LoadData();

        // Update the player points and coins from the saved data
        totalPlayerScore = saveManager.SaveTotalScore;
        totalPlayerGems = saveManager.SaveTotalGems;
        totalPlayerCoins = saveManager.SaveTotalCoins;
        highScore = saveManager.SaveHighScore;
        
<<<<<<< Updated upstream
        adjustMusicVolume = saveManager.MusicVolume;
        toggleMusicMute = saveManager.MusicMute;
        adjustSfxVolume = saveManager.SfxVolume;
        toggleSfxMute= saveManager.SfxMute;
=======
        adjustMusicVolume = saveManager.SaveMusicVolume;
        toggleMusicMute = saveManager.SaveMusicMute;
        adjustSfxVolume = saveManager.SaveSfxVolume;
        toggleSfxMute= saveManager.SaveSfxMute;
>>>>>>> Stashed changes
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
        saveManager.SaveHighScore = 0;
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