using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMemento
{
    private PlayerController playerController;

    public Vector3 position;
    public int life = 3;


    public PlayerMemento(PlayerController playerController)
    {
        this.playerController = playerController;
        SaveState();
    }

    public void SaveState()
    {
        position = playerController.transform.position;
        life = playerController.Life;
    }

    public void RestoreState()
    {
        playerController.transform.position = position;
        playerController.Life = life;
    }
}
