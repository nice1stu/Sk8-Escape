using System;
using Unity.VisualScripting;
using UnityEngine.InputSystem;

namespace Player.Input
{
    public class DefaultInputAction : IInputAction
    {
        public DefaultInputAction(InputAction inputActionImplementation)
        {
            _inputActionImplementation = inputActionImplementation;
        }

        private InputAction _inputActionImplementation;
        public event Action<InputAction.CallbackContext> Started
        {
            add => _inputActionImplementation.started += value;
            remove => _inputActionImplementation.started -= value;
        }

        public event Action<InputAction.CallbackContext> Performed
        {
            add => _inputActionImplementation.performed += value;
            remove => _inputActionImplementation.performed -= value;
        }

        public event Action<InputAction.CallbackContext> Canceled
        {
            add => _inputActionImplementation.canceled += value;
            remove => _inputActionImplementation.canceled -= value;
        }
    }
}