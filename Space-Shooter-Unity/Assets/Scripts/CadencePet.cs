using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CadencePet : MonoBehaviour
{
    public Transform player;              // Assign this via Inspector or automatically in Start
    public float followSpeed = 3f;        // Speed at which pet follows
    public float stopDistance = 1.2f;     // Distance to maintain from player

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Auto-find player if not set
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
        }
    }

    void FixedUpdate()
    {
        if (player == null)
            return;

        Vector2 direction = (player.position - transform.position);
        float distance = direction.magnitude;

        if (distance > stopDistance)
        {
            Vector2 moveDir = direction.normalized;
            Vector2 newPos = Vector2.MoveTowards(rb.position, player.position, followSpeed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);
        }
        else
        {
            // Optional: Add idle animation or slight bounce
            rb.velocity = Vector2.zero;
        }
    }
}

   


