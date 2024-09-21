using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    private static TimeManager instance;
    public static TimeManager Instance {  get { return instance; } }

    private TMP_Text timeText;

    private float counter = 120f;

    private bool isCounting = true;
    private bool timeExpired = false;
    public bool TimeExpired { get { return timeExpired; } }

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
        timeText = GetComponent<TMP_Text>();

        ObjectsList.OnAllObjectsFound += StopTimeForWinScreen;
    }

    void Update()
    {
        timeText.text = Mathf.Ceil(counter).ToString();
        DisocuntTimer();
    }

    private void OnDestroy()
    {
        ObjectsList.OnAllObjectsFound -= StopTimeForWinScreen;
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

    private void StopTimeForWinScreen()
    {
        isCounting = false;
        timeExpired = true;
    }
}
