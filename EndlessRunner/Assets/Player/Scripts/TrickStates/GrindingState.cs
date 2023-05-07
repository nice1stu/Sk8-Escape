using UnityEngine;
using System.Collections;
using UnityEngine.PlayerLoop;

namespace Player
{
    public class GrindingState : TrickState
    {
        private Transform[] _grindPath;
        private float _gravityCache;
        
        public override void Enter(PlayerController playerController)
        {
            base.Enter(playerController);
            _grindPath = playerController.grindPath;
            _gravityCache = playerController.rb.gravityScale;
            //playerController.grounded = false; maybe??? 
            playerController.rb.gravityScale = 0;
            playerController.rb.velocity = new Vector2(playerController.rb.velocity.x, 0);
        }

        protected override void UpdateInternal(PlayerController playerController)
        {
            var position = playerController.transform.position;
            playerController._canGrind = GrindingRail.IsWithinXBounds(position, _grindPath);

            if (!playerController._canGrind)
            {
                playerController.rb.gravityScale = _gravityCache;
                return;
            }
            
            position =
                GrindingRail.GetGrindPosition(position, _grindPath);
            playerController.transform.position = position;
        }
        
        public override void Exit(PlayerController playerController)
        {
            base.Exit(playerController);
            playerController.rb.gravityScale = _gravityCache;
        }
    }
}