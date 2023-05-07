using UnityEngine;

namespace Player
{
    public class CrashState : TrickState
    {
        public override void Enter(PlayerController playerController)
        {
            base.Enter(playerController);
            playerController.model.isAlive = false;
            playerController.rb.constraints = RigidbodyConstraints2D.None;
            playerController.rb.AddForce(playerController.wallNormal * 16, ForceMode2D.Impulse);
        }
    }
}