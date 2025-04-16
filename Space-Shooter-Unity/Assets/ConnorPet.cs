using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnorPet : MonoBehaviour
{
    public Transform player;
    public float followSpeed = 5f;
    public float followDistance = 1.5f;
    public Vector2 offset = new Vector2(-1f, 0f); // pet trails behind a bit

    void LateUpdate()
    {
        if (player == null) return;

        Vector2 targetPosition = (Vector2)player.position + offset;
        float distance = Vector2.Distance(transform.position, targetPosition);

        if (distance > followDistance)
        {
            Vector2 newPos = Vector2.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
            transform.position = newPos;
        }
    }
}
