using System.Collections.Generic;
using UnityEngine;

public class SegmentedTrainMouseFollow2D : MonoBehaviour
{
    public GameObject trainHeadPrefab;
    public GameObject trainCarPrefab;
    public int numberOfCars = 5;
    public float segmentSpacing = 0.5f;
    public float followSpeed = 10f;
    public float headMoveSpeed = 5f;

    private List<Transform> segments = new List<Transform>();

    void Start()
    {
        // Spawn head
        GameObject head = Instantiate(trainHeadPrefab, transform.position, Quaternion.identity);
        segments.Add(head.transform);

        // Spawn cars
        for (int i = 1; i <= numberOfCars; i++)
        {
            Vector3 spawnPos = transform.position - new Vector3(segmentSpacing * i, 0, 0);
            GameObject car = Instantiate(trainCarPrefab, spawnPos, Quaternion.identity);
            segments.Add(car.transform);
        }
    }

    void Update()
    {
        MoveHeadToMouse();
        FollowSegments();
    }

    void MoveHeadToMouse()
    {
        Transform head = segments[0];

        // Get mouse world position
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        // Move toward the mouse
        head.position = Vector3.MoveTowards(head.position, mousePos, headMoveSpeed * Time.deltaTime);

        // Optional: rotate head toward the mouse
        Vector2 dir = mousePos - head.position;
        if (dir.sqrMagnitude > 0.001f)
        {
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            head.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    void FollowSegments()
    {
        for (int i = 1; i < segments.Count; i++)
        {
            Transform current = segments[i];
            Transform target = segments[i - 1];

            float dist = Vector2.Distance(current.position, target.position);
            if (dist > segmentSpacing)
            {
                Vector2 direction = (target.position - current.position).normalized;
                Vector2 newPos = Vector2.Lerp(current.position, target.position, followSpeed * Time.deltaTime);
                current.position = newPos;

                // Rotate toward the previous segment
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                current.rotation = Quaternion.Euler(0, 0, angle);
            }
        }
    }
}