using static RunInventoryManager;
using UnityEngine;

public class InRunCoin : MonoBehaviour, IPickupable
{
    public void OnPickup()
    {
        coinAmount++;
        Destroy(gameObject);
        Debug.Log("coin amount:" + coinAmount);
    }
}