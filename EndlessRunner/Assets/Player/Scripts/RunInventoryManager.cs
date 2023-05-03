using UnityEngine;

namespace Player
{
    public class RunInventoryManager : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.layer == LayerMask.NameToLayer("Pickupable"))
            {
                if (col.TryGetComponent(out IPickupable pickupable))
                {
                    pickupable.OnPickup();
                }
            
                Debug.Log("Nice!!!");
            }
        }
    }
}
