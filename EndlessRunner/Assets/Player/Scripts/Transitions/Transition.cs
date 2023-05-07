using UnityEngine;

namespace Player
{
    public abstract class Transition
    {
        protected readonly TrickState from;
        protected readonly TrickState to;

        public Transition(TrickState from, TrickState to)
        {
            this.from = from;
            this.to = to;
            from.transitions.Add(this);
        }

        public virtual void Enter(PlayerController playerController)
        {
        
        }
    
        public virtual void Exit(PlayerController playerController)
        {
            
        }
    
        public bool Update(PlayerController playerController)
        {
            if (!CanTransition(playerController)) return false;
            
            from.Exit(playerController);
            to.Enter(playerController);
            playerController.currentState = to;
            return true;
        }

        protected abstract bool CanTransition(PlayerController playerController);
    }
}