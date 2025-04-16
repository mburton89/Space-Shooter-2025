using UnityEngine;

public class StevenPet : MonoBehaviour
{
    public Transform target;
    public float followDistance = 1.5f;
    public float maxSpeed = 5f;
    public float minSpeed = 0.1f;
    public float slowDownRadius = 3f; // How early the pet starts slowing down

    void Update()
    {
        if (target == null) return;

        Vector2 currentPos = transform.position;
        Vector2 targetPos = target.position;

        Vector2 direction = targetPos - currentPos;
        float distance = direction.magnitude;

        if (distance > followDistance)
        {
            direction.Normalize();

            // 🧠 Speed decreases as pet gets closer
            float t = Mathf.Clamp01((distance - followDistance) / slowDownRadius);
            float currentSpeed = Mathf.Lerp(minSpeed, maxSpeed, t);

            // 🐾 Move with easing
            transform.position += (Vector3)(direction * currentSpeed * Time.deltaTime);
        }
    }
}
