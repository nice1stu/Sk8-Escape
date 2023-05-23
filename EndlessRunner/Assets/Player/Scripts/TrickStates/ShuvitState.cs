using UnityEngine;

namespace Player
{
    public class ShuvitState : TrickState
    {
        private PlayerScoreModel HUD;
        public ShuvitState(float stateMinDuration) : base(stateMinDuration)
        {
            // You can add additional initialization code here
            HUD = GameObject.FindWithTag("HUD").GetComponentInChildren<PlayerScoreModel>();
        }
        
        public override void Enter(PlayerController playerController)
        {
            HUD.AddToScore(5);
            base.Enter(playerController);
            playerController.view.PlayShuvitAnim();
            playerController.AddToCurrentVelocity(Vector2.up * playerController.model.shuvitJumpForce);

        }
    }
}