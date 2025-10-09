using UnityEngine;

//This Script allows the player to control their character 
public class PlayerController : MonoBehaviour
{

    public float speed = 5f; // Speed of the player
    public float turnSpeedX = 200f; // Force of the player's jump
    public float turnSpeedY = 200f; // Force of the player's jump
    public Transform PlayerCameraRoot;
    private float xRotation = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleMouseLook();
    }

    void HandleMovement()
    {
        float mouseX = Input.GetAxis("Mouse X") * turnSpeedX * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * turnSpeedY * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        PlayerCameraRoot.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    void HandleMouseLook()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 move = transform.right * horizontal + transform.forward * vertical;
        transform.position += move * speed * Time.deltaTime;

    }   


}
