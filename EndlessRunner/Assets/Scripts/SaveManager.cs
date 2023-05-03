using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private int savedPlayerScore;
    private int savedPlayerGems;
    private int savedPlayerCoins;

    private float musicVolume = 1f;
    private bool musicMute = false;
    private float sfxVolume = 1f;
    private bool sfxMute = false;

    public InventoryManager inventoryManager;

    public int SavedPlayerScore { get; set; }
    public int SavedPlayerGems { get; set; }
    public int SavedPlayerCoins { get; set; }
    public int SavedHighScore { get; set; }

    public float MusicVolume
    {
        get { return musicVolume; }
        set
        {
            musicVolume = Mathf.Clamp01(value);
            AudioListener.volume = musicVolume;
        }
    }

    public bool MusicMute
    {
        get { return AudioListener.pause; }
        set { AudioListener.pause = value; }
    }

    public float SFXVolume
    {
        get { return sfxVolume; }
        set
        {
            sfxVolume = Mathf.Clamp01(value);
            AudioListener.volume = sfxVolume;
        }
    }

    public bool SFXMute
    {
        get { return sfxMute; }
        set { sfxMute = value; }
    }

    private void Start() => LoadData();

    public void LoadData()
    {
        string path = Application.persistentDataPath + "/save.json";
        if (!File.Exists(path)) return;
        string json = File.ReadAllText(path);
        SaveData saveData = JsonUtility.FromJson<SaveData>(json);
        SavedPlayerScore = saveData.playerScore;
        SavedPlayerGems = saveData.playerGems;
        SavedPlayerCoins = saveData.playerCoins;
        SavedHighScore = saveData.playerHighScore;
        inventoryManager.inventory = saveData.inventory;
        musicVolume = saveData.musicVolume;         // Update music settings
        AudioListener.volume = musicVolume;
        musicMute = saveData.musicMute;
        AudioListener.pause = musicMute;
        sfxVolume = saveData.sfxVolume;         // Update SFX settings
        AudioListener.volume = sfxVolume;
        sfxMute = saveData.sfxMute;
    }

    private void SaveData()
    {
        SaveData saveData = new SaveData
        {
            playerScore = SavedPlayerScore,
            playerGems = SavedPlayerGems,
            playerCoins = SavedPlayerCoins,
            playerHighScore = SavedHighScore,
            inventory = inventoryManager.inventory,
            musicVolume = musicVolume,             // Save music settings
            musicMute = musicMute,
            sfxVolume = sfxVolume,             // Save SFX settings
            sfxMute = sfxMute,
        };

        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(Application.persistentDataPath + "/save.json", json);
    }

    public void SaveGameData()
    {
        SaveData();
    }
}

[System.Serializable]
public class SaveData
{
    public int playerScore;
    public int playerGems;
    public int playerCoins;
    public int playerHighScore;
    public List<InventoryItem> inventory;
    public float musicVolume;
    public bool musicMute;
    public float sfxVolume;
    public bool sfxMute;
}