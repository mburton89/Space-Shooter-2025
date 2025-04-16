using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LilyPet : MonoBehaviour
{
    [Header("Target to Follow")]
    public Transform target;

    [Header("Follow Settings")]
    public float followSpeed = 5f;
    public bool smoothFollow = true;
    public Vector3 offset = Vector3.zero;

    void Update()
    {
        if (target == null) return;

        Vector3 targetPosition = target.position + offset;

        if (smoothFollow)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
    }
}
