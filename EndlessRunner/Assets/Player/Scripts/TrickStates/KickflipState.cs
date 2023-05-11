using UnityEngine;

namespace Player
{
    public class KickflipState : TrickState
    {
        public KickflipState(float stateMinDuration) : base(stateMinDuration)
        {
            
        }
        public override void Enter(PlayerController playerController)
        {
            base.Enter(playerController);
            playerController.view.PlayKickflipAnim();
            playerController.AddToCurrentVelocity(Vector2.up * playerController.model.kickflipJumpForce);
            playerController.trickParticles.Play();

        }
    }
}