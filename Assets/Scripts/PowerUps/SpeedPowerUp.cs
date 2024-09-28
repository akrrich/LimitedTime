using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUp : PowerUps
{
    protected override void Start()
    {
        base.Start();

        id = 0;
        durationPowerUp = 7.5f;
    }

    protected override void ActivePowerUp(Collision collision)
    {
        PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
        SpeedCommand speedCommand = new SpeedCommand(playerController);

        powerUpsManager.AddPowerUp(speedCommand, durationPowerUp);
    }
}
