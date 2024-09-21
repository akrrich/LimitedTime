using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    private static PauseManager instance;
    public static PauseManager Instance {  get { return instance; } }

    private AudioSource actionSound;
    private static AudioSource musicSource;
    public static AudioSource MusicSource { get { return musicSource; } set { musicSource = value; } }

    private GameObject panel;
    private GameObject Buttons;
    private GameObject panelSettings;


    private bool isGamePaused = false;
    public bool IsGamePaused { get { return isGamePaused; } }


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
        actionSound = GetComponent<AudioSource>();
        musicSource = GameObject.Find("MusicController").GetComponent<AudioSource>();

        panel = transform.Find("Panel").gameObject;
        Buttons = transform.Find("Buttons").gameObject;
        panelSettings = transform.Find("Panel Settings").gameObject;
    }

    void Update()
    {
        PauseStatus();
    }


    public void ResumeGame()
    {
        musicSource.UnPause();

        actionSound.Play();
        panel.SetActive(false);
        Buttons.SetActive(false);
        isGamePaused = false;
    }

    public void ReturnToMenu()
    {
        StartCoroutine(PlayClickSoundAndChangeScene("Menu"));
        isGamePaused = false;
    }

    public void Setting()
    {
        actionSound.Play();
        panelSettings.SetActive(true);
        Buttons.SetActive(false);
    }

    public void BackButton()
    {
        actionSound.Play();
        panelSettings.SetActive(false);
        Buttons.SetActive(true);
    }


    private void PauseStatus()
    {
        if (!isGamePaused)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                actionSound.Play();
                musicSource.Pause();

                isGamePaused = true;
                panel.SetActive(isGamePaused);
                Buttons.SetActive(isGamePaused);
            }
        }

        else if (isGamePaused)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                actionSound.Play();
                musicSource.UnPause();

                isGamePaused = false;
                panel.SetActive(isGamePaused);
                Buttons.SetActive(isGamePaused);
            }
        }

        Time.timeScale = isGamePaused ? 0f : 1f;
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
