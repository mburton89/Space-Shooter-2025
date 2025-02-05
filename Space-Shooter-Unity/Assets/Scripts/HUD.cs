using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public static HUD Instance;

    public Image healthBarFill;

    private void Awake()
    {
        Instance = this;   
    }

    public void DisplayHealth(int currentHealth, int maxHealth)
    { 
        float healthAmount = (float)currentHealth / (float)maxHealth;

        healthBarFill.fillAmount = healthAmount;
    }
}
