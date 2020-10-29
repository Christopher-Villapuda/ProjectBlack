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
    [SerializeField]
    private int currentLives;
    public Text livesShown;
    private void Start()
    {
        currentLives = maxLives;
        livesShown.text = currentLives.ToString();
    }

    public void TakeDamage(int amount)
    {
        currentLives -= amount;
        livesShown.text = currentLives.ToString();
        DeathCheck();
    }

    public void Heal(int amount)
    {
        currentLives += amount;
        livesShown.text = currentLives.ToString();
    }

    private void DeathCheck()
    {
       if(currentLives <= 0)
        {
            PlayerDied?.Invoke();
        }
    }
}

