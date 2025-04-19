using System.Collections.Generic;
using UnityEngine;

public class TrainSpawner : MonoBehaviour
{
    public GameObject trainHeadPrefab;
    public GameObject trainCarPrefab;
    public GameObject playerShip;

    public int numberOfCars = 50;
    public float headMoveSpeed = 8f;
    public float rotationSpeed = 135f;

    float historySpacing = 0.1f, followSpeed = 40f, segmentSpacing = 9.5f;

    // manages amount of segments
    private List<Transform> segments = new List<Transform>();
    // keeps track of path history
    private List<Vector3> positionHistory = new List<Vector3>();

    void Start()
    {
        // spawns head of train
        GameObject head = Instantiate(trainHeadPrefab, transform.position, Quaternion.identity);
        segments.Add(head.transform);

        // spawns train cars (with a totally radical 'for-loop')
        for (int i = 1; i <= numberOfCars; i++)
        {
            Vector3 spawnPos = transform.position - new Vector3(segmentSpacing * i, 0, 0);
            GameObject car = Instantiate(trainCarPrefab, spawnPos, Quaternion.identity);
            segments.Add(car.transform);
        }
    }

    void Update()
    {
        MoveHeadToPlayer();
        RecordHeadPosition();
        FollowSegments();
    }

    // each car follows one another, being led by the train head
    void FollowSegments()
    {
        for (int i = 1; i < segments.Count; i++)
        {
            Transform segment = segments[i];
            int index = Mathf.Min(Mathf.FloorToInt(i * segmentSpacing / historySpacing), positionHistory.Count - 1);
            Vector3 targetPos = positionHistory[index];

            // movement
            segment.position = Vector3.Lerp(segment.position, targetPos, followSpeed * Time.deltaTime);

            // rotation
            Vector3 direction = targetPos - segment.position;
            if (direction.sqrMagnitude > 0.001f)
            {
                float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);
                segment.rotation = Quaternion.RotateTowards(segment.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }

    // records train head position and places its location into list for body to follow
    void RecordHeadPosition()
    {
        positionHistory.Insert(0, segments[0].position);
        int maxHistory = Mathf.CeilToInt((numberOfCars + 1) * segmentSpacing / historySpacing);
        if (positionHistory.Count > maxHistory)
            positionHistory.RemoveAt(positionHistory.Count - 1);
    }


    // follows player
    void MoveHeadToPlayer()
    {
        if (playerShip == null) return;

        Transform head = segments[0];
        Vector3 direction = (playerShip.transform.position - head.position).normalized;

        // rotation
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);
        head.rotation = Quaternion.RotateTowards(head.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // movement
        head.position += head.right * headMoveSpeed * Time.deltaTime;
    }

}