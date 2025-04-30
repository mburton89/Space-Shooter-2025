using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : MonoBehaviour
{
    public static HUD Instance;

    public Image healthBarFill;
    public Image dashBoxFill;
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI bestText;
    public TextMeshProUGUI minesText;

    private void Awake()
    {
        Instance = this;   
    }

    public void DisplayHealth(int currentHealth, int maxHealth)
    { 
        float healthAmount = (float)currentHealth / (float)maxHealth;

        healthBarFill.fillAmount = healthAmount;
    }
    public void DisplayDash(bool isDashReady)
    {
        dashBoxFill.enabled = isDashReady;
    }

    public void DisplayWave(int currentWave)
    {
        waveText.SetText("Wave " + currentWave);
    }

    public void DisplayBest(int bestWave)
    {
        bestText.SetText("Best " + bestWave);
    }

    public void DisplayMineCount(int minesRemaining)
    {
        minesText.SetText("Mines: " + minesRemaining);
    }
}
