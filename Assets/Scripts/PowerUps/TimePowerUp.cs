using UnityEngine;

public class TimePowerUp : PowerUps
{
    protected override void Awake()
    {
        base.Awake();
        durationPowerUp = 7.5f;
    }

    protected override void ActivePowerUp(Collider collider)
    {
        PlayerController playerController = collider.gameObject.GetComponent<PlayerController>();
        TimeCommand timeCommand = new TimeCommand(playerController);

        powerUpsManager.AddPowerUp(timeCommand, durationPowerUp);
    }
}
