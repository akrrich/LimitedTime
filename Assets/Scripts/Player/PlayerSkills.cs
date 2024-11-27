using System;
using System.Collections.Generic;

public class PlayerSkills
{
    private PlayerController playerController;

    private Dictionary<int, Action> skillsMethods = new Dictionary<int, Action>();

    private float newSpeed = 3.5f;
    private float newJumpForce = 1.25f;
    private int newDamage = 1;
    private int newLife = 2;

    private float TimeToFinishTheReload = 1.25f;


    public Dictionary<int, Action> SkillsMethods { get => skillsMethods; }
    private static event Action onAxeSpeed;
    public static Action OnAxeSpeed { get => onAxeSpeed; set => onAxeSpeed = value; }


    public PlayerSkills(PlayerController playerController)
    {
        this.playerController = playerController;

        InitializeSkillsMethodsInDictionary();
    }


    private void InitializeSkillsMethodsInDictionary()
    {
        skillsMethods.Add(1, SpeedSkill);
        skillsMethods.Add(2, damageSkill);
        skillsMethods.Add(3, JumpForceSkill);
        skillsMethods.Add(4, LifeSkill);
        skillsMethods.Add(6, ReloadingTime);
        skillsMethods.Add(7, AxeSpeed);
        skillsMethods.Add(8, FireRate);
        skillsMethods.Add(9, ExtraBullets);
    }

    private void SpeedSkill()
    {
        playerController.Speed += newSpeed;
    }

    private void damageSkill()
    {
        playerController.Damage += newDamage;
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

    private void AxeSpeed()
    {
        onAxeSpeed?.Invoke();
    }

    private void FireRate()
    {

    }

    private void ExtraBullets()
    {
        playerController.BulletPool.TotalBullets = 75;
    }
}
