using UnityEngine;
using System.Collections;
using UnityEngine.PlayerLoop;

namespace Player
{
    public class GrindingState : TrickState
    {
        private Transform[] _grindPath;
        private float _gravityCache;
        private float elapsed;
        private PlayerScoreModel HUD;

        public override void Enter(PlayerController playerController)
        {
            base.Enter(playerController);
            playerController.isGrinding = true;
            _grindPath = playerController.grindPath;
            _gravityCache = playerController.rb.gravityScale;
            playerController.view.PlayGrindAnim();
            playerController.rb.gravityScale = 0;
            playerController.rb.velocity = new Vector2(playerController.rb.velocity.x, 0);
            elapsed = 0f;
            HUD = GameObject.FindWithTag("HUD").GetComponentInChildren<PlayerScoreModel>();
            if(playerController.grindParticles != null)
                playerController.grindParticles.Play();
        }

        protected override void UpdateInternal(PlayerController playerController)
        {
            Debug.Log("We are grinding");
            HUD.AddToScore(75 * Time.deltaTime);
            playerController.canGrind = GrindingRail.IsWithinXBounds(playerController.transform.position, _grindPath);

            if (!playerController.canGrind)
            {
                playerController.rb.gravityScale = _gravityCache;
                return;
            }
       
            if (elapsed < playerController.model.grindLerpToTime)
            {
                playerController.rb.velocity = LerpToRail(playerController);
                elapsed += Time.fixedDeltaTime;
            }

            else
                playerController.rb.velocity = FollowRailVelocity(playerController.rb);
        }

        public override void Exit(PlayerController playerController)
        {
            base.Exit(playerController);
            playerController.rb.gravityScale = _gravityCache;
            if (playerController.grindParticles != null)
                playerController.grindParticles.Stop();
        }

        private Vector2 FollowRailVelocity(Rigidbody2D rb)
        {
            Vector3 futurePos = rb.transform.position + (rb.velocity.x * Time.fixedDeltaTime) * Vector3.right;
            Vector3 desired = GrindingRail.GetGrindPosition(futurePos, _grindPath);
            if (desired == futurePos) // We can do this because desired will be set to the position of the transform if no more rail is available.
            {
                return rb.velocity;
            }

            Vector3 direction = desired - rb.transform.position;
            float yVel = direction.y / Time.fixedDeltaTime;
            return new Vector2(rb.velocity.x, yVel);
        }

        private Vector2 LerpToRail(PlayerController playerController)
        {
            Vector2 desired = GrindingRail.GetGrindPosition(
                playerController.transform.position +
                (playerController.rb.velocity.x * Time.fixedDeltaTime) * Vector3.right, _grindPath);
            float lerpthingy = Mathf.Lerp(0, desired.y - playerController.transform.position.y,
                elapsed / playerController.model.grindLerpToTime);
            return new Vector2(playerController.rb.velocity.x, lerpthingy / Time.fixedDeltaTime);
        }
    }
}