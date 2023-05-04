using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

// [RequireComponent(
//     typeof(SettingsMenuView), 
//     typeof(SettingsMenuController),
//     typeof(SettingsMenuModel)
//     )]
public class SettingsMenu : MonoBehaviour
{
    // Startup scene is responsible for loading the StartMenu scene
    public static SettingsMenu MenuInstance;

    private static SaveManager saveData;
    
    void Awake()
    {
        MenuInstance = this;
    }

    // Sets the UI to reflect backend values 
    private void SetStateFromBackend()
    {
        Assert.IsNotNull(MenuInstance);
        saveData
    }
    
    // private void 
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
