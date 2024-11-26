public class SpeedCommand : ICommand
{
    private PlayerController playerController;

    public SpeedCommand(PlayerController playerController)
    {
        this.playerController = playerController;
    }

    public void Execute()
    {
        if (playerController.StateController.ActualState() == playerController.StateController.WalkingState)
        {
            playerController.Speed += 4f;
        }

        if (playerController.StateController.ActualState() == playerController.StateController.RunningState)
        {
            playerController.Speed += 4f;
        }
    }

    public void Undo()
    {
        if (playerController.StateController.ActualState() == playerController.StateController.WalkingState)
        {
            playerController.Speed -= 4f;
        }

        if (playerController.StateController.ActualState() == playerController.StateController.RunningState)
        {
            playerController.Speed -= 4f;
        }
    }
}
