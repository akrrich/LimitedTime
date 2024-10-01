using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUp : PowerUps
{
    protected override void Awake()
    {
        base.Awake();
        durationPowerUp = 7.5f;
    }

    protected override void ActivePowerUp(Collider collider)
    {
        PlayerController playerController = collider.gameObject.GetComponent<PlayerController>();
        SpeedCommand speedCommand = new SpeedCommand(playerController);

        powerUpsManager.AddPowerUp(speedCommand, durationPowerUp);
    }
}
