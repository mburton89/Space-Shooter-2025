using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterSelector : MonoBehaviour
{
    [Header("UI References")]
    public Button leftButton;
    public Button rightButton;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public Image characterImage;
    public Image portraitImage;

    [Header("Character Data")]
    public string[] characterNames;
    [TextArea] public string[] characterDescriptions;
    public Sprite[] characterSprites;
    public Sprite[] characterPortraits;

    [Header("PlayerPref Settings")]
    public string prefKey = "SelectedCharacterIndex";
    public int defaultIndex = 0;

    private int currentIndex;

    void Start()
    {
        // Load saved index (or default), clamp, and apply
        currentIndex = PlayerPrefs.GetInt(prefKey, defaultIndex);
        ClampIndex();
        UpdateUI();

        // Hook up buttons
        leftButton.onClick.AddListener(() => Cycle(-1));
        rightButton.onClick.AddListener(() => Cycle(+1));
    }

    private void Cycle(int direction)
    {
        if (characterNames.Length == 0) return;

        // Advance index with wrap-around
        currentIndex = (currentIndex + direction + characterNames.Length) % characterNames.Length;

        // Save selection
        PlayerPrefs.SetInt(prefKey, currentIndex);
        PlayerPrefs.Save();

        UpdateUI();
    }

    private void UpdateUI()
    {
        // Update text
        nameText.text = characterNames[currentIndex];
        descriptionText.text = characterDescriptions[currentIndex];

        // Update main sprite
        if (currentIndex < characterSprites.Length && characterSprites[currentIndex] != null)
            characterImage.sprite = characterSprites[currentIndex];

        // Update portrait
        if (currentIndex < characterPortraits.Length && characterPortraits[currentIndex] != null)
            portraitImage.sprite = characterPortraits[currentIndex];
    }

    private void ClampIndex()
    {
        if (characterNames.Length == 0)
        {
            currentIndex = 0;
            return;
        }
        currentIndex = Mathf.Clamp(currentIndex, 0, characterNames.Length - 1);
    }
}
