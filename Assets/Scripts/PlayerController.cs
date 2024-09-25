using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private BulletPool bulletPool;

    private CameraFollow cameraTransform;
    private Rigidbody rb;
    private AudioSource[] playerAudios;
    private Animator anim;
    private StateController stateController;

    public BulletPool BulletPool { get => bulletPool; }
    public CameraFollow CameraTransform { get => cameraTransform; }
    public Rigidbody Rb { get => rb; }
    public AudioSource[] PLayerAudios { get => playerAudios; }
    public Animator Anim { get => anim; }
    public StateController StateController { get => stateController; }

    private Vector3 position;


    private float horizontalInput;
    private float verticalInput;

    private float reloadingTime = 0f;

    private float jumpForce;
    private float speed = 4f;

    private bool isGrounded = true;
    private bool canShoot = true;

    public float JumpForce { get => jumpForce; set { jumpForce = value; } }
    public float Speed { set { speed = value; } }
    public bool IsGrounded { get => isGrounded; set { isGrounded = value; } }
    public bool CanShoot { get => canShoot; set { canShoot = value; } }


    void Start()
    {
        BulletPool.OnReloading += ReloadingGunEvent;

        rb = GetComponent<Rigidbody>();
        playerAudios = GetComponentsInChildren<AudioSource>();
        cameraTransform = GetComponentInChildren<CameraFollow>();
        anim = GetComponentInChildren<Animator>();

        stateController = new StateController(this);
        StateController.InitializeState(stateController.IdleState);
    }

    void Update()
    {
        if (!PauseManager.Instance.IsGamePaused && !TimeManager.Instance.TimeExpired)
        {
            stateController.UpdateState();
        }

        ReloadGun();
    }

    void OnDestroy()
    {
        BulletPool.OnReloading -= ReloadingGunEvent;
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

 
    private void ReloadGun()
    {
        if (!canShoot)
        {
            reloadingTime += Time.deltaTime;

            if (reloadingTime >= 3f)
            {
                canShoot = true;
                reloadingTime = 0f;
                bulletPool.CounterBullets = 0;

                BulletPool.OnReloading += ReloadingGunEvent;
            }
        }
    }

    private void ReloadingGunEvent()
    {
        canShoot = false;
    }
}
