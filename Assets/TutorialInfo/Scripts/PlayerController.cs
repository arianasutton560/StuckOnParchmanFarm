using UnityEngine;

//This Script allows the player to control their character
public class PlayerController : MonoBehaviour
{

    public float speed = 5f; // Speed of the player
    public float turnSpeedX = 5f; // Force of the player's jump
    public float turnSpeedY = 4f; // Force of the player's jump
    public Transform playerView;
    private float xRotation = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleMouseLook();
    }

    void HandleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput) * speed * Time.deltaTime;
        transform.Translate(movement * speed * Time.deltaTime, Space.World);
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * turnSpeedX;
        float mouseY = Input.GetAxis("Mouse Y") * turnSpeedY;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerView.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }   


}
