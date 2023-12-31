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
            playerController.view.PlayOllieAnim();
            playerController.AddToCurrentVelocity(Vector2.up * playerController.model.ollieJumpForce);

            if(playerController.trickParticles != null)
                playerController.trickParticles.Play();
        }
    }
}