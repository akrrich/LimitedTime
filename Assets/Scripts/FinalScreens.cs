using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalScreens : MonoBehaviour
{
    private static FinalScreens instance;
    public static FinalScreens Instance {  get { return instance; } }

    private PlayerController playerController;

    private AudioSource actionSound;

    private GameObject panelWin;
    private GameObject panelLoose;
    private GameObject panelDeath;
    private GameObject panelGameComplete;

    [SerializeField] private ButtonsFacade buttonsFacade;


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
        playerController = FindObjectOfType<PlayerController>();

        actionSound = GetComponent<AudioSource>();

        panelWin = transform.Find("PanelWin").gameObject;
        panelLoose = transform.Find("PanelLoose").gameObject;
        panelDeath = transform.Find("PanelDeath").gameObject;
        panelGameComplete = transform.Find("PanelGameComplete").gameObject ;

        TimeManager.OnTimeExpired += GoToLoose;
        Enemies.OnPlayerDefeated += GoToDeath;
        ObjectsList.OnAllObjectsFound += GoToWin;
        ObjectsList.OnGameComplete += GoToGameComplete;

        buttonsFacade.InitializeReferences(null, PauseManager.Instance, this);
    }

    void OnDestroy()
    {
        TimeManager.OnTimeExpired -= GoToLoose;
        Enemies.OnPlayerDefeated -= GoToDeath;
        ObjectsList.OnAllObjectsFound -= GoToWin;
        ObjectsList.OnGameComplete -= GoToGameComplete;
    }


    public void PlayAgainThisLevel(string nameNextScene)
    {
        StartCoroutine(PlayClickSoundAndChangeScene(nameNextScene));
    }

    public void BackToMenuButton(string nameNextScene)
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
        PauseManager.Instance.MusicSource.Pause();
        panelLoose.SetActive(true);
    }

    private void GoToWin()
    {
        PauseManager.Instance.MusicSource.Pause();
        panelWin.SetActive(true);
    }

    private void GoToDeath()
    {
        panelDeath.SetActive(true);
    }

    private void GoToGameComplete()
    {
        panelGameComplete.SetActive(true);
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

    private IEnumerator PlayClickSoundAndRespawnPlayer()
    {
        actionSound.Play();

        yield return new WaitForSeconds(actionSound.clip.length);

        TimeManager.Instance.TimeExpired = false;
        TimeManager.Instance.IsCounting = true;


        PlayerController.OnRespawningPlayer += playerController.PlayerMemento.RestoreState;
        PlayerController.OnRespawningPlayer?.Invoke();
        PlayerController.OnRespawningPlayer -= playerController.PlayerMemento.RestoreState;

        panelDeath.SetActive(false);
    }
}
