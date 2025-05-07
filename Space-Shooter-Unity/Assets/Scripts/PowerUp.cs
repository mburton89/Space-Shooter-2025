using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [Header(" ===== General ===== ")]
    public Transform spawnPoint;
    private PlayerShip player;

    [Header(" ===== Health ===== ")]
    public bool restoreHealth;
    public int restoreHealthAmount;

    [Header(" ===== Speed ===== ")]
    public bool increaseSpeed;
    public bool revertSpeed;
    public int powerUpTimer;
    private float originalSpeed;
    private Coroutine revertCoroutine;
    public GameObject revertSpeedPrefab;

    [Header(" ===== MegaLaser ===== ")]
    public bool megaLaserRecharge;
    

    //public List<GameObject> enemyShipPrefabs;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerShip>())
        {
            player = collision.GetComponent<PlayerShip>();  // Get the PlayerShip reference from the player
            PowerUpEffect();
            Destroy(gameObject); 
        }
    }

    public void PowerUpEffect()
    {
        if (player == null) return;  // If no player reference, exit

        if (restoreHealth)
        {
            player.currentHealth += restoreHealthAmount;  

            if (player.currentHealth > player.maxHealth)
            {
                player.currentHealth = player.maxHealth; 
            }
            HUD.Instance.DisplayHealth(player.currentHealth, player.maxHealth);
        }

    if (increaseSpeed)
      {
        // Cancel previous coroutine if running
        if (revertCoroutine != null)
        {
            StopCoroutine(revertCoroutine);
            player.maxMovementSpeed = originalSpeed; // reset before reapplying
        }

        originalSpeed = player.maxMovementSpeed;
        player.maxMovementSpeed *= 1.5f;

            if (revertSpeedPrefab == null)
            {
            Debug.LogError("revertSpeedPrefab is not assigned in the Inspector!");
            return;
            }

        GameObject revertSpeedPre = Instantiate(revertSpeedPrefab);
      }

        if (megaLaserRecharge)
        {
            player.megaLaserReady = true;
            HUD.Instance.DisplayMLaser(player.megaLaserReady);
        }
    }
    private IEnumerator RevertSpeedAfterDelay()
    {
        yield return new WaitForSeconds(powerUpTimer);
        player.maxMovementSpeed /= 1.5f;
        print("current Max Speed" + player.maxMovementSpeed);
        revertCoroutine = null;
        Destroy(gameObject);
    }

    void Start()
    {
      if (revertSpeed)
      {
        player = FindObjectOfType<PlayerShip>();
        revertCoroutine = StartCoroutine(RevertSpeedAfterDelay());
      }
    }

    void Update()
    {
      
    }

    
    //public void SpawnPowerUp()
    //{
    //  int index = Random.Range(0, powerUpPrefabs.Count);
    //Instantiate(powerUpPrefabs[index], spawnPoint.position, transform.rotation, null);
    //}

    
}
