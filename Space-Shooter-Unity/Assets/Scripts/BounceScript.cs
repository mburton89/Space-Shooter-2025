using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class BounceScript : MonoBehaviour
{
    public float bounceForce = 500f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody2D rb = other.attachedRigidbody;
            if (rb != null)
            {
                Vector2 bounceDirection = (other.transform.position - transform.position).normalized;
                rb.AddForce(bounceDirection * bounceForce);
            }
        }
    }
}
