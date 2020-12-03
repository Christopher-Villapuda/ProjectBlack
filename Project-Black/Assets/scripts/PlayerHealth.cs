using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public static event Action PlayerDied;
    [SerializeField]
    private int maxLives = 3;
    private int currentLives;
    [SerializeField]
    private int startingMedkits = 0;
    private int currentMedkits;
    public Text livesShown;
    public Text medkitsShown;
    private void Start()
    {
        currentMedkits = startingMedkits;
        currentLives = 3;
        UpdateHealthText();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentMedkits >= 1 && currentLives < maxLives)
            {
                Heal(1);
            }
        }
    }

    public void TakeDamage(int amount)
    {
        currentLives -= amount;
        UpdateHealthText();
        DeathCheck();
    }

    public void MedkitFound(int amount)
    {
        currentMedkits += amount;
        medkitsShown.text = currentMedkits.ToString();
    }

    public void Heal(int amount)
    {


        currentLives += amount;
        currentMedkits -= amount;
        UpdateHealthText();

    }

    private void UpdateHealthText()
    {
        livesShown.text = currentLives.ToString();
        medkitsShown.text = currentMedkits.ToString();
    }

    private void DeathCheck()
    {
       if(currentLives <= 0)
        {
            PlayerDied?.Invoke();
        }
    }
}

