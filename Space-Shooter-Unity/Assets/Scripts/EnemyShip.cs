using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : Ship
{
    Transform target;
    public bool isShooter;
    public bool isMiner; // I need a better name for this

    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<PlayerShip>().transform;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerShip>())
        {
            collision.gameObject.GetComponent<PlayerShip>().TakeDamage(1); //Damage PLAYER ship
            Explode(); //Explod ENEMY Ship
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            FollowTarget();
        }

        if (isShooter && target != null && readyToShoot)
        {
            FireProjectile();
        }

        //if (isMiner && target != null && readyToDeploy)
        //{
        //    DeployMine();
        //}
    }

    void FollowTarget()
    {
        Vector2 directionToFace = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y);
        transform.up = directionToFace;
        Thrust();
    }
}
