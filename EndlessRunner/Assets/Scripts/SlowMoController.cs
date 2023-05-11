using UnityEngine;

public class SlowMoController : MonoBehaviour, IPickupable
{
    private PlayerScoreModel scoreModel;

    // Start is called before the first frame update
    private void Start()
    {
        scoreModel = GameObject.FindWithTag("HUD").GetComponentInChildren<PlayerScoreModel>();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void OnPickup()
    {
        scoreModel.AddPowerUp();
        Destroy(gameObject);
    }
}