using UnityEngine;

public class SplatOnHit : MonoBehaviour
{
    public GameObject particleEffectPrefab; // Assign a particle system prefab in Inspector

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HandleCollision(collision.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HandleCollision(collision.gameObject);
    }

    private void HandleCollision(GameObject other)
    {
        // Check if the other object has a Ship or Projectile component
        if (other.GetComponent<Ship>() != null || other.GetComponent<Projectile>() != null)
        {
            if (particleEffectPrefab != null)
            {
                // Get this object's color from its SpriteRenderer
                SpriteRenderer sr = GetComponent<SpriteRenderer>();
                Color baseColor = sr != null ? sr.color : Color.white;

                // Instantiate the particle effect
                GameObject particleObj = Instantiate(particleEffectPrefab, transform.position, Quaternion.identity);

                // Apply color to the particle system
                ParticleSystem ps = particleObj.GetComponent<ParticleSystem>();
                if (ps != null)
                {
                    var main = ps.main;
                    main.startColor = baseColor;
                }
            }

            Destroy(gameObject); // Destroy this object
        }
    }
}
