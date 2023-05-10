using UnityEngine.InputSystem;

namespace Player
{
    public class CoffinTransition : InputTransition
    {
        public CoffinTransition(TrickState from, TrickState to, InputAction inputAction) : base(from, to, inputAction)
        {
        }

        protected override bool CanTransitionInternal(PlayerController playerController)
        {
            return playerController.grounded;
        }
    }
}