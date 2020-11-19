using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public static event Action PlayerDied;

    private int maxLives = 3;
    [SerializeField]
    private int currentLives;
    private int medkits = 0;
    [SerializeField]
    private int currentMedkits;
    public Text livesShown;
    public Text medkitsShown;
    private void Start()
    {
        currentMedkits = medkits;
        currentLives = maxLives;
        livesShown.text = currentLives.ToString();
        medkitsShown.text = currentMedkits.ToString();
    }

    public void TakeDamage(int amount)
    {
        currentLives -= amount;
        livesShown.text = currentLives.ToString();
        medkitsShown.text = currentMedkits.ToString();
        DeathCheck();
    }

    public void MedkitFound(int amount)
    {
        currentMedkits += amount;
        medkitsShown.text = currentMedkits.ToString();
    }

    public void Heal(int amount)
    {
        if (Input.GetKeyDown("E"))
        {
            if (currentMedkits >= 1 && currentLives < maxLives)
            {
                currentLives += amount;
                currentMedkits -= amount;
                livesShown.text = currentLives.ToString();
                medkitsShown.text = currentMedkits.ToString();
            }

        }
        else
        {
            livesShown.text = currentLives.ToString();
            medkitsShown.text = currentMedkits.ToString();
        }
       
    }

    private void DeathCheck()
    {
       if(currentLives <= 0)
        {
            PlayerDied?.Invoke();
        }
    }
}

