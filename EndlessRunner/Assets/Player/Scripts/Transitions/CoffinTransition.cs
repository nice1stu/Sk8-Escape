using Player.Input;
using UnityEngine.InputSystem;

namespace Player
{
    public class CoffinTransition : InputTransition
    {
        public CoffinTransition(TrickState from, TrickState to, IInputAction inputAction) : base(from, to, inputAction)
        {
        }

        protected override bool CanTransitionInternal(PlayerController playerController)
        {
            return playerController.grounded;
        }
    }
}