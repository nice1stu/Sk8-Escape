using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class CrashTransition : Transition
    {
        private TrickState _previousState;
        private float _groundCheckDelay = 0.06f;

        public CrashTransition(TrickState from, TrickState to) : base(from, to)
        {
            _previousState = from;
        }

        public override void Enter(PlayerController playerController)
        {
            base.Enter(playerController);
            _groundCheckDelay = 0.06f;
        }

        protected override bool CanTransition(PlayerController playerController)
        {
            _groundCheckDelay -= Time.deltaTime;
            if (!playerController.model.isAlive) return true; // If the player dies in any other way
            //Debug.Log(_previousState.stateRemainingDuration);
            if (_groundCheckDelay <= 0)
            {
                if (playerController.grounded && _previousState.stateRemainingDuration > 0) Debug.Log("bruh");
                return (playerController.grounded && _previousState.stateRemainingDuration > 0); // if you touch the ground while performing a trick
            }

            return false;
        }
    }
}