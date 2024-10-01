using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCommand : ICommand
{
    private PlayerController playerController;


    public DamageCommand(PlayerController playerController)
    {
        this.playerController = playerController;
    }


    public void Execute()
    {
        playerController.Damage = 2;
    }

    public void Undo()
    {
        playerController.Damage = 1;
    }
}
