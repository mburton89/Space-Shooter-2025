using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject projectilePrefab;
    public GameObject minePrefab;
    public Transform projectileSpawnPoint;
    public Transform mineSpawnPoint;

    public int currentHealth;
    public int maxHealth;

    public float acceleration;
    public float currentMovementSpeed;
    public float maxMovementSpeed;
    public float projectileSpeed;

    public float fireRate;
    public float deployRate;

    private ParticleSystem thrustParticles;

    public GameObject explosionPrefab;

    public bool readyToShoot;
    public bool readyToDeploy;

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
        StartCoroutine(CoolDownBullet());
        SoundManager.Instance.PlayPewSound();
    }

    public void DeployMine()
    {
        GameObject mine = Instantiate(minePrefab, mineSpawnPoint.position, transform.rotation);
        mine.GetComponent<Mine>().GetDeployed(gameObject);
        Destroy(mine, 10);
        StartCoroutine(CoolDownMine());
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

        SoundManager.Instance.PlayExplosionSound();

        Destroy(gameObject);
    }

    private IEnumerator CoolDownBullet()
    {
        readyToShoot = false;
        yield return new WaitForSeconds(fireRate);
        readyToShoot = true;
    }
    private IEnumerator CoolDownMine()
    {
        readyToDeploy = false;
        yield return new WaitForSeconds(deployRate);
        readyToDeploy = true;
    }
}
