using System;
using UnityEngine.InputSystem;

namespace Player.Input
{
    public interface IInputAction
    {
        event Action<InputAction.CallbackContext> Started;
        event Action<InputAction.CallbackContext> Performed;
        event Action<InputAction.CallbackContext> Canceled;
    }
}