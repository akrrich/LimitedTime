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

    private float speed;
    public float Speed { set { speed = value; } }

    private bool isGrounded = true;
    public bool IsGrounded { get { return isGrounded; } set { isGrounded = value; } }

    private bool canShoot = true;
    public bool CanShoot { get { return canShoot; } set { canShoot = value; } }

    private float horizontalInput;
    private float verticalInput;


    private float timer = 0f;

    void Start()
    {
        speed = 4f;

        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        stateController = new StateController(this);
        StateController.InitializeState(stateController.IdleState);

        BulletPool.onReloading += ReloadingGun;
    }

    void Update()
    {
        if (!PauseManager.Instance.IsGamePaused && !TimeManager.Instance.TimeExpired)
        {
            stateController.UpdateState();
        }

        if (!canShoot)
        {
            timer += Time.deltaTime;

            print(timer);

            if (timer >= 3f)
            {
                BulletPool.onReloading -= ReloadingGun;

                canShoot = true;
                timer = 0f;
                bulletPool.CounterBullets = 0;

                BulletPool.onReloading += ReloadingGun;
            }
        }
    }

    void OnDestroy()
    {
        BulletPool.onReloading -= ReloadingGun;
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

    private void ReloadingGun()
    {
        canShoot = false;
    }
}
