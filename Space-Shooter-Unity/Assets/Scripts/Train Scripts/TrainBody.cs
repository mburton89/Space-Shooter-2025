using UnityEngine;

public class TrainBody : MonoBehaviour
{
    private SpriteRenderer sr;
    public Color flashColor = Color.white;
    public float flashDuration = 0.1f;
    private Color originalColor;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
    }

    public void OnHit()
    {
        StopAllCoroutines();
        StartCoroutine(FlashRoutine());
    }

    private System.Collections.IEnumerator FlashRoutine()
    {
        sr.color = flashColor;
        yield return new WaitForSeconds(flashDuration);
        sr.color = originalColor;
    }
}

