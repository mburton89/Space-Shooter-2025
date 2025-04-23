using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [Header("Game Objects/Prefabs")]
    public Rigidbody2D rb;
    public GameObject projectilePrefab;
    public GameObject minePrefab;
    public Transform projectileSpawnPoint;
    public GameObject dashShadowPrefab;
    private GameObject dashShadowInstance;
    private ParticleSystem thrustParticles;
    public GameObject explosionPrefab;

    [Header("Heath Settings")]
    public int currentHealth;
    public int maxHealth;

    [Header("Movement Settings")]
    public float acceleration;
    public float currentMovementSpeed;
    public float maxMovementSpeed;
    public float projectileSpeed;

    [Header("Dash Settings")]
    public float dashForce;
    public float dashCooldown;
    public bool readyToDash;
    public float shadowFadeDuration = 0.5f;

    [Header("Attack Settings")]
    public float fireRate;
    public bool readyToShoot;
    public int minesRemaining;


    void Awake()
    {
        thrustParticles = GetComponentInChildren<ParticleSystem>();
    }

    private void FixedUpdate()
    {
        if ((rb.velocity.magnitude > maxMovementSpeed) && (readyToDash))
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

    public void Dash()
    {
        if (readyToDash)
        {
            // Get mouse position in world space
            Vector3 mouseScreenPos = Input.mousePosition;
            mouseScreenPos.z = Mathf.Abs(Camera.main.transform.position.z);
            Vector3 mouseWorldPos3D = Camera.main.ScreenToWorldPoint(mouseScreenPos);
            Vector2 direction = (new Vector2(mouseWorldPos3D.x, mouseWorldPos3D.y) - rb.position).normalized;

            readyToDash = false;
            HUD.Instance.DisplayDash(readyToDash);

            dashShadowInstance = Instantiate(dashShadowPrefab, transform.position, Quaternion.identity);
            RotateShadow(direction);

            // Dash with instant velocity
            rb.velocity = direction * dashForce;

            StartCoroutine(DashCooldown());
            StartCoroutine(FadeShadow());
        }
    }

    private void RotateShadow(Vector2 direction)
    {
        // Calculate the angle between the shadow and the dash direction
        float angle = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;

        // Apply the calculated angle to the shadow's rotation
        dashShadowInstance.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
    }

    IEnumerator FadeShadow()
    {
        float timeElapsed = 0f;

        // Gradually fade the shadow out
        while (timeElapsed < shadowFadeDuration)
        {
            timeElapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, timeElapsed / shadowFadeDuration); // Fading from full opacity to invisible
            Color shadowColor = dashShadowInstance.GetComponent<SpriteRenderer>().color;
            dashShadowInstance.GetComponent<SpriteRenderer>().color = new Color(shadowColor.r, shadowColor.g, shadowColor.b, alpha);
            yield return null;
        }

        // Destroy shadow after it has faded out
        Destroy(dashShadowInstance);
    }

    private IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(dashCooldown);
        readyToDash = true;
        HUD.Instance.DisplayDash(readyToDash);
    }
}
