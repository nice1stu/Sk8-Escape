using UnityEngine.InputSystem;

namespace Player
{
    public class FallingTransition : Transition
    {
        public FallingTransition(TrickState from, TrickState to) : base(from, to)
        {
            
        }

        protected override bool CanTransition(PlayerController playerController)
        {
            return (!playerController.grounded && playerController.rb.velocity.y < playerController.model.initialFallingVelocity);
        }
    }
}