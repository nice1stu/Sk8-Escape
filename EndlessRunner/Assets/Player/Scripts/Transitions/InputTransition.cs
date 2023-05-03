using UnityEngine.InputSystem;

namespace Player
{
    public class InputTransition : Transition
    {
        private readonly InputAction _inputAction;
        private bool _actionPending;

        public InputTransition(TrickState from, TrickState to, InputAction inputAction) : base(from, to)
        {
            _inputAction = inputAction;
        }

        public override void Enter(PlayerController playerController)
        {
            _actionPending = false;
            base.Enter(playerController);
            _inputAction.performed += InputActionOnperformed;
        }

        private void InputActionOnperformed(InputAction.CallbackContext obj)
        {
            _actionPending = true;
        }

        public override void Exit(PlayerController playerController)
        {
            base.Exit(playerController);
            _inputAction.performed -= InputActionOnperformed;
        }

        protected virtual bool CanTransitionInternal(PlayerController playerController)
        {
            return true;
        }
    
        protected override bool CanTransition(PlayerController playerController)
        {
            if (!CanTransitionInternal(playerController)) _actionPending = false;
            return _actionPending;
        }
    }
}