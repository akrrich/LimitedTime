using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMemento
{
    private PlayerController playerController;

    private Vector3 position;
    private int life = 3;


    public PlayerMemento(PlayerController playerController)
    {
        this.playerController = playerController;
        SaveState();
    }

    public void SaveState()
    {
        position = playerController.transform.position;
    }

    public void RestoreState()
    {
        playerController.transform.position = position;
        playerController.Life = life;
        playerController.PlayerAlive = true;
        playerController.gameObject.SetActive(true);
    }
}
