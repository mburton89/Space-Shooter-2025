using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [Header(" ===== General ===== ")]
    public Rigidbody2D rb;
    public GameObject explosionPrefab;
    public List<GameObject> powerUpPrefabs;
    public float invincTime;
    private ParticleSystem thrustParticles;
    private Collider2D col;

    [Header(" ===== Health Settings ===== ")]
    public int currentHealth;
    public int maxHealth;

    [Header(" ===== Movement Settings ===== ")]
    public float acceleration;
    public float currentMovementSpeed;
    public float maxMovementSpeed;
    private bool canMove = true;

    [Header(" ====== Attack Settings ===== ")]
    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;
    public float fireRate;
    public float projectileSpeed;
    public bool readyToShoot;
    public bool readyToDeploy;

    [Header("- - - Mines - - -")]
    public int minesRemaining;
    public float deployRate;
    public Transform mineSpawnPoint;
    public GameObject minePrefab;

    [Header("- - - MegaLaser Settigns - - - ")]
    public bool megaLaserReady;
    public float megaLaserDuration;
    //public float megaLaserCooldown;
    public GameObject megaLaserPrefab;
    public Transform megaLaserSpawnPoint;

    [Header("Dash Settings")]
    public float dashInvincTime;
    public float dashForce;
    public float dashCooldown;
    public bool readyToDash = true;
    public float shadowFadeDuration = 0.5f;
    public int shadowCount = 3;
    public float spawnDelay = 0.05f;
    public GameObject dashShadowPrefab;


    void Awake()
    {
        thrustParticles = GetComponentInChildren<ParticleSystem>();
        col = GetComponent<Collider2D>();
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
        StartCoroutine(CoolDownBullet());
        //SoundManager.Instance.PlayPewSound();

        if (SoundsManager.Instance != null)
        {
            SoundsManager.Instance.PlayVariedSFX(SoundsManager.Instance.source21);
        }

    }

    public void DeployMine()
    {
        GameObject mine = Instantiate(minePrefab, mineSpawnPoint.position, transform.rotation);
        mine.GetComponent<Mine>().GetDeployed(gameObject);
        Destroy(mine, 10);
        StartCoroutine(CoolDownMine());
    }

    private IEnumerator CoolDownMine()
    {
        readyToDeploy = false;
        yield return new WaitForSeconds(deployRate);
        readyToDeploy = true;
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
            StartCoroutine(Invincibility());
        }

        if (SoundsManager.Instance != null)
        {
            SoundsManager.Instance.PlayVariedSFX(SoundsManager.Instance.source3);
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
            SpawnPowerUp();
        }

        if (GetComponent<PlayerShip>())
        {
            GameManager.Instance.GameOver();
        }

        //SoundManager.Instance.PlayExplosionSound();

        if (SoundsManager.Instance != null)
        {
            SoundsManager.Instance.PlayVariedSFX(SoundsManager.Instance.source7);
        }

        Destroy(gameObject);
    }

    private IEnumerator CoolDownBullet()
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

            if (SoundsManager.Instance != null)
            {
                SoundsManager.Instance.PlayVariedSFX(SoundsManager.Instance.source10);
            }
        }
    }

    public void MegaLaser()
    {
        if (megaLaserReady)
        {
            //Rigidbody2D rb = GetComponent<Rigidbody2D>();
            //rb.velocity = Vector2.zero;
            GameObject laser = Instantiate(megaLaserPrefab, megaLaserSpawnPoint.position, megaLaserSpawnPoint.rotation, megaLaserSpawnPoint);

            //Use these two for a powerup recharging laser
            megaLaserReady = false;
            HUD.Instance.DisplayMLaser(megaLaserReady);

            //StartCoroutine(MegaLaserCooldown());  //Use for a cooldown laser

            //StartCoroutine(MovementCooldown());
            Destroy(laser, megaLaserDuration);

            if (SoundsManager.Instance != null)
            {
                SoundsManager.Instance.PlayVariedSFX(SoundsManager.Instance.source19);
            }
        }
    }

    /*  //Use for a cooldown laser
    private IEnumerator MegaLaserCooldown()
    {
        megaLaserReady = false;
        HUD.Instance.DisplayMLaser(megaLaserReady);
        yield return new WaitForSeconds(megaLaserCooldown);
        megaLaserReady = true;
        HUD.Instance.DisplayMLaser(megaLaserReady);
    }
    */

    private IEnumerator MovementCooldown()
    {
        canMove = false;
        yield return new WaitForSeconds(megaLaserDuration);
        canMove = true;
    }

    public void SpawnPowerUp()
    {
        int index = Random.Range(0, powerUpPrefabs.Count);
        Instantiate(powerUpPrefabs[index], transform.position, transform.rotation, null);
    }

    public void Dash()
    {
        if (readyToDash)
        {
            Vector3 mouseScreenPos = Input.mousePosition;
            mouseScreenPos.z = Mathf.Abs(Camera.main.transform.position.z);
            Vector3 mouseWorldPos3D = Camera.main.ScreenToWorldPoint(mouseScreenPos);
            Vector2 direction = (new Vector2(mouseWorldPos3D.x, mouseWorldPos3D.y) - rb.position).normalized;

            readyToDash = false;
            HUD.Instance.DisplayDash(readyToDash);

            rb.velocity = direction * dashForce;

            StartCoroutine(DashInvincibility());
            StartCoroutine(DashCooldown());
            StartCoroutine(SpawnDashShadows(direction));

            if (SoundsManager.Instance != null)
            {
                SoundsManager.Instance.PlayVariedSFX(SoundsManager.Instance.source5);
            }
        }
    }
    /*
    private void RotateShadow(Vector2 direction)
    {
        float angle = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;

        shadow.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
    }
    */
    private IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(dashCooldown);
        readyToDash = true;
        HUD.Instance.DisplayDash(readyToDash);
    }

    private IEnumerator SpawnDashShadows(Vector2 direction)
    {
        for (int i = 0; i < shadowCount; i++)
        {
            yield return new WaitForSeconds(spawnDelay);

            GameObject shadow = Instantiate(dashShadowPrefab, transform.position, Quaternion.identity);
            //RotateShadow(direction);
            float angle = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
            shadow.transform.rotation = Quaternion.Euler(0f, 0f, angle);

            StartCoroutine(FadeShadow(shadow));
        }
    }

    private IEnumerator FadeShadow(GameObject shadow)
    {
        SpriteRenderer sr = shadow.GetComponent<SpriteRenderer>();

        Color startColor = sr.color;
        startColor.a = 1f;
        sr.color = startColor;

        yield return null; // Let it render before fading

        float timeElapsed = 0f;

        while (timeElapsed < shadowFadeDuration)
        {
            timeElapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, timeElapsed / shadowFadeDuration);
            sr.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            yield return null;
        }

        Destroy(shadow);
    }

    private IEnumerator Invincibility()
    {
        //Debug.Log("Invincibility started");
        col.enabled = false;
        yield return new WaitForSeconds(invincTime);
        col.enabled = true;
        //Debug.Log("Invincibility ended");
    }

    private IEnumerator DashInvincibility()
    {
        //Debug.Log("Invincibility started");
        col.enabled = false;
        yield return new WaitForSeconds(dashInvincTime);
        col.enabled = true;
        //Debug.Log("Invincibility ended");
    }
}
