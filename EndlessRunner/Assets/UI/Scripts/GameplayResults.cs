using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class GameplayResults : MonoBehaviour
{
    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI coinsCollectedText;

    public PlayerScoreModel _scoreManager;
    public SaveManager _saveManager;
    
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

        coinsCollectedText.text = $"Coins: {_scoreManager.GetCoins()}";
        
        //if(AdButtonPushed / AdWatched ) _saveManager.SavedPlayerCoins += (_scoreManager.GetCoins() * 2);
        //else(_saveManager.SavedPlayerCoins += _scoreManager.GetCoins();)
        
        if (currentScore > _saveManager.SavedHighScore) // highScoreFrom stewart 
        {
            _saveManager.SavedHighScore = currentScore; // newHighScore save to stewart
        }
        
        highScoreText.text = $"High Score: {_saveManager.SavedHighScore}"; //should get the saved file from stewart to display
        _saveManager.SaveGameData(); //hope this was enough to save after each
    }

    public void PlayAgain() => SceneManager.LoadScene("MainScene");

    public void GoToMainMenu() => SceneManager.LoadScene("StartMenu");

    public void WatchAds()
    {
        // TODO: Implement watching ads to double coins collected
    }
}
