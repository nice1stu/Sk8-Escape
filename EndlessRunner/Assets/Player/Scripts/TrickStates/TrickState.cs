using System.Collections.Generic;

namespace Player
{
    public abstract class TrickState
    {
        public void Update(PlayerController playerController)
        {
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