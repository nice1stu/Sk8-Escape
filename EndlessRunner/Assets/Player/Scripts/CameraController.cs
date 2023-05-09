using UnityEngine;

namespace Player
{
    public class CameraController : MonoBehaviour
    {
        public Transform target;
        public float damping = 5f;
        public Vector3 offset;

        private Vector3 _velocity = Vector3.zero;
    
        private void LateUpdate()
        {
            Vector3 desiredPosition = target.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref _velocity, damping);
        }

    }
}