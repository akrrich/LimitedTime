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

    private float counter = 5f;

    public bool timeExpired = false;

    private static event Action _OnTimeExpired;
    public static Action OnTimeExpired { get { return _OnTimeExpired; } set { _OnTimeExpired = value; } }

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
    }

    void Update()
    {
        timeText.text = Mathf.Ceil(counter).ToString();
        DisocuntTimer();
    }

    private void DisocuntTimer()
    {
        counter -= Time.deltaTime;

        if (counter <= 0)
        {
            counter = 0f;
            timeExpired = true;

            _OnTimeExpired?.Invoke();
        }
    }
}
