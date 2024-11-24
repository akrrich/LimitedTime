using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState {

    Start,
    WaitingFiveSeconds,
    Playing
}

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    private UpdateManager updateManager = new UpdateManager();

    [SerializeField] private PlayerController playerController;
    [SerializeField] private Texture2D cursorTexture;

    private GameState gameState;

    private event Action gameStatePlaying;
    private event Action gameStatePlayingFixedUpdate;

    private Scene currentScene;

    private bool showMira = true;

    public GameState GameState { get => gameState; }
    public Texture2D CursorTexture { get => cursorTexture; }
    public Action GameStatePlaying { get => gameStatePlaying; set => gameStatePlaying = value; }
    public Action GameStatePlayingFixedUpdate { get => gameStatePlayingFixedUpdate; set => gameStatePlayingFixedUpdate = value; }
    public bool ShowMira { get => showMira; set => showMira = value; }
    public Scene CurrentScene { get => currentScene; }


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
        gameState = GameState.Start;
    }

    void Update()
    {
        currentScene = SceneManager.GetActiveScene();

        updateManager.UpdateAllGame();
    }

    void FixedUpdate()
    {
        updateManager.FixedUpdateAllGame();
    }

    void OnGUI()
    {
        updateManager.OnGUIAllGame();
    }


    public void ChangeStateTo(GameState state)
    {
        gameState = state;
    }
}
