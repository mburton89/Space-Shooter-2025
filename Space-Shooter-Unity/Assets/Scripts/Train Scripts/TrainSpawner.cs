using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainSpawner : MonoBehaviour
{
    public GameObject trainHeadPrefab;
    public List<GameObject> trainCarPrefabs;
    public GameObject explosionEffect;
    public GameObject playerShip;

    float historySpacing = 0.1f, followSpeed = 40f;
    private int currentHits = 0;
    private bool isDespawning = false;

    public int numberOfCars = 50;
    public float headMoveSpeed = 8f;
    public float rotationSpeed = 135f;
    public float segmentSpacing = 7.5f;
    public int trainHealth = 10;

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

            //if (i % 10 == 0)
            //{
            //    Vector3 spawnPos = transform.position - new Vector3(segmentSpacing * i, 0, 0);
            //    GameObject car = Instantiate(trainCarPrefabs[2], spawnPos, Quaternion.identity);
            //    segments.Add(car.transform);
            //}
            if (i % 4 == 0)
            {
                Vector3 spawnPos = transform.position - new Vector3(segmentSpacing * i, 0, 0);
                GameObject car = Instantiate(trainCarPrefabs[1], spawnPos, Quaternion.identity);
                segments.Add(car.transform);
            }
            else
            {
                Vector3 spawnPos = transform.position - new Vector3(segmentSpacing * i, 0, 0);
                GameObject car = Instantiate(trainCarPrefabs[0], spawnPos, Quaternion.identity);
                segments.Add(car.transform);
            }
        }
    }

    void Update()
    {
        MoveHeadToPlayer();
        RecordHeadPosition();
        FollowSegments();
    }

    
    private IEnumerator DespawnTrain()
    {
        isDespawning = true;

        //start from the head and remove each segment one at a time
        foreach (Transform segment in segments)
        {
            if (segment != null)
            {
                Instantiate(explosionEffect, segment.position, Quaternion.identity);
                print(segment.gameObject);
                Destroy(segment.gameObject);
                yield return new WaitForSeconds(0.1f); // delay between each despawn
            }
        }

        segments.Clear();
    }

    // each car follows one another, being led by the train head
    void FollowSegments()
    {
        // check
        if (segments.Count == 0) return;

        for (int i = 1; i < segments.Count; i++)
        {
            Transform segment = segments[i];

            // check
            if (segment == null) continue;

            int index = Mathf.Min(Mathf.FloorToInt(i * segmentSpacing / historySpacing), positionHistory.Count - 1);
            
            //check
            if (index < 0 || index >= positionHistory.Count) continue;

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
        if (segments.Count == 0 || segments[0] == null) return;

        positionHistory.Insert(0, segments[0].position);
        int maxHistory = Mathf.CeilToInt((numberOfCars + 1) * segmentSpacing / historySpacing);
        if (positionHistory.Count > maxHistory)
            positionHistory.RemoveAt(positionHistory.Count - 1);
    }


    // follows player
    void MoveHeadToPlayer()
    {
        if (playerShip == null || segments.Count == 0 || segments[0] == null) return;

        Transform head = segments[0];
        Vector3 direction = (playerShip.transform.position - head.position).normalized;

        // rotation
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);
        head.rotation = Quaternion.RotateTowards(head.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // movement
        head.position += head.right * headMoveSpeed * Time.deltaTime;
    }

    public void RegisterHit()
    {
        if (isDespawning) return;

        currentHits++;

        if (currentHits >= trainHealth)
        {
            StartCoroutine(DespawnTrain());
        }
    }

}