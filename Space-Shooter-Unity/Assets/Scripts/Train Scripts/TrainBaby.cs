using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainBaby : Ship
{
    Transform target;

    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<PlayerShip>().transform;
    }

    // Update is called once per frame
    void Update()
    {
       if (target != null)
        {
            FollowTarget();
        } 
    }
    void FollowTarget()
    {
        Vector2 directionToFace = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y);
        transform.up = directionToFace;
        Thrust();
    }
}
