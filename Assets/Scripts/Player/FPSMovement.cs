using UnityEngine;
using UnityEngine.InputSystem;

public class FPSMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpHeight = 1.5f;
    [SerializeField] float sprintSpeed = 8f;
    [SerializeField] float gravity = -20f;

    [Header("Mouse Look")]
    [SerializeField]public float mouseSensitivity = 0.1f;
    [SerializeField] float maxLookAngle = 80f;

    [Header("References")]
    [SerializeField] Transform playerCamera;

    [Header("Breathing")]
    [SerializeField] float breathingAmount = 0.025f;
    [SerializeField] float breathingSpeed = 1.5f;
    Vector3 cameraStartPosition;

    CharacterController characterController;



    Vector2 moveInput;
    Vector2 lookInput;
    bool isSprinting;
    public bool isShoot;
    public bool zoom;
    float verticalVelocity;
    float cameraPitch;
    bool curserLocked = true;

    
    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        cameraStartPosition = playerCamera.localPosition;
        zoom = false;
    }

    void Start()
    {
        SetCurserState(true); //game start curser appear
    }

    void Update()
    {
        MovePlayer();
        LookAround();
        BreathingMotion();



    }

    public void ChangeRotationSpeed(float amount)//scope rotation
    {
        mouseSensitivity = amount;
     }
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed && characterController.isGrounded)
        {
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    public void OnSprint(InputValue value)
    {
        isSprinting = value.isPressed;
    }


    public void OnShooting(InputValue value)
    {
        if (value.isPressed)
        {
            Debug.Log("Player Shoot");
            isShoot = true;
        }

    }

    public void OnZoom(InputValue value)
    {
        zoom = value.isPressed;
        Debug.Log("Zoom: " + zoom);
    }

    private void OnApplicationFocus(bool hasFocus)
    {
       SetCurserState(curserLocked);

    }

    public void SetCurserState(bool newState)
    {
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    }
    void MovePlayer()
    {
        Vector3 movement =
            transform.right * moveInput.x +
            transform.forward * moveInput.y;
        float currentSpeed = isSprinting ? sprintSpeed : moveSpeed;
        movement *= currentSpeed;

        if (characterController.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }

        verticalVelocity += gravity * Time.deltaTime;

        movement.y = verticalVelocity;

        characterController.Move(movement * Time.deltaTime);
    }

    void LookAround()
    {
        float mouseX = lookInput.x * mouseSensitivity;
        float mouseY = lookInput.y * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);

        cameraPitch -= mouseY;
        cameraPitch = Mathf.Clamp(
            cameraPitch,
            -maxLookAngle,
            maxLookAngle
        );

        playerCamera.localRotation =
            Quaternion.Euler(cameraPitch, 0f, 0f);
    }

    void BreathingMotion()
    {
        // Time.time creates a continuously changing value.
        // Sin() converts it into a smooth up-and-down movement.
        float breathing = Mathf.Sin(Time.time * breathingSpeed);

        // Move the camera slightly up and down.
        // breathingAmount keeps the movement very small.
        float verticalOffset = breathing * breathingAmount;

        playerCamera.localPosition =
            cameraStartPosition + Vector3.up * verticalOffset;
    }

}