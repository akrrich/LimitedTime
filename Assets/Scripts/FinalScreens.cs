using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalScreens : MonoBehaviour
{
    private GameObject PanelWin;
    private GameObject PanelLoose;

    private AudioSource actionSound;


    void Start()
    {
        PanelWin = transform.Find("PanelWin").gameObject;
        PanelLoose = transform.Find("PanelLoose").gameObject;

        actionSound = GetComponent<AudioSource>();

        TimeManager.OnTimeExpired += GoToLoose;
    }

    void OnDestroy()
    {
        TimeManager.OnTimeExpired -= GoToLoose;
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

    private IEnumerator PlayClickSoundAndChangeScene(string sceneToLoad)
    {
        actionSound.Play();

        yield return new WaitForSeconds(actionSound.clip.length);

        ChangeScene(sceneToLoad);
    }

    private void ChangeScene(string sceneToLoad)
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
