using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Input
{
    public class SwipeInputAction : IInputAction
    {
        private readonly InputAction _dragInputAction;
        private readonly InputAction _touchDownInputAction;
        private readonly InputAction _touchReleaseInputAction;
        private bool _swipeLocked;
        private float _length;

        public SwipeInputAction(InputAction dragInputAction, InputAction touchDownInputAction, InputAction touchReleaseInputAction)
        {
            _dragInputAction = dragInputAction;
            _touchDownInputAction = touchDownInputAction;
            _touchReleaseInputAction = touchReleaseInputAction;
            
            _touchDownInputAction.started += StartSwipe;
            _dragInputAction.performed += OnDrag;
            _touchDownInputAction.canceled += EndSwipe;
        }

        private void EndSwipe(InputAction.CallbackContext obj)
        {
            Debug.Log($"Endswipe: {_swipeLocked}");
            if(_swipeLocked) 
                Performed?.Invoke(obj);
        }

        private void OnDrag(InputAction.CallbackContext obj)
        {
            _length += obj.ReadValue<float>();
            if (_length < 80)
                return;
            if(_swipeLocked) 
                return; 
            Debug.Log($"On swipe: {_swipeLocked}");
            Started?.Invoke(obj); 
            _swipeLocked = true;
        }

        private void StartSwipe(InputAction.CallbackContext obj)
        {
            _length = 0f;
            Debug.Log($"Touch start: {_swipeLocked}");
            _swipeLocked = false;
        }

        public event Action<InputAction.CallbackContext> Started;
        public event Action<InputAction.CallbackContext> Performed;
        public event Action<InputAction.CallbackContext> Canceled;
    }
}