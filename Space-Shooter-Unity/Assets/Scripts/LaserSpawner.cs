using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSpawner : MonoBehaviour
{
    public GameObject laserPrefab;
    public float spawnInterval = 0.5f;
    public float laserSpeed = 5f;
    public float laserLifetime = 4f;

    private Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;
        InvokeRepeating(nameof(SpawnLaser), 0f, spawnInterval);
    }

    void SpawnLaser()
    {
        Vector2 spawnPos;
        Vector2 targetPos;

        int side = Random.Range(0, 4); // 0=left, 1=right, 2=top, 3=bottom

        // ViewportToWorldPoint uses (0,0) = bottom-left, (1,1) = top-right
        float buffer = 1f; // how far offscreen to spawn
        switch (side)
        {
            case 0: // Left
                spawnPos = mainCam.ViewportToWorldPoint(new Vector3(-buffer, Random.value, 0));
                break;
            case 1: // Right
                spawnPos = mainCam.ViewportToWorldPoint(new Vector3(1 + buffer, Random.value, 0));
                break;
            case 2: // Top
                spawnPos = mainCam.ViewportToWorldPoint(new Vector3(Random.value, 1 + buffer, 0));
                break;
            default: // Bottom
                spawnPos = mainCam.ViewportToWorldPoint(new Vector3(Random.value, -buffer, 0));
                break;
        }

        targetPos = mainCam.ViewportToWorldPoint(new Vector3(Random.value, Random.value, 0));

        Vector2 direction = (targetPos - spawnPos).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        GameObject laser = Instantiate(laserPrefab, spawnPos, Quaternion.Euler(0f, 0f, angle));

        LaserMover mover = laser.AddComponent<LaserMover>();
        mover.direction = direction;
        mover.speed = laserSpeed;
        mover.lifetime = laserLifetime;
    }
}
