namespace Player
{
    public class CoastState : TrickState
    {
        public override void Enter(PlayerController playerController)
        {
            base.Enter(playerController);
            playerController.view.PlayCoastAnim();
        }
    }
}