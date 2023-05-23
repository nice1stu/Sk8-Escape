using System;
using Ads.Scripts;
using Lean.Localization;
using Player;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class GameplayResults : MonoBehaviour
{
    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI coinsCollectedText;

    public PlayerScoreModel _scoreManager;
    public SaveManager _saveManager;
    public AdRewardedDisplay ads;
    public RunInventoryManager cointest;
    public Button adButton;
    public static bool hasPlayedAd;

    [SerializeField] private string scorePhraseName = "Score";
    [SerializeField] private string highScorePhraseName = "Highscore";
    [SerializeField] private string coinsPhraseName = "Coins";


    void Start()
    { 
        //_scoreManager = FindObjectOfType<PlayerScoreModel>();
        //_saveManager = FindObjectOfType<SaveManager>();
    }

    private void OnEnable()
    {
         _saveManager.LoadData(); //loading from saveFile (stewarts thing)
        int currentScore = (int)_scoreManager.GetScore(); // sets/shows score
        
        string scoreTranslation     = LeanLocalization.GetTranslationText(scorePhraseName);
        string highScoreTranslation = LeanLocalization.GetTranslationText(highScorePhraseName);
        //string coinsTranslation     = LeanLocalization.GetTranslationText(coinsPhraseName);
        
        Assert.IsNotNull(scoreTranslation);
        Assert.IsNotNull(highScoreTranslation);
        //Assert.IsNotNull(coinsTranslation);
        
        Debug.Log(scoreTranslation);
        
        currentScoreText.text = $"{scoreTranslation}: {currentScore}";

        int currentCoin = cointest.GetCoinAmount();
        coinsCollectedText.text = $" {currentCoin}";
        
        _saveManager.SaveTotalCoins += cointest.GetCoinAmount();

        if (currentScore > _saveManager.SaveHighScore) // highScoreFrom stewart 
        {
            _saveManager.SaveHighScore = currentScore; // newHighScore save to stewart
            Social.ReportScore(currentScore,GPGSIds.leaderboard_leaderboard, _ => {}); //upload score to the leaderboard
        }
        
        highScoreText.text = $"{highScoreTranslation}: {_saveManager.SaveHighScore}"; //should get the saved file from stewart to display
        _saveManager.SaveGameData(); //hope this was enough to save after each
        
        DisableAdButton();
    }

    private void OnDisable()
    {
        
        hasPlayedAd = false;
    }

    public void PlayAgain()
    {
        // Reset the score to zero
        _scoreManager.SetScore(0);
        cointest.SetCoinAmount(0);
        SceneManager.LoadScene("MainScene");
    }


    public void GoToMainMenu()
    {
        _scoreManager.SetScore(0);
        cointest.SetCoinAmount(0);
        SceneManager.LoadScene("StartMenu");
    }

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
        //string coinsTranslation     = LeanLocalization.GetTranslationText(coinsPhraseName);
        
        _saveManager.SaveTotalCoins +=  cointest.GetCoinAmount();
        coinsCollectedText.text = $"{cointest.GetCoinAmount() * 2}";
        
        
        _saveManager.SaveGameData(); //hope this was enough to save after each
    }

    public void DisableAdButton()
    {
        if (cointest.GetCoinAmount() == 0)
        {
            adButton.interactable = false;
        }
    }
}
