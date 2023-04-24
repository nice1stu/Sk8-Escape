using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScoreController: MonoBehaviour
{
    private float oldPlayerPosition;
    private GameObject player;
    private PlayerScoreModel model;
    private PlayerScoreView view;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        oldPlayerPosition = player.transform.position.x;
        model = gameObject.GetComponent<PlayerScoreModel>();
        view = gameObject.GetComponent<PlayerScoreView>();
    }

    // Update is called once per frame
    void Update()
    {
        //Get player position
        float newPlayerPosition = player.transform.position.x;
        
        //This *can* result is taking points away from the player if they travel backwards, but in practice that should never happen
        model.AddToScore(newPlayerPosition - oldPlayerPosition); //This might make us a bit vulnerable to round errors, but do we really care *that* much?

        oldPlayerPosition = newPlayerPosition;
    }
}
