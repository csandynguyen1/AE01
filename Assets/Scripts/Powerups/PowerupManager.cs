using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    public static PowerupManager Instance { get; private set; }

    public float playerDamage = 3f; // This stores the player's damage value persistently
    public float playerHealth = 9f;
    public float playerSpeed = 2f;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Call this method to increase the player's damage from power-ups
    public void IncreasePlayerDamage(int amount)
    {
        playerDamage += amount;
    }
    public void IncreasePlayerHealth(int amount)
    {
        playerHealth += amount;
    }
    public void IncreasePlayerSpeed(int amount)
    {
        playerSpeed += amount;
    }
}
