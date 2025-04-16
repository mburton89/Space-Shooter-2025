using UnityEngine;

public class MattPet : MonoBehaviour
{
    public Transform target;               // Object to follow
    public float maxSpeed = 5f;            // Max movement speed
    public float acceleration = 10f;       // How fast the pet speeds up
    public float followDistance = 1.5f;    // Desired distance from the target

    private Vector2 velocity = Vector2.zero;

    void Update()
    {
        if (target == null) return;

        Vector2 currentPos = transform.position;
        Vector2 direction = (Vector2)(target.position - transform.position);
        float distance = direction.magnitude;

        if (distance > followDistance)
        {
            // Calculate desired velocity toward the target (at max speed)
            Vector2 desiredVelocity = direction.normalized * maxSpeed;

            // Accelerate smoothly toward the desired velocity
            velocity = Vector2.MoveTowards(velocity, desiredVelocity, acceleration * Time.deltaTime);
        }
        else
        {
            // Gradually slow down when within follow range
            velocity = Vector2.MoveTowards(velocity, Vector2.zero, acceleration * Time.deltaTime);
        }

        // Apply the velocity
        transform.position += (Vector3)(velocity * Time.deltaTime);
    }
}
