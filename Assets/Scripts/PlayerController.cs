using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    public Transform CameraTransform { get { return cameraTransform; } }

    [SerializeField] private BulletPool bulletPool;
    public BulletPool BulletPool { get { return bulletPool; } }

    private StateController stateController;
    public StateController StateController { get { return stateController; } }

    private Rigidbody rb;
    public Rigidbody Rb { get { return rb; } }

    private Animator anim;
    public Animator Anim { get { return anim; } }


    private float jumpForce;
    public float JumpForce { get { return jumpForce; } set { jumpForce = value; } }

    private float speed = 4f;
    public float Speed { set { speed = value; } }

    private bool isGrounded = true;
    public bool IsGrounded { get { return isGrounded; } set { isGrounded = value; } }

    private bool canShoot = true;
    public bool CanShoot { get { return canShoot; } set { canShoot = value; } }

    private float horizontalInput;
    private float verticalInput;


    private float reloadingTime = 0f;


    void Start()
    {
        BulletPool.OnReloading += ReloadingGunEvent;

        rb = GetComponent<Rigidbody>();
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
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");

            Vector3 inputMovement = new Vector3(horizontalInput, 0, verticalInput);

            Vector3 cameraForward = cameraTransform.forward;
            cameraForward.y = 0;
            Vector3 movement = cameraForward * inputMovement.z + cameraTransform.right * inputMovement.x;
            movement.y = 0;

            rb.velocity = movement * speed + new Vector3(0, rb.velocity.y, 0);

            anim.transform.position = transform.position;
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
