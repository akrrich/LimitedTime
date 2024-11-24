public class UpdateManager
{
    private CursorController cursorController = new CursorController();


    public void UpdateAllGame()
    {
        if (GameManager.Instance.GameState == GameState.Playing)
        {
            GameManager.Instance.GameStatePlaying?.Invoke();
        }

        cursorController.CheckSceneNameToShowCursor();
    }

    public void FixedUpdateAllGame()
    {
        if (GameManager.Instance.GameState == GameState.Playing)
        {
            GameManager.Instance.GameStatePlayingFixedUpdate?.Invoke();
        }
    }

    public void OnGUIAllGame()
    {
        cursorController.OnGUICursor();
    }
}
