using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private PlayerController playerController;

    private Vector3 cameraOffset;

    private float sensitivity = 2f;
    private float rotationX = 0f;

    private float offSetY = 2.75f;

    
    void Start()
    {
        playerController = GetComponentInParent<PlayerController>();

        cameraOffset = new Vector3(0f, offSetY, 0.3f);
        transform.position = playerController.transform.position + cameraOffset;

        GameManager.Instance.GameStatePlaying += UpdateCameraFollow;
    }

    void UpdateCameraFollow()
    {
        if (!PauseManager.Instance.IsGamePaused && !TimeManager.Instance.TimeExpired)
        {
            cameraOffset = new Vector3(0f, offSetY, 0.3f);

            float mouseX = Input.GetAxis("Mouse X") * sensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

            rotationX -= mouseY;
            rotationX = Mathf.Clamp(rotationX, -80f, 80f);

            transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);

            playerController.transform.Rotate(Vector3.up * mouseX);
        }
    }

    void OnDestroy()
    {
        GameManager.Instance.GameStatePlaying -= UpdateCameraFollow;
    }
}
