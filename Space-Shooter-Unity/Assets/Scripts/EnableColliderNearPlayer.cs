using UnityEngine;

public class EnableColliderNearPlayer : MonoBehaviour
{
    public float activationRadius = 10f;
    public float checkInterval = 0.5f; // Time between checks (seconds)

    private Transform player;
    private Collider2D myCollider;

    void Start()
    {
        myCollider = GetComponent<Collider2D>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (player == null)
        {
            Debug.LogWarning("Player not found. Make sure the Player is tagged 'Player'.");
            return;
        }

        InvokeRepeating(nameof(CheckDistanceToPlayer), 0f, checkInterval);
    }

    void CheckDistanceToPlayer()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);
        myCollider.enabled = (distance < activationRadius);
    }
}
