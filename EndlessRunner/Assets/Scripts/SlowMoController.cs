using UnityEngine;

public class SlowMoController : MonoBehaviour, IPickupable
{
    private PlayerScoreModel scoreModel;
    private HUDSlowmo HUDElement;

    // Start is called before the first frame update
    private void Start()
    {
        scoreModel = GameObject.FindWithTag("HUD").GetComponentInChildren<PlayerScoreModel>();
        HUDElement = GameObject.FindWithTag("HUD").GetComponentInChildren<HUDSlowmo>();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void OnPickup()
    {
        scoreModel.AddPowerUp();
        HUDElement.SetEnabled(true);
        Destroy(gameObject);
    }
}