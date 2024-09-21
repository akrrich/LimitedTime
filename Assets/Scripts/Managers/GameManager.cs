using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    private Scene currentScene;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void Update()
    {
        currentScene = SceneManager.GetActiveScene();

        switch (currentScene.name)
        {
            case "Menu":

                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;

            break;

            case "Level 1":
            case "Level 2":

                if (!PauseManager.Instance.IsGamePaused && !TimeManager.Instance.TimeExpired)
                {
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                }

                else
                {
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                }

            break;
        }
    }
}
