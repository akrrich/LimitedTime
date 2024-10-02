using System.Collections;
using System.Collections.Generic;
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

    private static event Action onReloadingText;
    public static Action OnReloadingText { get => onReloadingText; set => onReloadingText = value; }

    private static event Action onReloadingFinished;
    public static Action OnReloadingFinished { get => onReloadingFinished; set => onReloadingFinished = value; }


    public BulletPool BulletPool { get => bulletPool; }
    public CameraFollow CameraTransform { get => cameraTransform; }
    public Rigidbody Rb { get => rb; }
    public AudioSource[] PLayerAudios { get => playerAudios; }
    public Animator Anim { get => anim; }
    public StateController StateController { get => stateController; }

    private Vector3 position;

    private int life = 3;
    private int damage = 1;

    private float horizontalInput;
    private float verticalInput;

    private float reloadingTime = 0f;

    private float jumpForce;
    private float speed = 4f;

    private bool isGrounded = true;
    private bool canShoot = true;
    public bool playerAlive = true;

    public int Life { get => life; set => life = value; }
    public int Damage { get => damage; set => damage = value; } 

    public float JumpForce { get => jumpForce; set { jumpForce = value; } }
    public float Speed { get => speed; set { speed = value; } }
    public bool IsGrounded { get => isGrounded; set { isGrounded = value; } }
    public bool CanShoot { get => canShoot; set { canShoot = value; } }


    void Start()
    {
        bulletPool = FindObjectOfType<BulletPool>();
        rb = GetComponent<Rigidbody>();
        playerAudios = GetComponentsInChildren<AudioSource>();
        cameraTransform = GetComponentInChildren<CameraFollow>();
        anim = GetComponentInChildren<Animator>();

        stateController = new StateController(this);
        StateController.InitializeState(stateController.IdleState);

        BulletPool.OnReloading += ReloadingGunEvent;
    }

    void Update()
    {
        if (!PauseManager.Instance.IsGamePaused && !TimeManager.Instance.TimeExpired)
        {
            stateController.UpdateState();
            CheckPlayerAlive();
            ReloadGunAutomatic();
            ReloadGunManualy();
        }

        foreach (AudioSource audios in playerAudios)
        {
            PauseManager.PauseAndUnPauseSounds(audios);
        }
    }

    void FixedUpdate()
    {
        if (!PauseManager.Instance.IsGamePaused && !TimeManager.Instance.TimeExpired)
        {
            position = transform.position;

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

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("floor"))
        {
            isGrounded = true;
        }
    }

    void OnDestroy()
    {
        BulletPool.OnReloading -= ReloadingGunEvent;
    }


    private void ReloadGunManualy()
    { 
        if (Input.GetKeyDown(KeyCode.R) && canShoot)
        {
            onReloadingText?.Invoke();

            canShoot = false;
            reloadingTime += Time.deltaTime;

            if (reloadingTime >= 2f)
            {
                canShoot = true;
                reloadingTime = 0f;
                bulletPool.CounterBullets = 15;

                BulletPool.OnReloading += ReloadingGunEvent;

                onReloadingFinished?.Invoke();
            }
        }
    }

    private void ReloadGunAutomatic()
    {
        if (!canShoot)
        {
            onReloadingText?.Invoke();

            reloadingTime += Time.deltaTime;

            if (reloadingTime >= 2f)
            {
                canShoot = true;
                reloadingTime = 0f;
                bulletPool.CounterBullets = 15;

                BulletPool.OnReloading += ReloadingGunEvent;

                onReloadingFinished?.Invoke();
            }
        }
    }

    private void ReloadingGunEvent()
    {
        canShoot = false;
    }

    private void CheckPlayerAlive()
    {
        if (life >= 1)
        {
            playerAlive = true;
        }

        else
        {
            playerAlive = false;
        }
    }
}
