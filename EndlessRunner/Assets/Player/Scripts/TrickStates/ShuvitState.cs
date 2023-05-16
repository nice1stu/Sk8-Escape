using UnityEngine;

namespace Player
{
    public class ShuvitState : TrickState
    {
        public ShuvitState(float stateMinDuration) : base(stateMinDuration)
        {
            // You can add additional initialization code here
        }
        
        public override void Enter(PlayerController playerController)
        {
            base.Enter(playerController);
            playerController.view.PlayShuvitAnim();
            playerController.AddToCurrentVelocity(Vector2.up * playerController.model.shuvitJumpForce);
            if(playerController.trickParticles != null)
                playerController.trickParticles.Play();

        }
    }
}