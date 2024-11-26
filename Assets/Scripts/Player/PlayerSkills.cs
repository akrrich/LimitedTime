using System;

public class PlayerSkills
{
    private PlayerController playerController;

    private float newSpeed = 3.5f;
    private float newJumpForce = 1.25f;
    private int newDamage = 1;
    private int newLife = 2;

    private float TimeToFinishTheReload = 1.25f;


    private static event Action onAxeSpeed;
    public static Action OnAxeSpeed { get => onAxeSpeed; set => onAxeSpeed = value; }


    public PlayerSkills(PlayerController playerController)
    {
        this.playerController = playerController; 
    }


    public void SpeedSkill()
    {
        playerController.Speed += newSpeed;
    }

    public void JumpForceSkill()
    {
        playerController.JumpForce += newJumpForce;
    }

    public void damageSkill()
    {
        playerController.Damage += newDamage;
    }

    public void LifeSkill()
    {
        playerController.Life += newLife;
    }

    public void ReloadingTime()
    {
        playerController.TimeToFinishTheReload = TimeToFinishTheReload;
    }

    public void AxeSpeed()
    {
        onAxeSpeed?.Invoke();
    }

    public void FireRate()
    {

    }

    public void ExtraBullets()
    {

    }
}
