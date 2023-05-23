using Player.Input;
using UnityEngine.InputSystem;

namespace Player
{
    public class InputTransition : Transition
    {
        private readonly IInputAction _inputAction;
        private readonly bool _triggerOnCancel;
        private bool _actionPending;


        public InputTransition(TrickState from, TrickState to, IInputAction inputAction, bool triggerOnCancel = false) : base(from, to)
        {
            _inputAction = inputAction;
            _triggerOnCancel = triggerOnCancel;
        }

        public InputTransition(TrickState from, TrickState to, InputAction inputAction, bool triggerOnCancel = false) : base(from, to)
        {
            _inputAction = new DefaultInputAction(inputAction);
            _triggerOnCancel = triggerOnCancel;
        }

        public override void Enter(PlayerController playerController)
        {
            _actionPending = false;
            base.Enter(playerController);
            if (!_triggerOnCancel)
                _inputAction.Performed += InputActionOnperformed;
            else
                _inputAction.Canceled += InputActionOnperformed;
        }

        private void InputActionOnperformed(InputAction.CallbackContext obj)
        {
            _actionPending = true;
        }

        public override void Exit(PlayerController playerController)
        {
            base.Exit(playerController);
            if (!_triggerOnCancel)
                _inputAction.Performed -= InputActionOnperformed;
            else
                _inputAction.Canceled -= InputActionOnperformed;
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