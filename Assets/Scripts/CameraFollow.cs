using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;

    private Vector3 offset;

    private float sensitivity = 2f;
    private float verticalRotation = 0f;

    void Start()
    {
        transform.position = player.position + offset;
    }

    void Update()
    {
        if (!PauseManager.Instance.IsGamePaused && !TimeManager.Instance.TimeExpired)
        {
            float horizontalInput = Input.GetAxis("Mouse X");
            transform.Rotate(Vector3.up * horizontalInput * sensitivity);

            float verticalInput = Input.GetAxis("Mouse Y");
            verticalRotation -= verticalInput * sensitivity;
            verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(verticalRotation, transform.localRotation.eulerAngles.y, 0f);

            transform.position = player.position + offset;
        }
    }
}
