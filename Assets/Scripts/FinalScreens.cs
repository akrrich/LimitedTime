using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalScreens : MonoBehaviour
{
    private PlayerController playerController;

    private AudioSource actionSound;

    private GameObject PanelWin;
    private GameObject PanelLoose;
    private GameObject PanelDeath;


    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();

        actionSound = GetComponent<AudioSource>();

        PanelWin = transform.Find("PanelWin").gameObject;
        PanelLoose = transform.Find("PanelLoose").gameObject;
        PanelDeath = transform.Find("PanelDeath").gameObject;

        TimeManager.OnTimeExpired += GoToLoose;
        Deforme.OnPlayerDefeated += GoToDeath;
        ObjectsList.OnAllObjectsFound += GoToWin;
    }

    void OnDestroy()
    {
        TimeManager.OnTimeExpired -= GoToLoose;
        Deforme.OnPlayerDefeated -= GoToDeath;
        ObjectsList.OnAllObjectsFound -= GoToWin;
    }


    public void BackToMenuButton(string nameNextScene)
    {
        StartCoroutine(PlayClickSoundAndChangeScene(nameNextScene));
    }

    public void PlayAgainThisLevel(string nameNextScene)
    {
        StartCoroutine(PlayClickSoundAndChangeScene(nameNextScene));
    }

    public void NextLevelButton(string nameNextScene)
    {
        StartCoroutine(PlayClickSoundAndChangeScene(nameNextScene));
    }

    public void RespawnPlayerButton()
    {
        StartCoroutine(PlayClickSoundAndRespawnPlayer());
    }


    private void GoToLoose()
    {
        PauseManager.MusicSource.Pause();
        PanelLoose.SetActive(true);
    }

    private void GoToWin()
    {
        PauseManager.MusicSource.Pause();
        PanelWin.SetActive(true);
    }

    private void GoToDeath()
    {
        PanelDeath.SetActive(true);
    }

    private IEnumerator PlayClickSoundAndChangeScene(string sceneToLoad)
    {
        actionSound.Play();

        yield return new WaitForSeconds(actionSound.clip.length);

        ChangeScene(sceneToLoad);
    }

    private IEnumerator PlayClickSoundAndRespawnPlayer()
    {
        actionSound.Play();

        yield return new WaitForSeconds(actionSound.clip.length);

        TimeManager.Instance.TimeExpired = false;
        TimeManager.Instance.IsCounting = true;


        PlayerController.OnRespawningPlayer += playerController.PlayerMemento.RestoreState;
        PlayerController.OnRespawningPlayer?.Invoke();
        PlayerController.OnRespawningPlayer -= playerController.PlayerMemento.RestoreState;

        PanelDeath.SetActive(false);
    }


    private void ChangeScene(string sceneToLoad)
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
