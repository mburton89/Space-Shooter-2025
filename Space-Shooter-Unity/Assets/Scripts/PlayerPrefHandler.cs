using UnityEngine;
using UnityEngine.UI;

public class PlayerPrefHandler : MonoBehaviour
{
    [Header("PlayerPref Settings")]
    public string prefKey = "SelectedIndex";
    public int defaultIndex = 0;

    [Header("References")]
    public SpriteRenderer targetSpriteRenderer;
    public Image targetImage;

    [Header("Assets by Index")]
    public Sprite[] sprites;
    public Sprite[] textures;
    public ParticleSystem particleSystem;

    private int currentIndex;

    public Color[] colors;

    void Start()
    {
        // initialize index from PlayerPrefs
        currentIndex = PlayerPrefs.GetInt(prefKey, defaultIndex);
        ClampIndex();
        UpdateVisuals();
    }

    void Update()
    {
        // cycle left
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            currentIndex--;
            ClampIndex();
            UpdateVisuals();
            PlayerPrefs.SetInt(prefKey, currentIndex);
        }

        // cycle right
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentIndex++;
            ClampIndex();
            UpdateVisuals();
            PlayerPrefs.SetInt(prefKey, currentIndex);
        }
    }

    private void ClampIndex()
    {
        int max = Mathf.Max(sprites.Length, textures.Length);
        if (max == 0) return;
        // wrap-around instead of clamp for a circular list
        if (currentIndex < 0) currentIndex = max - 1;
        else if (currentIndex >= max) currentIndex = 0;
    }

    private void UpdateVisuals()
    {
        // 1. Sprite
        if (targetSpriteRenderer != null && currentIndex < sprites.Length)
            targetSpriteRenderer.sprite = sprites[currentIndex];

        // 2. UI Texture
        if (targetImage != null && currentIndex < textures.Length)
            targetImage.sprite = textures[currentIndex];

        // 3. Particle Systems
        particleSystem.startColor = colors[currentIndex];
    }

    void OnDisable()
    {
        // flush the pref when this object goes away
        PlayerPrefs.Save();
    }
}
