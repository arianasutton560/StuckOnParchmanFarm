using UnityEngine;

//This Script allows the player to control their character
public class PlayerController : MonoBehaviour
{

    public float speed = 5f; // Speed of the player
    public float turnSpeed = 5f; // Force of the player's jump
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        // Get input from the horizontal and vertical axes (WASD or Arrow Keys)
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate the movement direction based on input
        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput).normalized;

        // Move the player in the direction of the input
        transform.Translate(movement * speed * Time.deltaTime, Space.World);

        // If there is movement input, rotate the player to face the movement direction
        if (movement.magnitude > 0)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, turnSpeed * Time.deltaTime);
        }

    }
}
