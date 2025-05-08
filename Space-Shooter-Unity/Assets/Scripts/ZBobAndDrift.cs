using UnityEngine;

public class ZBobAndDrift : MonoBehaviour
{
    public float bobSpeed = 2f;              // Speed of bobbing on Z axis
    public float bobAmount = 0.1f;           // Scale range on Z axis (e.g., +/- 0.1)

    public float driftRadius = 0.5f;         // Max distance from original position
    public float driftSpeed = 0.5f;          // Speed of drifting movement

    private Vector3 initialPosition;
    private Vector3 targetOffset;
    private float driftTimer;
    private float zBobTimer;

    void Start()
    {
        initialPosition = transform.position;
        PickNewDriftTarget();
        driftTimer = Random.value * Mathf.PI * 2f; // randomize start
        zBobTimer = Random.value * Mathf.PI * 2f;
    }

    void Update()
    {
        // Z-axis bobbing scale
        zBobTimer += Time.deltaTime * bobSpeed;
        float zScale = 1f + Mathf.Sin(zBobTimer) * bobAmount;
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, zScale);

        // Smooth drifting around initial position
        driftTimer += Time.deltaTime * driftSpeed;
        transform.position = initialPosition + Vector3.Lerp(Vector3.zero, targetOffset, (Mathf.Sin(driftTimer) + 1f) / 2f);

        // Occasionally pick a new drift direction
        if (driftTimer > Mathf.PI * 2f)
        {
            driftTimer = 0f;
            PickNewDriftTarget();
        }
    }

    void PickNewDriftTarget()
    {
        float angle = Random.Range(0f, Mathf.PI * 2f);
        float radius = Random.Range(0.1f, driftRadius);
        targetOffset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f) * radius;
    }
}
