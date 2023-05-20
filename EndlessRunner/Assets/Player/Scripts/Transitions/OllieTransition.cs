using UnityEngine.InputSystem;

namespace Player
{
    public class OllieTransition : InputTransition
    {
        public OllieTransition(TrickState from, TrickState to, InputAction inputAction) : base(from, to, inputAction)
        {
        }

        protected override bool CanTransitionInternal(PlayerController playerController)
        {
            if (playerController.isGrinding) return true;
            return playerController.grounded;
        }
    }
}