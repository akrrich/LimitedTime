using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePowerUp : PowerUps
{
    protected override void Awake()
    {
        base.Awake();
        durationPowerUp = 10f;
    }

    protected override void ActivePowerUp(Collider collider)
    {
        PlayerController playerController = collider.gameObject.GetComponent<PlayerController>();
        DamageCommand damageCommand = new DamageCommand(playerController);

        powerUpsManager.AddPowerUp(damageCommand, durationPowerUp);
    }
}
