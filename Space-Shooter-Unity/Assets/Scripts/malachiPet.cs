using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class malachiPet : MonoBehaviour
{
    public Transform player;  // Reference to the player's transform
    public float followDistance = 2f;  // Distance to maintain between the pet and the player
    public float followSpeed = 3f;  // Speed at which the pet follows

    private void Update()
    {
        if (player != null)
        {
            // Calculate the direction from the pet to the player
            Vector3 direction = player.position - transform.position;

            // If the distance is greater than the follow distance, move the pet
            if (direction.magnitude > followDistance)
            {
                direction.Normalize();  // Normalize the direction vector to avoid faster movement diagonally
                transform.position += direction * followSpeed * Time.deltaTime;  // Move pet towards player
            }
        }
    }
}
