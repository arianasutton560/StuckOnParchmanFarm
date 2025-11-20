using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float speed = 5f;

    // mouse sensitivity
    public float mouseSensitivityX = 2f;
    public float mouseSensitivityY = 2f;

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
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivityX;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivityY;

        // Rotate the player body only on the Y axis
        transform.Rotate(Vector3.up * mouseX);

        // Rotate the camera on the X axis only
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        PlayerCameraRoot.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Move relative to the players forward direction (NOT camera)
        Vector3 move = transform.forward * vertical + transform.right * horizontal;

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





