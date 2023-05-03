using UnityEngine;

namespace Player
{
    public class PlayerModel : MonoBehaviour
    {
        [Header("General Movement")]
        public float movementSpeed = 5f;
        [Header("Tricks")]
        public float ollieJumpForce = 5f;
        public float kickflipJumpForce = 3f;
        public float shuvitJumpForce = 2f;
        public float coffinTime = 1f;
        [Header("Crouching")]
        public float crouchSharpness = 20f;
        public float playerStandHeight = 1.65f;
        public float playerCrouchHeight = 1f;
        public float crouchAirRatio = 0.5f;
        public bool smoothCrouch = true;
        [Header("State Conditions")] 
        public float initialFallingVelocity = 0.05f;
        [Header("Collisions")] 
        public float maxGroundAngle = 45f;
        public LayerMask groundLayers;
        [Header("Other")]
        public bool isAlive = true;
    }
}
