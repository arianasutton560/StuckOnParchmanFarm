using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float turnSpeedX = 200f;
    public float turnSpeedY = 200f;
    public Transform PlayerCameraRoot;

    private CharacterController controller;
    private float xRotation = 0f;
    private Vector3 velocity;
    public float gravity = -9.81f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        HandleLook();
        HandleMovement();
    }

    void HandleLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * turnSpeedX * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * turnSpeedY * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        PlayerCameraRoot.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Get camera direction
        Transform cam = Camera.main.transform;

        // Calculate camera forward and right directions, but flatten them (ignore camera tilt)
        Vector3 camForward = cam.forward;
        Vector3 camRight = cam.right;
        camForward.y = 0f;
        camRight.y = 0f;
        camForward.Normalize();
        camRight.Normalize();

        // Movement relative to camera
        Vector3 move = camForward * vertical + camRight * horizontal;

        controller.Move(move * speed * Time.deltaTime);

        // Apply gravity
        if (controller.isGrounded)
        {
            velocity.y = -2f;
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }

        controller.Move(velocity * Time.deltaTime);
    }

}



