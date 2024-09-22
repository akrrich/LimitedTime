using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private PlayerController player;

    private Vector3 offset;

    private float sensitivity = 2f;
    private float verticalRotation = 0f;

    void Start()
    {
        offset = new Vector3(0f, 2.3f, 0f);
        transform.position = player.transform.position + offset;
    }

    void Update()
    {
        if (!PauseManager.Instance.IsGamePaused && !TimeManager.Instance.TimeExpired)
        {
            float horizontalInput = Input.GetAxis("Mouse X");
            float verticalInput = Input.GetAxis("Mouse Y");

            transform.Rotate(Vector3.up * horizontalInput * sensitivity);

            verticalRotation -= verticalInput * sensitivity;
            verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);
            transform.localRotation = Quaternion.Euler(verticalRotation, transform.localRotation.eulerAngles.y, 0f);

            Vector3 playerPosition = player.transform.position + offset;
            transform.position = playerPosition;
        }
    }
}
