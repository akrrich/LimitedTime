using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCommand : ICommand
{
    private PlayerController playerController;


    public TimeCommand(PlayerController playerController)
    {
        this.playerController = playerController;   
    }


    public void Execute() 
    {
        TimeManager.Instance.IsCounting = false;
    }

    public void Undo()
    {
        TimeManager.Instance.IsCounting = true;
    }
}
