using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainCar2 : MonoBehaviour
{

    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;
    public float projectileSpeed = 100f;
    public Transform target;

    public float delay = 5f;
    public float delay2 = 10f;

    public int projectileAmount = 10;
    public float fireRate = 0.5f;

    private Rigidbody2D rb;

    private void Start()
    {
        StartCoroutine(AttackCooldown());
        target = GameObject.FindWithTag("Player").transform;
    }

    public IEnumerator FireProjectile()
    {
        for (int i = 1; i <= projectileAmount; i++)
        {
            GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, transform.rotation);
            rb = projectile.GetComponent<Rigidbody2D>();
            projectile.GetComponent<Projectile>().GetFired(gameObject);
            rb.gravityScale = 0f;

            if (target != null)
            {
                Vector2 direction = (target.position - transform.position).normalized;
                rb.velocity = direction * projectileSpeed;

                // Optional: rotate to face target
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                projectile.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
            }

            Destroy(projectile, 1);
            SoundManager.Instance.PlayPewSound();
            yield return new WaitForSeconds(fireRate);
        }
        
    }

    private IEnumerator AttackCooldown()
    {
        // Wait before first call
        yield return new WaitForSeconds(delay);

        // Loop forever
        while (true)
        {
            StartCoroutine(FireProjectile());
            yield return new WaitForSeconds(delay2);
        }
    }
}
