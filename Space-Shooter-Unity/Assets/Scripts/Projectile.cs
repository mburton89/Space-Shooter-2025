using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damageToGive = 1;
    GameObject firingShip;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Ignore collisions with the firing ship
        if (collision.gameObject == firingShip)
            return;

        // If it's a ship, deal damage
        Ship ship = collision.GetComponent<Ship>();
        if (ship != null)
        {
            ship.TakeDamage(damageToGive);
            Destroy(gameObject);
            return;
        }

        // If it's the train body, flash white
        TrainBody trainBody = collision.GetComponent<TrainBody>();
        if (trainBody != null)
        {
            trainBody.OnHit();
            Destroy(gameObject);
            return;
        }
    }

    public void GetFired(GameObject shipThatFired)
    { 
        firingShip = shipThatFired;
    }


}
