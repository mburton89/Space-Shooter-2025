using UnityEngine;
using System.Collections.Generic;

public class SpriteScatterer : MonoBehaviour
{
    public GameObject objectToScatter; // The prefab to scatter
    public int numberToScatter = 10;   // Number of objects to scatter
    public int maxAttemptsPerObject = 100; // Max tries before giving up on placing one

    private List<Bounds> placedBounds = new List<Bounds>();

    void Start()
    {
        ScatterObjects();
    }

    void ScatterObjects()
    {
        SpriteRenderer baseRenderer = GetComponent<SpriteRenderer>();
        if (baseRenderer == null || objectToScatter == null)
        {
            Debug.LogWarning("Missing SpriteRenderer or objectToScatter prefab.");
            return;
        }

        Bounds areaBounds = baseRenderer.bounds;
        int placedCount = 0;
        int attempts = 0;

        while (placedCount < numberToScatter && attempts < numberToScatter * maxAttemptsPerObject)
        {
            attempts++;

            // TEMP instantiate off-screen to get actual size
            GameObject temp = Instantiate(objectToScatter, Vector3.one * 10000, Quaternion.identity);
            SpriteRenderer tempSR = temp.GetComponent<SpriteRenderer>();

            if (tempSR == null)
            {
                Debug.LogWarning("objectToScatter must have a SpriteRenderer.");
                Destroy(temp);
                return;
            }

            Vector2 objSize = tempSR.bounds.size;

            float randomX = Random.Range(areaBounds.min.x + objSize.x / 2, areaBounds.max.x - objSize.x / 2);
            float randomY = Random.Range(areaBounds.min.y + objSize.y / 2, areaBounds.max.y - objSize.y / 2);
            Vector3 finalPos = new Vector3(randomX, randomY, transform.position.z);

            Bounds newBounds = new Bounds(finalPos, objSize);

            if (!IsOverlapping(newBounds))
            {
                temp.transform.position = finalPos;

                // Assign random vivid color
                Color vividColor = Color.HSVToRGB(Random.value, 1f, 1f);
                tempSR.color = vividColor;

                placedBounds.Add(newBounds);
                placedCount++;
            }
            else
            {
                Destroy(temp); // Retry with new object
            }
        }

        if (placedCount < numberToScatter)
        {
            Debug.LogWarning($"Only placed {placedCount} out of {numberToScatter} objects due to space constraints.");
        }
    }

    bool IsOverlapping(Bounds newBounds)
    {
        foreach (Bounds existing in placedBounds)
        {
            if (existing.Intersects(newBounds))
                return true;
        }
        return false;
    }
}
