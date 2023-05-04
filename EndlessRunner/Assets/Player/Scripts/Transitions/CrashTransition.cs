using UnityEngine.InputSystem;

namespace Player
{
    public class CrashTransition : Transition
    {
        public CrashTransition(TrickState from, TrickState to) : base(from, to)
        {
            
        }

        protected override bool CanTransition(PlayerController playerController)
        {
            return (!playerController.grounded && playerController.rb.velocity.y < playerController.model.initialFallingVelocity);
        }
    }
}