using UnityEngine;
using System.Collections;

public class TrainBody : MonoBehaviour
{
    private SpriteRenderer sr;
    public float flashDuration = 0.1f;
    private Color originalColor;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Ship ship = collision.GetComponent<Ship>();
        if (ship != null)
        {
            ship.TakeDamage(1);

            if (ship.currentHealth <= 0)
            {
                ship.Explode();
            }

            return;
        }   
    }

    public void OnHit()
    {
        StopAllCoroutines();
        StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        sr.color = Color.white;
        yield return new WaitForSeconds(flashDuration);
        sr.color = originalColor;
    }
}

