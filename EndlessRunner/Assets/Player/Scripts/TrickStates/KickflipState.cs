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
            //Would be much better to store the PlayerScoreModel somewhere but idk how to do that, so we have to find it each time
            GameObject.FindWithTag("HUD").GetComponentInChildren<PlayerScoreModel>().AddToScore(5);
            base.Enter(playerController);
            playerController.view.PlayKickflipAnim();
            playerController.AddToCurrentVelocity(Vector2.up * playerController.model.kickflipJumpForce);
            if (playerController.trickParticles != null)
                playerController.trickParticles.Play();

        }
    }
}