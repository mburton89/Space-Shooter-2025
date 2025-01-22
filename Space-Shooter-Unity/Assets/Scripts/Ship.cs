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

    // Start is called before the first frame update
    void Start()
    {
        
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
    }

    public void Thrust()
    {
        rb.AddForce(transform.up * acceleration);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        { 
            Explode();
        }
    }

    public void Explode()
    { 
        Destroy(gameObject);
    }
}
