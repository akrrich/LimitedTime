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

    private float jumpForce = 5f;
    private float runSpeed = 7.5f;
    private float walkSpeed = 5;


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
        if (!PauseManager.Instance.IsGamePaused)
        {
            stateController.UpdateState();
            Jump();
        }
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Run();
        }
        else
        {
            Walk();
        }

        Idle();
    }

    private void Idle()
    {
        if (Mathf.Abs(rb.velocity.x) < 0.1f && Mathf.Abs(rb.velocity.z) < 0.1f)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
    }

    private void Run()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 inputMovement = new Vector3(horizontalInput, 0, verticalInput);

        Vector3 cameraForward = cameraTransform.forward;
        cameraForward.y = 0;

        Vector3 movement = cameraForward * inputMovement.z + cameraTransform.right * inputMovement.x;
        movement.y = 0;

        rb.velocity = movement * runSpeed + new Vector3(0, rb.velocity.y, 0);
    }

    private void Walk()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 inputMovement = new Vector3(horizontalInput, 0, verticalInput);

        Vector3 cameraForward = cameraTransform.forward;
        cameraForward.y = 0;

        Vector3 movement = cameraForward * inputMovement.z + cameraTransform.right * inputMovement.x;
        movement.y = 0;

        rb.velocity = movement * walkSpeed + new Vector3(0, rb.velocity.y, 0);
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
