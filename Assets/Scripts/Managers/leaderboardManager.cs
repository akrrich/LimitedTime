using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class leaderboardManager : MonoBehaviour
{
    private List<PlayersTime> playerTimes = new List<PlayersTime>();

    private string[] userNames =
    {
        "TU", "ElPatopollon", "SickDarky", "Elmagosan22", "Dzans2005",
        "simro2000", "totopro768", "TikyLoco04", "nachofduty", "Fechu776"
    };

    [SerializeField] private TMP_Text[] textNamesInScreen;

    private Scene currentScene;

    private int randomTimeNumber;


    void Start()
    {
        currentScene = SceneManager.GetActiveScene();

        for (int i = 0; i < 10; i++)
        {
            if (userNames[i] != "TU")
            {
                float randomTime = Random.Range(currentScene.name == "Level 1" ? 70 : 150, currentScene.name == "Level 1" ? 180 : 280);
                playerTimes.Add(new PlayersTime(userNames[i], randomTime));
            }

            else
            {
                playerTimes.Add(new PlayersTime(userNames[i], TimeManager.Instance.GetElapsedTime()));
            }
        }

        QuickSort quickSort = new QuickSort();
        quickSort.Sort(playerTimes, 0, playerTimes.Count - 1);

        UpdateLeaderboardUI();
    }

    private void UpdateLeaderboardUI()
    {
        for (int i = 0; i < playerTimes.Count; i++)
        {
            TMP_Text text = textNamesInScreen[i];
            PlayersTime player = playerTimes[i];

            text.text = $"{i + 1} - {player.PlayerName} {Mathf.Floor(player.Time)}";

            if (player.PlayerName == "TU")
            {
                text.color = Color.green;
            }

            else
            {
                text.color = Color.white;
            }
        }
    }


    public class PlayersTime
    {
        private string playerName;
        private float time;

        public string PlayerName { get =>  playerName; }
        public float Time { get => time; }

        public PlayersTime(string playerName, float time)
        {
            this.playerName = playerName;
            this.time = time;
        }
    }
}
