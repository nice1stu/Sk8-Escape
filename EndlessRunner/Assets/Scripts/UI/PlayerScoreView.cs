using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerScoreView : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI text;

    private PlayerScoreModel model;
    // Start is called before the first frame update
    void Start()
    {
        text.SetText("This is where points should be displayed");
        model = gameObject.GetComponent<PlayerScoreModel>();
    }

    // Update is called once per frame
    void Update()
    {
        //Here we convert the score which is a float into an int, because the player doesnt want to see decimals
        int score = (int)model.GetScore();
        
        text.SetText("Score: " + score);
    }
}
