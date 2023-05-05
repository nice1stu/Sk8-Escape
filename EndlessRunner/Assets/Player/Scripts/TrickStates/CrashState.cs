namespace Player
{
    public class CrashState : TrickState
    {
        public override void Enter(PlayerController playerController)
        {
            base.Enter(playerController);
            playerController.model.isAlive = false;
        }
    }
}