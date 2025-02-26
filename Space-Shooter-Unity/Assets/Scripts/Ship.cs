using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;

    public int currentHealth;
    public int maxHealth;

    public float acceleration;
    public float currentMovementSpeed;
    public float maxMovementSpeed;
    public float projectileSpeed;

    public float fireRate;

    private ParticleSystem thrustParticles;

    public GameObject explosionPrefab;

    public bool readyToShoot;

    void Awake()
    {
        thrustParticles = GetComponentInChildren<ParticleSystem>();
    }

    private void FixedUpdate()
    {
        if (rb.velocity.magnitude > maxMovementSpeed)
        { 
            rb.velocity = rb.velocity.normalized * maxMovementSpeed;
        }
    }

    public void FireProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, transform.rotation);
        projectile.GetComponent<Rigidbody2D>().AddForce(transform.up * projectileSpeed);
        projectile.GetComponent<Projectile>().GetFired(gameObject);
        Destroy(projectile, 5);
        StartCoroutine(CoolDown());
    }

    public void Thrust()
    {
        rb.AddForce(transform.up * acceleration);
        thrustParticles.Emit(1);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (GetComponent<PlayerShip>())
        { 
            HUD.Instance.DisplayHealth(currentHealth , maxHealth);
        }

        if (currentHealth <= 0)
        { 
            Explode();
        }
    }

    public void Explode()
    {
        GameObject explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(explosion, 5);
        
        if (GetComponent<EnemyShip>())
        { 
            EnemyShipSpawner.Instance.CountEnemyShips();
        }

        if (GetComponent<PlayerShip>())
        {
            GameManager.Instance.GameOver();
        }

        Destroy(gameObject);
    }

    private IEnumerator CoolDown()
    {
        readyToShoot = false;
        yield return new WaitForSeconds(fireRate);
        readyToShoot = true;
    }
}
