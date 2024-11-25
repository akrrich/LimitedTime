using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    private static PauseManager instance;
    public static PauseManager Instance {  get { return instance; } }

    private AudioSource actionSound;
    private AudioSource musicSource;

    private GameObject panel;
    private GameObject Buttons;
    private GameObject panelSettings;
    private GameObject panelSkillTree;

    [SerializeField] private ButtonsFacade buttonsFacade;

    public AudioSource MusicSource { get { return musicSource; } set { musicSource = value; } }

    private bool isGamePaused = false;
    public bool IsGamePaused { get { return isGamePaused; } set => isGamePaused = value; }


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
    }

    void Start()
    {
        actionSound = GetComponent<AudioSource>();
        musicSource = GameObject.Find("MusicController").GetComponent<AudioSource>();

        panel = transform.Find("Panel").gameObject;
        Buttons = transform.Find("Buttons").gameObject;
        //panelSettings = transform.Find("Panel Settings").gameObject;
        panelSkillTree = transform.Find("PanelSkillTree").gameObject;

        buttonsFacade.InitializeReferences(null, this, FinalScreens.Instance);
    }

    void Update()
    {
        PauseStatus();

        PauseAndUnPauseSounds(musicSource);
    }


    public void ResumeGame()
    {
        actionSound.Play();
        panel.SetActive(false);
        Buttons.SetActive(false);
        isGamePaused = false;
    }

    public void SkillTree()
    {
        actionSound.Play();
        panel.SetActive(false);
        panelSkillTree.SetActive(true);
    }

    public void Settings()
    {
        actionSound.Play();
        panelSettings.SetActive(true);
        Buttons.SetActive(false);
    }

    public void ReturnToMenu()
    {
        StartCoroutine(PlayClickSoundAndChangeScene("Menu"));
        isGamePaused = false;
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

    public void PauseAndUnPauseSounds(AudioSource sound)
    {
        if (isGamePaused)
        {
            sound.Pause();
        }

        else
        {
            sound.UnPause();
        }
    }
}
