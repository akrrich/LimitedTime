using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;

    private StateController stateController;
    public StateController StateController { get { return stateController; } }

    private Rigidbody rb;
    public Rigidbody Rb { get { return rb; } }


    private float jumpForce;
    public float JumpForce { get { return jumpForce; } set { jumpForce = value; } }

    private float speed;
    public float Speed { set { speed = value; } }

    private bool isGrounded = true;
    public bool IsGrounded { get { return isGrounded; } set { isGrounded = value; } } 

    private float horizontalInput;
    private float verticalInput;


    void Awake()
    {
        stateController = new StateController(this);
        StateController.InitializeState(stateController.IdleState);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!PauseManager.Instance.IsGamePaused && !TimeManager.Instance.timeExpired)
        {
            stateController.UpdateState();
        }
    }

    void FixedUpdate()
    {
        if (!PauseManager.Instance.IsGamePaused && !TimeManager.Instance.timeExpired)
        {
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");

            Vector3 inputMovement = new Vector3(horizontalInput, 0, verticalInput);

            Vector3 cameraForward = cameraTransform.forward;
            cameraForward.y = 0;
            Vector3 movement = cameraForward * inputMovement.z + cameraTransform.right * inputMovement.x;
            movement.y = 0;

            rb.velocity = movement * speed + new Vector3(0, rb.velocity.y, 0);
        }
    }

    void OnCollisionStay(Collision collision)
    {
        CheckColitionWithFloor(collision);
    }

    private void CheckColitionWithFloor(Collision collision)
    {
        if (collision.gameObject.CompareTag("floor"))
        {
            isGrounded = true;
        }
    }
}
