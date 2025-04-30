using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [Header(" ===== GameObjects/Prefabs ===== ")]
    public Rigidbody2D rb;
    public GameObject projectilePrefab;
    public GameObject minePrefab;
    public Transform projectileSpawnPoint;
    public GameObject explosionPrefab;
    private ParticleSystem thrustParticles;

    [Header(" ===== Health Settings ===== ")]
    public int currentHealth;
    public int maxHealth;

    [Header(" ===== Movement Settings ===== ")]
    public float acceleration;
    public float currentMovementSpeed;
    public float maxMovementSpeed;
    private bool canMove = true;

    [Header(" ====== Attack Settings ===== ")]
    public float fireRate;
    public float projectileSpeed;
    public bool readyToShoot;
    public int minesRemaining;

    [Header("- - - MegaLaser Settigns - - - ")]
    public bool megaLaserReady;
    public float megaLaserDuration;
    public float megaLaserCooldown;
    public GameObject megaLaserPrefab;
    public Transform megaLaserSpawnPoint;

    void Awake()
    {
        thrustParticles = GetComponentInChildren<ParticleSystem>();
    }

    private void FixedUpdate()
    {
        if (rb.velocity.magnitude > maxMovementSpeed )
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
        SoundManager.Instance.PlayPewSound();
    }

    public void Thrust()
    {
        if (!canMove) return;

        rb.AddForce(transform.up * acceleration);
        thrustParticles.Emit(1);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (GetComponent<PlayerShip>())
        {
            HUD.Instance.DisplayHealth(currentHealth, maxHealth);
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

    private IEnumerator CoolDown()
    {
        readyToShoot = false;
        yield return new WaitForSeconds(fireRate);
        readyToShoot = true;
    }

    public void DropMine()
    {
        if (minesRemaining > 0)
        {
            GameObject mine = Instantiate(minePrefab, transform.position, transform.rotation);
            mine.GetComponent<Projectile>().GetFired(gameObject);
            minesRemaining--;
            if (GetComponent<PlayerShip>())
            {
                HUD.Instance.DisplayMineCount(minesRemaining);
            }
        }
    }

    public void MegaLaser()
    {
        if (megaLaserReady)
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.zero;
            GameObject laser = Instantiate(megaLaserPrefab, megaLaserSpawnPoint.position, megaLaserSpawnPoint.rotation, megaLaserSpawnPoint);

            StartCoroutine(MegaLaserCooldown());
            StartCoroutine(MovementCooldown());
            Destroy(laser, megaLaserDuration);
        }
    }

    private IEnumerator MegaLaserCooldown()
    {
        megaLaserReady = false;
        HUD.Instance.DisplayMLaser(megaLaserReady);
        yield return new WaitForSeconds(megaLaserCooldown);
        megaLaserReady = true;
        HUD.Instance.DisplayMLaser(megaLaserReady);
    }

    private IEnumerator MovementCooldown()
    {
        canMove = false;
        yield return new WaitForSeconds(megaLaserDuration);
        canMove = true;
    }
}