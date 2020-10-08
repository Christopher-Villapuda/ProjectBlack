using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static event Action PlayerDied;

    [SerializeField]
    private int maxLives = 3;
    private int currentLives;
    private void Start()
    {
        currentLives = maxLives;
    }

    public void TakeDamage(int amount)
    {
        currentLives -= amount;
        DeathCheck();
    }

    private void DeathCheck()
    {
       if(currentLives <= 0)
        {
            PlayerDied?.Invoke();
        }
    }
}

