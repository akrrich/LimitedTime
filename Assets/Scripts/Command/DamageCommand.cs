public class DamageCommand : ICommand
{
    private PlayerController playerController;


    public DamageCommand(PlayerController playerController)
    {
        this.playerController = playerController;
    }


    public void Execute()
    {
        playerController.Damage += 1;
    }

    public void Undo()
    {
        playerController.Damage -= 1;
    }
}
