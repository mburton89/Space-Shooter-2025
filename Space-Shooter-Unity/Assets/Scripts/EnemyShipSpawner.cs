using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipSpawner : MonoBehaviour
{
    public static EnemyShipSpawner Instance;

    int baseNumberOfShips;
    int currentNumberOfShips;
    int currentWave = 1;

    public Transform spawnPoint;
    public Transform pivot;
    public List<GameObject> enemyShipPrefabs;

    public GameObject playerShip;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        baseNumberOfShips = FindObjectsOfType<EnemyShip>().Length;
        currentNumberOfShips = baseNumberOfShips;

        HUD.Instance.DisplayBest(PlayerPrefs.GetInt("HighestWave"));
    }

    public void SpawnWaveOfEnemies()
    {
        int numberOfEnemiesToSpawn = baseNumberOfShips + currentWave - 1;

        for (int i = 0; i < numberOfEnemiesToSpawn; i++)
        {
            int index = Random.Range(0, enemyShipPrefabs.Count);
            float zRotation = Random.Range(0, 360);

            pivot.eulerAngles = new Vector3(0, 0, zRotation);
            Instantiate(enemyShipPrefabs[index], spawnPoint.position, transform.rotation, null);
        }
    }

    public void CountEnemyShips()
    { 
        currentNumberOfShips = FindObjectsOfType<EnemyShip>().Length;

        print("Number of Enemy Ships: " + currentNumberOfShips);

        if (currentNumberOfShips == 1)
        {
            currentWave++;
            HUD.Instance.DisplayWave(currentWave);
            SpawnWaveOfEnemies();

            if (currentWave > PlayerPrefs.GetInt("HighestWave"))
            {
                // YAAAYY WE SET A NEW HIGH SCORE
                PlayerPrefs.SetInt("HighestWave", currentWave);
                HUD.Instance.DisplayBest(PlayerPrefs.GetInt("HighestWave"));
            }
        }
    }
}
