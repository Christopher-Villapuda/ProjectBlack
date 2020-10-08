using System.Collections;
using Fungus;
using System.Collections.Generic;
using UnityEngine;

[EventHandlerInfo("Test", "Death","This is a test tooltip.")] 

public class DeathEventHandler : EventHandler
{
    private void OnPlayerDied()
    {
        ExecuteBlock();
    }
    private void OnEnable()
    {
        PlayerHealth.PlayerDied += OnPlayerDied;
    }

    private void OnDisable()
    {
        PlayerHealth.PlayerDied -= OnPlayerDied;
    }
}
