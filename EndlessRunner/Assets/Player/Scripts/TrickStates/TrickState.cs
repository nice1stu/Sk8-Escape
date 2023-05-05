using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public abstract class TrickState
    {
        private float stateMinDuration;
        public float stateRemainingDuration;

        public TrickState(float stateMinDuration)
        {
            this.stateMinDuration = stateMinDuration;
        }

        public TrickState()
        {
            stateMinDuration = 0;
        }


        public void Update(PlayerController playerController)
        {
            stateRemainingDuration -= Time.deltaTime;
            UpdateInternal(playerController);
            foreach (var transition in transitions)
            {
                if (transition.Update(playerController))
                    break;
            }
        }

        protected virtual void UpdateInternal(PlayerController playerController)
        {
        
        }

        public virtual void Enter(PlayerController playerController)
        {
            stateRemainingDuration = stateMinDuration;
            Debug.Log("Trickstate: "+ this);
            foreach (var transition in transitions)
            {
                transition.Enter(playerController);
            }
        }

        public virtual void Exit(PlayerController playerController)
        {
            foreach (var transition in transitions)
            {
                transition.Exit(playerController);
            }
        }

        public List<Transition> transitions = new List<Transition>();
    }
}