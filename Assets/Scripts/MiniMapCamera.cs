using UnityEngine;

public class MiniMapCamera : MonoBehaviour
{
    private PlayerController playercontroller;
    private float heightOffset = 70f;


    void Start()
    {
        playercontroller = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        transform.position = new Vector3(playercontroller.transform.position.x, playercontroller.transform.position.y + heightOffset, playercontroller.transform.position.z);
    }
}
