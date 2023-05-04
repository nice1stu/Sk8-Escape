using System;
using Ads.Scripts;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameplayResults : MonoBehaviour
{
    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI coinsCollectedText;

    public PlayerScoreModel _scoreManager;
    public SaveManager _saveManager;
    public AdInterstitialDisplay ads;
    public RunInventoryManager cointest;
    public Button adButton;
    public static bool hasPlayedAd;
    
    void Start()
    { 
        //_scoreManager = FindObjectOfType<PlayerScoreModel>();
        //_saveManager = FindObjectOfType<SaveManager>();
    }

    private void OnEnable()
    {
         _saveManager.LoadData(); //loading from saveFile (stewarts thing)
        int currentScore = (int)_scoreManager.GetScore(); // sets/shows score
        currentScoreText.text = $"Score: {currentScore}";

        int currentCoin = cointest.GetCoinAmount();
        coinsCollectedText.text = $"Coins: {currentCoin}";
        
        _saveManager.SaveTotalCoins += cointest.GetCoinAmount();

        if (currentScore > _saveManager.SaveHighScore) // highScoreFrom stewart 
        {
            _saveManager.SaveHighScore = currentScore; // newHighScore save to stewart
        }
        
        highScoreText.text = $"High Score: {_saveManager.SaveHighScore}"; //should get the saved file from stewart to display
        _saveManager.SaveGameData(); //hope this was enough to save after each
    }

    private void OnDisable()
    {
        cointest.SetCoinAmount(0);
        _scoreManager.SetScore(0);
        hasPlayedAd = false;
    }

    public void PlayAgain()
    {
        // Reset the score to zero
       

        SceneManager.LoadScene("MainScene");
    }


    public void GoToMainMenu() => SceneManager.LoadScene("StartMenu");

    public void WatchAds()
    {
        // TODO: Implement watching ads to double coins collected
        ads.InitializeAds();
        ads.LoadAd();
        DoubleCoins();
        adButton.interactable = false;
        //disable button
        hasPlayedAd = true;
    }

    private void DoubleCoins()
    {
        _saveManager.SaveTotalCoins +=  cointest.GetCoinAmount();
        coinsCollectedText.text = $"Coins: {cointest.GetCoinAmount() * 2}";
        
        _saveManager.SaveGameData(); //hope this was enough to save after each
    }
}
