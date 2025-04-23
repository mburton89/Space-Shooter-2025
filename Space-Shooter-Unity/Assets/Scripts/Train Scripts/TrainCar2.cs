using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainCar2 : MonoBehaviour
{

    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;
    public float projectileSpeed = 100f;

    public float delay = 5f;
    public float delay2 = 10f;

    public int projectileAmount = 10;
    public float fireRate = 0.5f;

    private void Start()
    {
        StartCoroutine(AttackCooldown());
    }

    public IEnumerator FireProjectile()
    {
        for (int i = 1; i <= projectileAmount; i++)
        {
            GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, transform.rotation);
            projectile.GetComponent<Rigidbody2D>().AddForce(transform.up * projectileSpeed);
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
