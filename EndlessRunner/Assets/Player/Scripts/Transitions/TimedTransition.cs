using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class TimedTransition : Transition
    {
        private float _remainingTime;
        private float _ctorTime;
        
        public TimedTransition(TrickState from, TrickState to, float time) : base(from, to)
        {
            _ctorTime = time;
            _remainingTime = _ctorTime;
        }

        public override void Enter(PlayerController playerController)
        {
            base.Enter(playerController);
            _remainingTime = _ctorTime; 
        }

        protected override bool CanTransition(PlayerController playerController)
        {
            _remainingTime -= Time.deltaTime;
            return _remainingTime <= 0f;
        }
    }
}