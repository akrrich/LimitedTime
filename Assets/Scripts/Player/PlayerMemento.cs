using UnityEngine;

public class PlayerMemento : MonoBehaviour
{
    private PlayerController playerController;

    private Vector3 position;
    private int life = 5;
    private float respawnRadius = 15f;

    private string[] enemiesTags = { "EnemieDeforme", "EnemieManzillado" };


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
        DestroySurroundings();
        playerController.transform.position = position;
        playerController.Life = life;
        playerController.PlayerAlive = true;
        playerController.gameObject.SetActive(true);
        playerController.PLayerAudios[4].Play();
    }

    private void DestroySurroundings()
    {
        Collider[] colliders = Physics.OverlapSphere(playerController.transform.position, respawnRadius);

        foreach (Collider collider in colliders)
        {
            foreach (string tag in enemiesTags)
            {
                if (collider.gameObject.CompareTag(tag))
                {
                    Destroy(collider.gameObject);
                }
            }
        }
    }
}
