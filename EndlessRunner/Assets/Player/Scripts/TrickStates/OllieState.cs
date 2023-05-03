using UnityEngine;

namespace Player
{
    public class OllieState : TrickState
    {
        public override void Enter(PlayerController playerController)
        {
            base.Enter(playerController);
            playerController.AddToCurrentVelocity(Vector2.up * playerController.model.ollieJumpForce);

        }
    }
}