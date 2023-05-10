using UnityEngine.InputSystem;

namespace Player
{
    public class ShuvitTransition : InputTransition
    {
        public ShuvitTransition(TrickState from, TrickState to, InputAction inputAction) : base(from, to, inputAction)
        {
        }

        protected override bool CanTransitionInternal(PlayerController playerController)
        {
            return playerController.grounded;
        }
    }
}