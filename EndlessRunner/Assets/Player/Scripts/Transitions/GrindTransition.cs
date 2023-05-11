using UnityEngine.InputSystem;

namespace Player
{
    public class GrindTransition : InputTransition
    {
        public GrindTransition(TrickState from, TrickState to, InputAction inputAction) : base(from, to, inputAction)
        {
        }

        protected override bool CanTransitionInternal(PlayerController playerController)
        {
            return playerController._canGrind;
        }
    }
}