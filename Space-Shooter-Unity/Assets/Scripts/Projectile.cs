using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damageToGive = 1;
    GameObject firingShip;
    public bool IsMegaLaser;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Ignore collisions with the firing ship
        if (collision.gameObject == firingShip)
            return;

        print(collision.gameObject);
        // If it's a ship, deal damage
        Ship ship = collision.GetComponent<Ship>();
        if (ship != null)
        {
            collision.GetComponent<Ship>().TakeDamage(damageToGive);

            if (!IsMegaLaser)
            {
                Destroy(gameObject);
            }

            return;
        }

        // If it's the train body, flash white and register projectile
        TrainBody trainBody = collision.GetComponent<TrainBody>();
        print(gameObject.name);
        if (trainBody != null && gameObject.name.Contains("Projectile") && !gameObject.name.Contains("Lazer"))
        {
            trainBody.OnHit();
            TrainSpawner spawner = FindObjectOfType<TrainSpawner>();
            if (spawner != null)
            {
                spawner.RegisterHit();
            }

            Destroy(gameObject);
            return;
        }
    }

    public void GetFired(GameObject shipThatFired)
    { 
        firingShip = shipThatFired;
    }
}
