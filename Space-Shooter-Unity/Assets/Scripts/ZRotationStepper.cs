using UnityEngine;

public class ZRotationSpawner : MonoBehaviour
{
    public GameObject prefab;         // Prefab to instantiate
    public float rotationStep = 10f;  // Rotation step in degrees
    public Vector3 spawnPosition;     // Where to spawn each object

    void Start()
    {
        SpawnObjectsWithRotation();
    }

    void SpawnObjectsWithRotation()
    {
        float currentRotation = 0f;

        while (currentRotation < 360f)
        {
            Quaternion rotation = Quaternion.Euler(0f, 0f, currentRotation);
            Instantiate(prefab, spawnPosition, rotation, transform);
            currentRotation += rotationStep;
        }
    }
}
