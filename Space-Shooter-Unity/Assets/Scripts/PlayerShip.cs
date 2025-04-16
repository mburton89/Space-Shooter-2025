using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : Ship
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            Thrust();
        }
        if (Input.GetMouseButtonDown(0))
        {
            FireProjectile();
        }
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    DeployMine();
        //}

        FollowMouse();
    }

    void FollowMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
        Vector2 directionToFace = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        transform.up = directionToFace;
    }
}
