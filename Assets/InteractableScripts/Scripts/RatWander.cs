using UnityEngine;

public class RatWander : MonoBehaviour
{
    public float moveSpeed = 1.2f;
    public float changeDirectionTime = 2f;

    public Vector2 minBounds = new Vector2(-15, -9.5f);
    public Vector2 maxBounds = new Vector2(0, 0);

    private Vector3 direction;
    private float timer;

    public float turnSpeed = 6f;  
    void Start()
    {
        PickRandomDirection();
    }

    void Update()
    {
        // Move
        transform.position += direction * moveSpeed * Time.deltaTime;

        //prison cell bounds
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, minBounds.x, maxBounds.x),
            transform.position.y,
            Mathf.Clamp(transform.position.z, minBounds.y, maxBounds.y)
        );

        // Smooth rotation to face movement direction
        if (direction.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                targetRotation,
                Time.deltaTime * turnSpeed
            );
        }

        // Timer to change direction
        timer += Time.deltaTime;
        if (timer >= changeDirectionTime)
        {
            PickRandomDirection();
            timer = 0;
        }
    }

    void PickRandomDirection()
    {
        direction = new Vector3(
            Random.Range(-1f, 1f),
            0,
            Random.Range(-1f, 1f)
        ).normalized;
    }
}

