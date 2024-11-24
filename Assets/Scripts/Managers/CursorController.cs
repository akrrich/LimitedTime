using UnityEngine;

public class CursorController
{
    public void CheckSceneNameToShowCursor()
    {
        switch (GameManager.Instance.CurrentScene.name)
        {
            case "Menu":
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
                break;

            case "Level 1":
            case "Level 2":

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

    public void OnGUICursor()
    {
        float cursorScale;

        if (GameManager.Instance.CurrentScene.name != "Menu")
        {
            if (!PauseManager.Instance.IsGamePaused && !TimeManager.Instance.TimeExpired && GameManager.Instance.ShowMira)
            {
                cursorScale = 0.1f;

                float width = GameManager.Instance.CursorTexture.width * cursorScale;
                float height = GameManager.Instance.CursorTexture.height * cursorScale;

                float posX = (Screen.width - width) / 2;
                float posY = (Screen.height - height) / 2;

                GUI.DrawTexture(new Rect(posX, posY, width, height), GameManager.Instance.CursorTexture);
            }

            else
            {
                cursorScale = 0f;
            }
        }
    }
}
