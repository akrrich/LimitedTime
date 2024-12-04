using System;
using System.Collections.Generic;

public class PlayerSkills
{
    private PlayerController playerController;

    private Dictionary<int, Action> skillsMethods = new Dictionary<int, Action>();

    private static event Action onAxeSpeed;

    private float newSpeed = 3.5f;
    private float newJumpForce = 1.25f;
    private int newLife = 2;
    private float TimeToFinishTheReload = 1.25f;


    public Dictionary<int, Action> SkillsMethods { get => skillsMethods; }
    public static Action OnAxeSpeed { get => onAxeSpeed; set => onAxeSpeed = value; }


    public PlayerSkills(PlayerController playerController)
    {
        this.playerController = playerController;

        InitializeSkillsMethodsInDictionary();
    }


    private void InitializeSkillsMethodsInDictionary()
    {
        skillsMethods.Add(1, SpeedSkill);
        skillsMethods.Add(2, AxeSpeed);
        skillsMethods.Add(3, JumpForceSkill);
        skillsMethods.Add(4, LifeSkill);
        skillsMethods.Add(5, ReloadingTime);
        skillsMethods.Add(6, ExtraBullets);
    }

    private void SpeedSkill()
    {
        playerController.Speed += newSpeed;
    }

    private void AxeSpeed()
    {
        onAxeSpeed?.Invoke();
    }

    private void JumpForceSkill()
    {
        playerController.JumpForce += newJumpForce;
    }

    private void LifeSkill()
    {
        playerController.Life += newLife;
    }

    private void ReloadingTime()
    {
        playerController.TimeToFinishTheReload = TimeToFinishTheReload;
    }

    private void ExtraBullets()
    {
        playerController.BulletPool.TotalBullets = 75;
    }
}
