using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CursorController : MonoBehaviour
{
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (PauseManager.Instance.IsGamePaused || TimeManager.Instance.TimeExpired)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
