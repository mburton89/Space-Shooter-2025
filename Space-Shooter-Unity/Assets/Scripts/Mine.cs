using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    public int damageToGive = 100;
    public GameObject mineExplosionPrefab;
    GameObject deployingShip;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Ship>() && collision.gameObject != deployingShip)
        {
            collision.GetComponent<Ship>().TakeDamage(damageToGive);

            GameObject explosion = Instantiate(mineExplosionPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
            Destroy(explosion, 3);
        }
    }

    public void GetDeployed(GameObject shipThatDeployed)
    {
        deployingShip = shipThatDeployed;
    }
}