using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DamagePowerup : MonoBehaviour
{
    public int additionalDamage = 1; // The additional damage the power-up provides
    public string nextLevel;


    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("powerup collision");
        if (other.CompareTag("Player"))
        {
            Debug.Log("player touched powerup");
            // Increase the damage using the GameManager
            PowerupManager.Instance.IncreasePlayerDamage(additionalDamage);

            // Optional: Destroy the power-up after it's been collected
            DestroyPowerups();
            EnterNextLevel();
        }
    }
    private void DestroyPowerups()
    {
        Debug.Log("powerups deleted");
        GameObject[] powerups = GameObject.FindGameObjectsWithTag("Powerup");
        foreach (GameObject powerup in powerups)
        {
            Destroy(powerup);
        }
    }
    private void EnterNextLevel()
    {
        if (nextLevel != null)
        {
            SceneManager.LoadScene(nextLevel);
        }
    }
}
