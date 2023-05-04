using UnityEngine;

namespace Player
{
    public class KickflipState : TrickState
    {
        public override void Enter(PlayerController playerController)
        {
            base.Enter(playerController);
            playerController.AddToCurrentVelocity(Vector2.up * playerController.model.kickflipJumpForce);

        }
    }
}