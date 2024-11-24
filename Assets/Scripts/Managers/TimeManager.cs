using System.Collections;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    private static TimeManager instance;
    public static TimeManager Instance {  get { return instance; } }

    private TMP_Text timeText;

    private Scene currentScene;

    private int waitingTimeForStartGame = 5;

    private float counter;

    private bool isCounting;
    private bool timeExpired = false;

    public bool TimeExpired { get => timeExpired; set => timeExpired = value; }
    public bool IsCounting { get => isCounting; set => isCounting = value; }

    private static event Action onTimeExpired;
    public static Action OnTimeExpired { get { return onTimeExpired; } set { onTimeExpired = value; } }


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
        currentScene = SceneManager.GetActiveScene();
        CheckSceneForRealTime();

        timeText = GetComponent<TMP_Text>();

        ObjectsList.OnAllObjectsFound += StopTimeForWinScreenOrLoseScreen;
        ObjectsList.OnGameComplete += StopTimeForWinScreenOrLoseScreen;
        Enemies.OnPlayerDefeated += StopTimeForWinScreenOrLoseScreen;
    }

    void Update()
    {
        timeText.text = Mathf.Ceil(counter).ToString();
        DisocuntTimer();
    }

    void OnDestroy()
    {
        ObjectsList.OnAllObjectsFound -= StopTimeForWinScreenOrLoseScreen;
        ObjectsList.OnGameComplete -= StopTimeForWinScreenOrLoseScreen;
        Enemies.OnPlayerDefeated -= StopTimeForWinScreenOrLoseScreen;
    }


    public IEnumerator WaitFiveSeconds(GameObject objectList)
    {
        isCounting = false;
        GameManager.Instance.ChangeStateTo(GameState.WaitingFiveSeconds);
        GameManager.Instance.ShowMira = false;
        objectList.SetActive(true);

        yield return new WaitForSeconds(waitingTimeForStartGame);

        isCounting = true;
        GameManager.Instance.ShowMira = true;
        objectList.SetActive(false);
        GameManager.Instance.ChangeStateTo(GameState.Playing);
    }


    private void CheckSceneForRealTime()
    {
        switch (currentScene.name)
        {
            case "Level 1":
                counter = 120f;
            break;

            case "Level 2":
                counter = 180;     
            break;
        }
    }

    private void DisocuntTimer()
    {
        if (isCounting)
        {
            counter -= Time.deltaTime;

            if (counter <= 0f)
            {
                counter = 0f;
                timeExpired = true;

                onTimeExpired?.Invoke();
            }
        }
    }

    private void StopTimeForWinScreenOrLoseScreen()
    {
        timeExpired = true;
        isCounting = false;
    }
}
