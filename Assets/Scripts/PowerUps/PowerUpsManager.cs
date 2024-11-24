using System.Collections;
using UnityEngine;

public class PowerUpsManager : MonoBehaviour
{
    private Pila<ICommand> commandPowerUpsStack = new Pila<ICommand>();


    public void AddPowerUp(ICommand command, float durationActualPowerUp)
    {
        command.Execute();
        commandPowerUpsStack.Push(command);

        StartCoroutine(HandlePowerUpDuration(command, durationActualPowerUp));
    }

    private IEnumerator HandlePowerUpDuration(ICommand command, float durationActualPowerUp)
    {
        yield return new WaitForSeconds(durationActualPowerUp);

        command.Undo();
        commandPowerUpsStack.Pop();
    }
}
