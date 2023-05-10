using UnityEngine;

namespace Player
{
    public class OllieState : TrickState
    {
        public OllieState(float stateMinDuration) : base(stateMinDuration)
        {
            
        }
        public override void Enter(PlayerController playerController)
        {
            base.Enter(playerController);
            playerController.AddToCurrentVelocity(Vector2.up * playerController.model.ollieJumpForce);
            
            playerController.particles.Play();

        }
    }
}