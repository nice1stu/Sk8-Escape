using UnityEngine;
using System.Collections;

namespace Player
{
    public class CoffinState : TrickState
    {
        public override void Enter(PlayerController playerController)
        {
            base.Enter(playerController);
            playerController.targetPlayerHeight = playerController.model.playerCrouchHeight;
        }
        

        public override void Exit(PlayerController playerController)
        {
            base.Exit(playerController);
            playerController.targetPlayerHeight = playerController.model.playerStandHeight;
        }
    }
}