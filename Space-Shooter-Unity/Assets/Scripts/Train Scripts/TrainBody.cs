using UnityEngine;
using System.Collections;

public class TrainBody : MonoBehaviour
{
    public Transform laserSpawnPoint, enemySpawnPoint;
    public GameObject laserPrefab, trainBabyPrefab;

    public bool isLaserSpawner, isEnemySpawner;
    private bool readyToFire, readyToSpawn;

    private SpriteRenderer sr;
    public float flashDuration = 0.1f;
    private Color originalColor;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
    }

    private void Update()
    {

        if (isLaserSpawner && !readyToFire)
        {
            FireLaser();
        }
        if (isEnemySpawner && !readyToSpawn)
        {
            SpawnEnemy();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // make the player ship take damage
        Ship ship = collision.GetComponent<Ship>();
        if (ship != null)
        {
            ship.TakeDamage(1);

            if (ship.currentHealth <= 0)
            {
                ship.Explode();
            }

            return;
        }
    }

    public void FireLaser()
    {
        GameObject laser = Instantiate(laserPrefab, laserSpawnPoint.position, transform.rotation);
        laser.GetComponent<Rigidbody2D>().AddForce(transform.up * 1);
        Destroy(laser, 2f);
        StartCoroutine(LaserCoolDown());
    }

    public void SpawnEnemy()
    {
        GameObject clyde = Instantiate(trainBabyPrefab, enemySpawnPoint.position, transform.rotation);
        Destroy(clyde, 10f);
        StartCoroutine(EnemySpawnCoolDown());
    }

    // turns train car white to signify its been hurt
    public void OnHit()
    {
        StopAllCoroutines();
        StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        sr.color = Color.white;
        yield return new WaitForSeconds(flashDuration);
        sr.color = originalColor;
    }

    private IEnumerator LaserCoolDown()
    {
        readyToFire = true;
        yield return new WaitForSeconds(10f);
        readyToFire = false;
    }

    private IEnumerator EnemySpawnCoolDown()
    {
        readyToSpawn = true;
        yield return new WaitForSeconds(30F);
        readyToSpawn = false;
    }
}

