using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    private BulletPool bulletPool;
    private CameraFollow cameraTransform;
    private Rigidbody rb;
    private AudioSource[] playerAudios;
    private Animator anim;
    private StateController stateController;
    private PlayerMemento playerMemento;

    public BulletPool BulletPool { get => bulletPool; }
    public CameraFollow CameraTransform { get => cameraTransform; }
    public Rigidbody Rb { get => rb; }
    public AudioSource[] PLayerAudios { get => playerAudios; }
    public Animator Anim { get => anim; }
    public StateController StateController { get => stateController; }
    public PlayerMemento PlayerMemento { get => playerMemento; }

    private static event Action onReloadingText;
    public static Action OnReloadingText { get => onReloadingText; set => onReloadingText = value; }

    private static event Action onReloadingFinished;
    public static Action OnReloadingFinished { get => onReloadingFinished; set => onReloadingFinished = value; }

    private static event Action onRespawningPlayer;
    public static Action OnRespawningPlayer { get => onRespawningPlayer; set => onRespawningPlayer = value; }


    private int life = 5;
    private int damage = 1;

    private float horizontalInput;
    private float verticalInput;

    private float reloadingTimeManualy = 0f;
    private float reloadingTimeAutomatic = 0f;

    private float jumpForce;
    private float speed = 4f;

    private bool isGrounded = true;
    private bool canShoot = true;
    private bool playerAlive = true;
    private bool realoadingGunAutomatic = false;
    private bool realoadingManualy = false;

    public int Life { get => life; set => life = value; }
    public int Damage { get => damage; set => damage = value; } 
    public float JumpForce { get => jumpForce; set { jumpForce = value; } }
    public float Speed { get => speed; set { speed = value; } }
    public bool IsGrounded { get => isGrounded; set { isGrounded = value; } }
    public bool CanShoot { get => canShoot; set { canShoot = value; } }
    public bool PlayerAlive { get => playerAlive; set { playerAlive = value; } }


    void Start()
    {
        bulletPool = FindObjectOfType<BulletPool>();
        rb = GetComponent<Rigidbody>();
        playerAudios = GetComponentsInChildren<AudioSource>();
        cameraTransform = GetComponentInChildren<CameraFollow>();
        anim = GetComponentInChildren<Animator>();

        stateController = new StateController(this);
        StateController.InitializeState(stateController.IdleState);

        BulletPool.OnReloadingAutomatic += ReloadGunAutomaticEvent;

        GameManager.Instance.GameStatePlaying += UpdatePlayerController;
        GameManager.Instance.GameStatePlayingFixedUpdate += FixedUpdatePlayerController;
    }

    void UpdatePlayerController()
    {
        PlayerMechanics();

        foreach (AudioSource audios in playerAudios)
        {
            PauseManager.Instance.PauseAndUnPauseSounds(audios);
        }
    }

    void FixedUpdatePlayerController()
    {
        PlayerPhysics();
    }

    void OnCollisionEnter(Collision collision)
    {
        CheckPlayerColisions(collision);
    }

    void OnDestroy()
    {
        BulletPool.OnReloadingAutomatic -= ReloadGunAutomaticEvent;

        GameManager.Instance.GameStatePlaying -= UpdatePlayerController;
        GameManager.Instance.GameStatePlayingFixedUpdate -= FixedUpdatePlayerController;
    }


    private void PlayerMechanics()
    {
        CheckPlayerAlive();

        if (!PauseManager.Instance.IsGamePaused && !TimeManager.Instance.TimeExpired)
        {
            stateController.UpdateState();
            ReloadGunManualy();
            ReloadGunAutomatic();
            CheckIfCanShootOrNot();
        }

        else
        {
            stateController.TransitionTo(stateController.IdleState);
        }
    }

    private void PlayerPhysics()
    {
        if (!PauseManager.Instance.IsGamePaused && !TimeManager.Instance.TimeExpired)
        {
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");

            Vector3 cameraForward = cameraTransform.transform.forward;
            cameraForward.y = 0;
            cameraForward.Normalize();

            Vector3 right = cameraTransform.transform.right;
            Vector3 movement = (cameraForward * verticalInput + right * horizontalInput).normalized * speed;

            rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);

            anim.transform.position = transform.position;
            anim.transform.rotation = transform.rotation;
        }
    }

    private void CheckPlayerColisions(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "floor":
                isGrounded = true;
                break;

            case "BulletManzillado":
                life -= 1;
                break;
        }
    }

    private void ReloadGunManualy()
    {
        if (bulletPool.TotalBullets >= 1 && bulletPool.CounterBullets < 15)
        {
            if (Input.GetKeyDown(KeyCode.R) && canShoot)
            {
                onReloadingText?.Invoke();
                canShoot = false;
                realoadingManualy = true;
            }

            if (!canShoot && realoadingManualy)
            {
                reloadingTimeManualy += Time.deltaTime;

                if (reloadingTimeManualy >= 2f)
                {
                    canShoot = true;
                    realoadingManualy = false;

                    int bulletsNeeded = 15 - bulletPool.CounterBullets;
                    int bulletsToReload = Mathf.Min(bulletsNeeded, bulletPool.TotalBullets);

                    bulletPool.CounterBullets += bulletsToReload;
                    bulletPool.TotalBullets -= bulletsToReload;

                    reloadingTimeManualy = 0f;

                    onReloadingFinished?.Invoke();
                }
            }
        }

        else
        {
            canShoot = true;
        }
    }

    private void ReloadGunAutomatic()
    {
        if (realoadingGunAutomatic)
        {
            canShoot = false;
            onReloadingText?.Invoke();

            reloadingTimeAutomatic += Time.deltaTime;

            if (reloadingTimeAutomatic >= 2f)
            {
                realoadingGunAutomatic = false;
                canShoot = true;

                int bulletsNeeded = 15 - bulletPool.CounterBullets;
                int bulletsToReload = Mathf.Min(bulletsNeeded, bulletPool.TotalBullets);

                bulletPool.CounterBullets += bulletsToReload;
                bulletPool.TotalBullets -= bulletsToReload;

                reloadingTimeAutomatic = 0f;

                onReloadingFinished?.Invoke();
            }
        }
    }

    private void ReloadGunAutomaticEvent()
    {
        realoadingGunAutomatic = true;
    }

    private void CheckIfCanShootOrNot()
    {
        if (bulletPool.CounterBullets == 0 && bulletPool.TotalBullets == 0)
        {
            canShoot = false;
        }
    }

    private void CheckPlayerAlive()
    {
        if (life >= 1)
        {
            playerAlive = true;
        }

        else
        {
            playerMemento = new PlayerMemento(this);
            playerAlive = false;
            gameObject.SetActive(false);
        }
    }
}
