using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject projectilePrefab;
    public GameObject minePrefab;
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

    public int minesRemaining;

    public List<GameObject> powerUpPrefabs;

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
        //SoundManager.Instance.PlayPewSound();
        SoundsManager.Instance.PlayVariedSFX(SoundsManager.Instance.source21);
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
        SoundsManager.Instance.PlaySFX(SoundsManager.Instance.source3);
        SoundsManager.Instance.PlaySFX(SoundsManager.Instance.source4);
    }

    public void Explode()
    {
        GameObject explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(explosion, 5);
        
        if (GetComponent<EnemyShip>())
        { 
            EnemyShipSpawner.Instance.CountEnemyShips();
            //SpawnPowerUp();
        }

        if (GetComponent<PlayerShip>())
        {
            SoundsManager.Instance.PlaySFX(SoundsManager.Instance.source10);
            GameManager.Instance.GameOver();
        }

        //SoundManager.Instance.PlayExplosionSound();
        SoundsManager.Instance.PlaySFX(SoundsManager.Instance.source7);
        Destroy(gameObject);
    }

    private IEnumerator CoolDown()
    {
        readyToShoot = false;
        yield return new WaitForSeconds(fireRate);
        readyToShoot = true;
    }

    //public void SpawnPowerUp()
    //{
    //  int index = Random.Range(0, powerUpPrefabs.Count);
    //Instantiate(powerUpPrefabs[index],transform.position, transform.rotation, null);
    //}


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
}
