using UnityEngine;

public class ButtonsFacade : MonoBehaviour
{
    private MainMenu mainMenu;
    private PauseManager pauseManager;
    private FinalScreens finalScreens;


    public void InitializeReferences(MainMenu mainMenu, PauseManager pauseManager, FinalScreens finalScreens)
    {
        this.mainMenu = mainMenu;
        this.pauseManager = pauseManager;
        this.finalScreens = finalScreens;
    }


    // MainMenu
    public void PlayGame()
    {
        mainMenu.PlayButton();
    }

    public void ShowSettingsMenu()
    {
        mainMenu.SettingsButton();
    }

    public void Credits()
    {
        mainMenu.CreditsButton();
    }

    public void Exit()
    {
        mainMenu.ExitButton();
    }

    public void BackMainMenu()
    {
        mainMenu.BackButton();
    }


    // PauseManager
    public void ResumeGame()
    {
        pauseManager.ResumeGame();
    }

    public void SkillTree()
    {
        pauseManager.SkillTree();
    }

    public void ShowSettingsPauseManager()
    {
        pauseManager.Settings();
    }

    public void MenuPauseManager()
    {
        pauseManager.ReturnToMenu();
    }

    public void BackPauseManager()
    {
        pauseManager.BackButton();
    }


    //FinalScreens
    public void PlayAgainLevel(string nameNextScene)
    {
        finalScreens.PlayAgainThisLevel(nameNextScene);
    }

    public void NextLevel(string nameNextScene)
    {
        finalScreens.NextLevelButton(nameNextScene);
    }

    public void MenuFinalScreens(string nameNextScene)
    {
        finalScreens.BackToMenuButton(nameNextScene);
    }

    public void RevivePlayer()
    {
        finalScreens.RespawnPlayerButton(); 
    }
}
