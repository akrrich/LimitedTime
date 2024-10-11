using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    [SerializeField] private PlayerController playerController;

    [SerializeField] private Texture2D cursorTexture;

    private Scene currentScene;

    private float cursorScale = 0.1f;


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

    void Update()
    {
        currentScene = SceneManager.GetActiveScene();

        switch (currentScene.name)
        {
            case "Menu":
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
            break;

            case "Level 1": case "Level 2":

                if (!PauseManager.Instance.IsGamePaused && !TimeManager.Instance.TimeExpired)
                {
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Confined;
                }

                else
                {
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.Confined;
                }

            break;
        }
    }

    void OnGUI()
    {
        if (currentScene.name != "Menu")
        {
            if (!PauseManager.Instance.IsGamePaused && !TimeManager.Instance.TimeExpired)
            {
                cursorScale = 0.1f;

                float width = cursorTexture.width * cursorScale;
                float height = cursorTexture.height * cursorScale;

                float posX = (Screen.width - width) / 2;
                float posY = (Screen.height - height) / 2;

                GUI.DrawTexture(new Rect(posX, posY, width, height), cursorTexture);
            }

            else
            {
                cursorScale = 0f;
            }
        }
    }
}
