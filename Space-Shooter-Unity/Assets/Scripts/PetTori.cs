using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetTori : MonoBehaviour

{
    public Transform PlayerShip;            // The player to follow
    public float followSpeed = 2f;      // Speed at which it follows
    public float followDistance = 1.5f; // Minimum distance to keep from the player

    void Update()
    {
        if (PlayerShip == null) return;

        // Calculate the distance between follower and player
        float distance = Vector2.Distance(transform.position, PlayerShip.position);

        if (distance > followDistance)
        {
            // Move toward the player
            Vector2 direction = (PlayerShip.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, PlayerShip.position, followSpeed * Time.deltaTime);
        }
    }
}

